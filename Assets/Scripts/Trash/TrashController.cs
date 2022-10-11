using Agate.MVC.Core;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.GameManager;
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
            PublishSubscribe.Instance.Subscribe<MessageStoreToPool>(StoreToPoolWithMessage);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageStoreToPool>(StoreToPoolWithMessage);
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
            _rigidbody.isKinematic = true;
            base.StoreToPool();
        }

        // Overload with MessageStoreToPool
        public override void StoreToPoolWithMessage(MessageStoreToPool message)
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.isKinematic = true;
            base.StoreToPoolWithMessage(message);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Intruder"))
            {
                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());

                if (GameManagerController.Instance.IsWindSpawn)
                {
                    PublishSubscribe.Instance.Publish<MessageSetRandomPropetiesWindArea>(new MessageSetRandomPropetiesWindArea());
                }

                // Store game object to pool and set active false
                Invoke(nameof(StoreToPool), 1);
            }
        }

        public override void OnCreate() { }
    }
}