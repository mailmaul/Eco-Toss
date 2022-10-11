using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.TrashThrowing
{
    public class TrashThrowingController : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;
        [SerializeField] float _throwForceInX = 1f; // to control throw force in X directions
        [SerializeField] float _throwForceInY = 1f; // to control throw force in Y directions
        [SerializeField] float _throwForceInZ = 425f; // to control throw force in Z direction

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageTrashThrowing>(ThrowTrash);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageTrashThrowing>(ThrowTrash);
        }

        // add force to balls rigidbody in 3D space depending on swipe direction and throw forces
        void ThrowTrash(MessageTrashThrowing message)
        {
            _rigidbody = message.TrashRigidbody;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(
                -message.SwipeDirection.x * _throwForceInX,
                -message.SwipeDirection.y * _throwForceInY,
                _throwForceInZ
                );
        }
    }
}