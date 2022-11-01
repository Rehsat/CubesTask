using Task.Utils;
using Task.MovableObjects;
using System.Text;
using UnityEngine;
namespace Task
{
    [RequireComponent(typeof(StringToValuesConverter))]
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private MovableObjectsFactory _movableObjectsFactory;
        [SerializeField] private MovableObjectsInputController _movableObjectsInputController;

        private StringToValuesConverter _converter;
        private InstantinateStates _currentState;

        private float _currentCubeSpawnTime;
        private float _currentCubeSpeed;

        private const int STATES_COUNT = 3;
        public InstantinateStates CurrentState
        {
            get => _currentState;
            set
            {
                if ((int)value >= STATES_COUNT)
                    _currentState = 0;
                else
                    _currentState = value;

                if (value == InstantinateStates.MoveVector)
                    WriteCurrentState(CurrentState, "", " (input example: 1,-2,12)");
                else
                    WriteCurrentState(CurrentState);
            }
        }
        private void Awake()
        {
            _converter = GetComponent<StringToValuesConverter>();
            _converter.Init(_movableObjectsInputController);
        }
        private void OnEnable()
        {
            _movableObjectsInputController.OnInputedTextChange += TryConvertStringByState;

            WriteCurrentState(CurrentState);
        }
        private void TryConvertStringByState(string stringToConvert)
        {
            try
            {
                ConvertStringByState(stringToConvert);
            }
            catch
            {
                WriteCurrentState(CurrentState, "Ucnorrect input. ");
            }
        }
        private void ConvertStringByState(string stringToConvert)
        {
            switch (CurrentState)
            {
                case InstantinateStates.TimeToSpawn:
                    _currentCubeSpawnTime = _converter.ConvertStringToFloat(stringToConvert);
                    break;
                case InstantinateStates.Speed:
                    _currentCubeSpeed = _converter.ConvertStringToFloat(stringToConvert);
                    break;
                case InstantinateStates.MoveVector:
                    var currentCubeMoveVector = _converter.ConvertStringToVector3(stringToConvert);
                    CreateMovableObject(_currentCubeSpawnTime, _currentCubeSpeed, currentCubeMoveVector);
                    break;
            }
            CurrentState++;
        }
        private void CreateMovableObject(float timeToSpawn, float speed, Vector3 moveVector)
        {
            var movableObjectData = new MovableObjectData(timeToSpawn, moveVector);
            _movableObjectsFactory.CreateMovableObject(movableObjectData, timeToSpawn);
        }

        private void WriteCurrentState(InstantinateStates state, string beforeTitle = "", string afterTitle = "")
        {
            StringBuilder newString = new StringBuilder();
            newString.Append(beforeTitle).Append("Enter ").Append(state.ToString()).Append(afterTitle);
            _movableObjectsInputController.ChangeTitleText(newString.ToString());
        }
        private void OnDisable()
        {
            _movableObjectsInputController.OnInputedTextChange -= TryConvertStringByState;

        }
    }
    public enum InstantinateStates
    {
        TimeToSpawn,
        Speed,
        MoveVector
    }
}