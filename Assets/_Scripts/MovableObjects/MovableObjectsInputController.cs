using System;
using Task.Utils;
using UnityEngine;

namespace Task.MovableObjects
{
    public class MovableObjectsInputController : MonoBehaviour, IStringReturner
    {
        [SerializeField] private MovableObjectsInputWidget _widget;

        public string InputedText { get; set; }
        public Action<string> OnInputedTextChange;
        private void OnEnable()
        {
            var inputFieldOnEndEdit = _widget.InputField.onEndEdit;
            inputFieldOnEndEdit.AddListener(SaveInputedText);
        }
        public void SaveInputedText(string text)
        {
            InputedText = text;
            _widget.InputField.text = String.Empty;
            OnInputedTextChange(InputedText);
        }
        public string GetString()
        {
            return InputedText;
        }
        public void ChangeTitleText(string newTitle)
        {
            _widget.Title.text = newTitle;
        }
        private void OnDisable()
        {
            var inputFieldOnEndEdit = _widget.InputField.onEndEdit;
            inputFieldOnEndEdit.AddListener(SaveInputedText);
        }
    }
}
