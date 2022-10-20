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
        private bool _hasCollided = false;
        private float _stayDuration = 0f;
        private float _stayMaxDuration = 1.5f;

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
            if (Debug.isDebugBuild)
            {
                Debug.Log(transform.name + "nabrak" + collision.transform.name);
            }

            if ((collision.gameObject.CompareTag("Ground") ||
                collision.gameObject.tag.Substring(0, 8) == "TrashCan") && _hasCollided == false)
            {       
                _hasCollided = true;
                Debug.Log("has collided= " + _hasCollided);
                if (collision.gameObject.CompareTag("Ground"))
                {
                    PublishSubscribe.Instance.Publish<MessageDecreaseHealth>(new MessageDecreaseHealth());
                    Invoke(nameof(StoreToPool), 0.5f);
                }
                else if (collision.gameObject.tag.Substring(0, 8) == "TrashCan" || collision.gameObject.CompareTag("Intruder"))
                {
                    StoreToPool();
                }

                if (GameManagerController.Instance.IsWindSpawn)
                {
                    PublishSubscribe.Instance.Publish<MessageSetRandomPropertiesWindArea>(new MessageSetRandomPropertiesWindArea());
                }

                //Debug.Log("spawn");
                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (_stayDuration < _stayMaxDuration)
            {
                _stayDuration += Time.fixedDeltaTime;
            }
            else
            {
                StoreToPool();
                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
                _stayDuration = 0;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (Debug.isDebugBuild)
            {
                Debug.Log(transform.name + "nabrak" + other.transform.name);
            }

            if (other.gameObject.CompareTag("Intruder") && _hasCollided == false)
            {       
                _hasCollided = true;
                Debug.Log("has collided= " + _hasCollided);
                    StoreToPool();

                if (GameManagerController.Instance.IsWindSpawn)
                {
                    PublishSubscribe.Instance.Publish<MessageSetRandomPropertiesWindArea>(new MessageSetRandomPropertiesWindArea());
                }

                //Debug.Log("spawn");
                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
            }
        }
    }
}