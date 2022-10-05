using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.InputSystem
{
    public class InputSystemController : MonoBehaviour
    {
        private Vector3 _touchRayPosition; // touch raycast
        private Vector2 _touchStartPosition, _touchEndPosition, _swipeDirection;

        [SerializeField] Rigidbody _rigidbody;

        // Update is called once per frame
        void Update()
        {
            // if you touch the screen
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _touchRayPosition = Input.GetTouch(0).position;
                _touchRayPosition.z = 100f;
                _touchRayPosition = Camera.main.ScreenToWorldPoint(_touchRayPosition);

                _touchStartPosition = Input.GetTouch(0).position;
                Raycast();
            }

            // if you release your finger
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                // getting release finger position
                _touchEndPosition = Input.GetTouch(0).position;

                // calculating swipe direction in 2D space
                _swipeDirection = _touchStartPosition - _touchEndPosition;

                PublishSubscribe.Instance.Publish<MessageTrashThrowing>(new MessageTrashThrowing(_rigidbody, _swipeDirection));
            }
        }

        private void Raycast()
        {
            RaycastHit hit;

            Ray ray = new Ray(transform.position, _touchRayPosition - transform.position);
            if (Debug.isDebugBuild)
            {
                Debug.DrawRay(transform.position, _touchRayPosition - transform.position, Color.red, 3f);
            }

            bool raycastIsHit = Physics.Raycast(ray, out hit);

            if (raycastIsHit)
            {
                if (hit.collider != null && hit.rigidbody.isKinematic == true)
                {
                    _rigidbody = hit.rigidbody;
                }
            }
        }
    }
}