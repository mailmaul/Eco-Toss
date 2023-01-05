using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using UnityEngine;

namespace EcoTeam.EcoToss.TrashThrowing
{
    public class TrashThrowingController : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;
        [SerializeField] Rigidbody _simulatedRigidbody;
        [SerializeField] float _throwForceInX = 0.4f; // to control throw force in X directions
        [SerializeField] float _throwForceInY = 375f; // to control throw force in Y directions
        [SerializeField] float _throwForceInZ = 575f; // to control throw force in Z direction

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSetTrashToThrow>(SetRigidbody);
            PublishSubscribe.Instance.Subscribe<MessageTrashThrowing>(ThrowTrash);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSetTrashToThrow>(SetRigidbody);
            PublishSubscribe.Instance.Unsubscribe<MessageTrashThrowing>(ThrowTrash);
        }

        // add force to balls rigidbody in 3D space depending on swipe direction and throw forces
        void ThrowTrash(MessageTrashThrowing message)
        {
            if (_rigidbody != null)
            {
                _rigidbody.isKinematic = false;

                Vector3 force = new Vector3
                    (
                        -message.SwipeDirection.x * _throwForceInX,
                        _throwForceInY,
                        _throwForceInZ
                    );

                _rigidbody.AddForce(force);
                _rigidbody = null;
                PublishSubscribe.Instance.Publish<MessageDeleteTrajectory>(new MessageDeleteTrajectory());
            }
        }

        public void SimulateThrowTrash(Vector2 swipeDirectionSimulation, Rigidbody rigidbody)
        {
            if (_rigidbody != null)
            {
                _simulatedRigidbody = rigidbody;

                if (_simulatedRigidbody != null)
                {
                    _simulatedRigidbody.isKinematic = false;

                    Vector3 force = new Vector3
                        (
                            -swipeDirectionSimulation.x * _throwForceInX,
                            _throwForceInY,
                            _throwForceInZ
                        );

                    _simulatedRigidbody.AddForce(force);
                    _simulatedRigidbody = null;
                }
            }
        }

        private void SetRigidbody(MessageSetTrashToThrow message)
        {
            _rigidbody = message.TrashToThrow.GetComponent<Rigidbody>();
        }
    }
}