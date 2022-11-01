using System;
using UnityEngine;

namespace Task.Utils
{
    public class StringToValuesConverter : MonoBehaviour
    {
        private IStringReturner _stringReturner;
        public void Init(IStringReturner stringReturner)
        {
            _stringReturner = stringReturner;
        }
        public Vector3 ConvertStringToVector3(string stringToConvert, char spliter = ',')
        {
            string[] coordinatesString = stringToConvert.Split(spliter);

            var xCoordinate = ConvertStringToFloat(coordinatesString[0]);
            var yCoordinate = ConvertStringToFloat(coordinatesString[1]);
            var zCoordinate = ConvertStringToFloat(coordinatesString[2]);

            return new Vector3(xCoordinate, yCoordinate, zCoordinate);
        }
        public float ConvertStringToFloat(string stringToConvert)
        {
            return (float)Convert.ToDouble(stringToConvert);
        }
    }
}
