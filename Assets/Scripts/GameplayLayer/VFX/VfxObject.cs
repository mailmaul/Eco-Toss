using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.ObjectPooling;

namespace EcoTeam.EcoToss.VFX
{
    public class VfxObject : PoolObject
    {
        public void UnUse()
        {
            StoreToPool();
        }
    }
}

