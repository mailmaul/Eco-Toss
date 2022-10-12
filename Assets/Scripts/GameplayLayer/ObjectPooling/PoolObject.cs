using EcoTeam.EcoToss.PubSub;
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
            //Debug.Log("Intial" + poolSystem);
            Pooling = poolSystem;
        }
        public abstract void OnCreate();
        public virtual void StoreToPool()
        {
            //Debug.Log(Pooling);
            //Debug.Log("store to pool tanpa message");
            Pooling.Store(this);
            gameObject.SetActive(false);
        }

        // Overload with MessageStoreToPool
        public virtual void StoreToPoolWithMessage(MessageStoreToPool message)
        {
            //Debug.Log(Pooling);
            //Debug.Log("store to pool dengan message");
            Pooling.Store(this);
            gameObject.SetActive(false);
        }
    }
}