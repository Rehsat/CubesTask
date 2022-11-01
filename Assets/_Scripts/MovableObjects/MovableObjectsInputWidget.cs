using UnityEngine.UI;
using UnityEngine;

namespace Task.MovableObjects
{
    public class MovableObjectsInputWidget : MonoBehaviour
    {
        [SerializeField] private Text _title;
        [SerializeField] private InputField _inputField;
        public InputField InputField => _inputField;
        public Text Title => _title;
    }
}