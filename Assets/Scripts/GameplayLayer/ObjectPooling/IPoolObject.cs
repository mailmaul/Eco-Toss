using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.ObjectPooling
{
    public interface IPoolObject
    {
        public Transform transform { get; }
        public GameObject gameObject { get; }
        PoolingSystem Pooling { get; }

        void Initial(PoolingSystem poolSystem);
        //void OnCreate();
        void StoreToPool();
    }
}