using UnityEngine;
using UnityEngine.Events;

namespace Task.MovableObjects
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovableObject : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onReachedGoalPoint;
        private MovableObjectData _data;
        private Rigidbody _rigidbody;

        private Vector3 _goalPoint;

        private bool _isMoving;
        public void Init(MovableObjectData data)
        {
            _data = data;
            _goalPoint = transform.position + _data.MoveVector;
            _rigidbody = GetComponent<Rigidbody>();
            StartMoving();
        }
        private void StartMoving()
        {
            _isMoving = true;
        }
        private void FixedUpdate()
        {
            if (_isMoving)
            {
                Move();
                if (CheckDistanceToDestiny() <= 0)
                {
                    _onReachedGoalPoint?.Invoke();
                }
            }
        }
        private void Move()
        {
            var nextMove = Vector3.MoveTowards(transform.position, _goalPoint, _data.Speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(nextMove);
        }
        private float CheckDistanceToDestiny()
        {
            return Vector3.Distance(transform.position, _goalPoint);
        }
    }
    public class MovableObjectData
    {
        private float _speed;
        private Vector3 _move;

        public float Speed => _speed;
        public Vector3 MoveVector => _move;

        public MovableObjectData(float speed, Vector3 move)
        {
            _speed = speed;
            _move = move;
        }
    }
}
