using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Task.Utils
{
    public class Destroyer : MonoBehaviour
    {
        public void DestroyObject(GameObject objectToDestroy)
        {
            Destroy(objectToDestroy);
        }
    }

}