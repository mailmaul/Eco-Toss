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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Intruder"))
            {
                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());

                // Store game object to pool and set active false
                Invoke("StoreToPool", 3);
            }
        }

        public override void OnCreate()
        {

        }
    }
}