using System.Collections;
using UnityEngine;

namespace Task.MovableObjects
{
    public class MovableObjectsFactory : MonoBehaviour
    {
        [SerializeField] private MovableObject _objectPrefab;
        public void CreateMovableObject(MovableObjectData movableObjectData, float timeToSpawn)
        {
            StartCoroutine(StartSpawning(movableObjectData, timeToSpawn));
        }
        private IEnumerator StartSpawning(MovableObjectData movableObjectData, float timeToSpawn)
        {
            yield return new WaitForSeconds(timeToSpawn);
            var newObject = Instantiate(_objectPrefab, transform.position, Quaternion.identity);
            newObject.Init(movableObjectData);
        }
    }
}