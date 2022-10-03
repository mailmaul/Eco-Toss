using Agate.MVC.Core;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Trash
{
    public class TrashController : PoolObject
    {
        private void OnCollisionEnter(Collision collision)
        {
            PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());

            // Store game object to pool and set active false
            StoreToPool();
        }

        public override void OnCreate()
        {

        }
    }
}