using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.ObjectPooling
{
    public abstract class PoolObject : MonoBehaviour, IPoolObject
    {
        public PoolingSystem Pooling { private set; get; }
        void IPoolObject.Initial(PoolingSystem poolSystem)
        {
            Debug.Log("Intial" + poolSystem);
            Pooling = poolSystem;
        }
        public abstract void OnCreate();
        public virtual void StoreToPool()
        {
            Debug.Log(Pooling);
            Pooling.Store(this);
            gameObject.SetActive(false);
        }
    }
}