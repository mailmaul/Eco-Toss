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
            if (collision.gameObject.CompareTag("Ground") ||
                collision.gameObject.CompareTag("Intruder") ||
                collision.gameObject.tag.Substring(8) == "TrashCan")
            {
                if (collision.gameObject.CompareTag("Ground"))
                {
                    PublishSubscribe.Instance.Publish<MessageDecreaseHealth>(new MessageDecreaseHealth());
                }

                if (GameManagerController.Instance.IsWindSpawn)
                {
                    PublishSubscribe.Instance.Publish<MessageSetRandomPropetiesWindArea>(new MessageSetRandomPropetiesWindArea());
                }

                // Store game object to pool and set active false
                Invoke(nameof(StoreToPool), 0.5f);
                //StoreToPool();

                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
            }
        }
    }
}