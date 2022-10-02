using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kelompok6.EcoToss.ObjectPooling
{
    public class PoolingSystem : MonoBehaviour
    {
        public int AmountToPool;
        Queue<IPoolObject> _storedList = new Queue<IPoolObject>();
        Queue<IPoolObject> _spawnedList = new Queue<IPoolObject>();

        public PoolingSystem(int poolLimit = 10)
        {
            this.AmountToPool = poolLimit;
        }

        public IPoolObject CreateObject(IPoolObject objectPrefab, Vector3 spawnPos, Transform parent = null)
        {
            IPoolObject outObject;
            if (_storedList.Count < 1 || _storedList.Peek().gameObject == null)
            {
                outObject = MonoBehaviour.Instantiate(objectPrefab.gameObject).
                GetComponent<IPoolObject>();
                outObject.Initial(this);
            }
            else
            {
                outObject = _storedList.Dequeue();
            }
            outObject.transform.position = spawnPos;
            outObject.transform.parent = parent;


            outObject.OnCreate();
            outObject.gameObject.SetActive(true);

            _spawnedList.Enqueue(outObject);


            return outObject;

        }

        // Overload with Rotation
        public IPoolObject CreateObject(IPoolObject objectPrefab, Vector3 spawnPosition, Quaternion spawnRotation, Transform parent = null)
        {
            IPoolObject outObject;
            if (_storedList.Count < 1)
            {
                outObject = MonoBehaviour.Instantiate(objectPrefab.gameObject).
                GetComponent<IPoolObject>();
                outObject.Initial(this);
            }
            else
            {
                outObject = _storedList.Dequeue();
            }
            outObject.transform.position = spawnPosition;
            outObject.transform.rotation = spawnRotation;
            outObject.transform.parent = parent;


            outObject.OnCreate();
            outObject.gameObject.SetActive(true);

            _spawnedList.Enqueue(outObject);


            return outObject;

        }

        public void Store(IPoolObject poolObject)
        {
            _storedList.Enqueue((IPoolObject)poolObject);
        }
    }
}