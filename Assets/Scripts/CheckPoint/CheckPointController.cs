using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.CheckPoint
{
    public class CheckPointController : PoolObject
    {
        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageCheckPointDestroy>(DestroyCheckPoint);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageCheckPointDestroy>(DestroyCheckPoint);
        }

        public void DestroyCheckPoint(MessageCheckPointDestroy msg)
        {
            if(gameObject == msg.CheckPointObject)
            {
                StoreToPool();
            }
        }

        public override void OnCreate()
        {
            
        }
    }
}

