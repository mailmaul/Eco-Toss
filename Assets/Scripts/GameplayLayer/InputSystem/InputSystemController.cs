using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EcoTeam.EcoToss.InputSystem
{
    public class InputSystemController : MonoBehaviour
    {
        private Vector2 _touchStartPosition, _touchEndPosition, _swipeDirection;
        private bool _fingerDown;

        void Update()
        {
            // Detect if there is touch input
            if (_fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(0))
            {
                _touchStartPosition = Input.touches[0].position;
                _fingerDown = true;
            }

            // If you hold your finger
            if (_fingerDown && Time.timeScale == 1)
            {
                OnTouchHold();

                // Detect if player is removing their touch input
                if (Input.GetTouch(0).phase == TouchPhase.Canceled || Time.timeScale == 0)
                {
                    return;
                }
                else if(Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    OnRelease();
                }
            }
        }

        private void CalculateFingerPosition()
        {
            // getting release finger position
            _touchEndPosition = Input.GetTouch(0).position;

            // calculating swipe direction in 2D space
            _swipeDirection = _touchStartPosition - _touchEndPosition;
        }

        private void OnTouchHold()
        {
            CalculateFingerPosition();
            PublishSubscribe.Instance.Publish<MessageSimulateThrowing>(new MessageSimulateThrowing(_swipeDirection));
        }

        private void OnRelease()
        {
            PublishSubscribe.Instance.Publish<MessageTrashThrowing>(new MessageTrashThrowing(_swipeDirection));
            _fingerDown = false;
        }
    }
}