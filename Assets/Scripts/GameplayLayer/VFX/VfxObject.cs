using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.ObjectPooling;

namespace EcoTeam.EcoToss.VFX
{
    public class VfxObject : PoolObject
    {
        private void Start()
        {
            Invoke("StoreToPool", 3f);
        }

        public override void StoreToPool()
        {
            base.StoreToPool();
        }
    }
}

