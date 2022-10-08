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
        private Rigidbody _rigidbody;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageStoreToPool>(StoreToPool);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageStoreToPool>(StoreToPool);
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void StoreToPool()
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            base.StoreToPool();
        }

        // Overload with MessageStoreToPool
        public override void StoreToPool(MessageStoreToPool message)
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            base.StoreToPool();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Intruder"))
            {
                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());

                // Store game object to pool and set active false
                Invoke(nameof(StoreToPool), 1);
            }
        }

        public override void OnCreate()
        {
            
        }
    }
}