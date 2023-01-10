using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.ObjectPooling
{
    public class PoolingSystem
    {
        public int AmountToPool;
        List<IPoolObject> _storedList = new List<IPoolObject>();
        List<IPoolObject> _spawnedList = new List<IPoolObject>();

        public PoolingSystem(int AmountToPool = 10)
        {
            this.AmountToPool = AmountToPool;
        }

        public IPoolObject CreateObject(IPoolObject objectPrefab, Vector3 spawnPosition, Transform parent = null)
        {
            IPoolObject outObject;
            if (_storedList.Count < AmountToPool)
            {
                outObject = MonoBehaviour.Instantiate(objectPrefab.gameObject).GetComponent<IPoolObject>();
                outObject.Initial(this);
            }
            else
            {
                outObject = _storedList[Random.Range(0, _storedList.Count)];
                _storedList.Remove(outObject);
            }
            outObject.transform.position = spawnPosition;
            outObject.transform.parent = parent;


            outObject.gameObject.SetActive(true);

            _spawnedList.Add(outObject);


            return outObject;

        }

        // Overload with Rotation
        public IPoolObject CreateObject(IPoolObject objectPrefab, Vector3 spawnPosition, Quaternion spawnRotation, Transform parent = null)
        {
            IPoolObject outObject;
            if (_storedList.Count < AmountToPool)
            {
                outObject = MonoBehaviour.Instantiate(objectPrefab.gameObject).GetComponent<IPoolObject>();
                outObject.Initial(this);
            }
            else
            {
                outObject = _storedList[Random.Range(0, _storedList.Count)];
                _storedList.Remove(outObject);
            }
            outObject.transform.position = spawnPosition;
            outObject.transform.rotation = spawnRotation;
            outObject.transform.parent = parent;


            outObject.gameObject.SetActive(true);

            _spawnedList.Add(outObject);


            return outObject;

        }

        public void Store(IPoolObject poolObject)
        {
            _storedList.Add((IPoolObject)poolObject);
        }

        public void Clear()
        {
            _storedList.Clear();
            _spawnedList.Clear();
        }
    }
}