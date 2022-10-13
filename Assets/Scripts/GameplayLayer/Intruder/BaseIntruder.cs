using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.ObjectPooling;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.Intruder
{
    public abstract class BaseIntruder : PoolObject
    {
        public abstract void Movement();
        public abstract void Intrude();
    }
}
