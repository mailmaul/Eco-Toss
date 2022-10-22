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
        [Header("Properties")]
        [SerializeField] protected float _speed;
        [SerializeField] protected float _timer;
        protected float _currentTime;
        protected bool _isMove;

        public abstract void Movement();
        public abstract void Intrude();
    }
}

