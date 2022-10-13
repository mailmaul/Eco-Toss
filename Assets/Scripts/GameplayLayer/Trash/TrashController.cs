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
        private Quaternion _defaultRotation;
        private Rigidbody _rigidbody;
        private bool _hasCollided;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _defaultRotation = transform.rotation;
        }

        public override void StoreToPool()
        {
            transform.SetPositionAndRotation(transform.position, _defaultRotation);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.isKinematic = true;
            _hasCollided = false;
            base.StoreToPool();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(transform.name + "nabrak" + collision.transform.name);

            if (collision.gameObject.CompareTag("Ground") ||
                collision.gameObject.CompareTag("Intruder") ||
                collision.gameObject.tag.Substring(0, 8) == "TrashCan")
            {
                if (_hasCollided == false)
                {
                    _hasCollided = true;

                    if (collision.gameObject.CompareTag("Ground") ||
                        collision.gameObject.CompareTag("Intruder"))
                    {
                        PublishSubscribe.Instance.Publish<MessageDecreaseHealth>(new MessageDecreaseHealth());
                        Invoke(nameof(StoreToPool), 0.5f);
                    }
                    // Store game object to pool and set active false
                    else if (collision.gameObject.tag.Substring(0, 8) == "TrashCan")
                    {
                        StoreToPool();
                    }

                    if (GameManagerController.Instance.IsWindSpawn)
                    {
                        PublishSubscribe.Instance.Publish<MessageSetRandomPropetiesWindArea>(new MessageSetRandomPropetiesWindArea());
                    }

                    Debug.Log("spawn");
                    PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
                }
            }
        }
    }
}