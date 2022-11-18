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
            Pooling = poolSystem;
        }

        public virtual void StoreToPool()
        {
            Pooling.Store(this);
            gameObject.SetActive(false);
        }

        // Overload with MessageStoreToPool
        public virtual void StoreToPoolWithMessage(MessageStoreToPool message)
        {
            Pooling.Store(this);
            gameObject.SetActive(false);
        }
    }
}