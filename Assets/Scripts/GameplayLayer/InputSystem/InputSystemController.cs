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
        [SerializeField] private int _pixelToDetect = 50;

        void Update()
        {
            // Detect if there is touch input
            if (_fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(0))
            {
                _touchStartPosition = Input.touches[0].position;
                _fingerDown = true;
            }

            if (_fingerDown && Time.timeScale == 1)
            {
                // Detect if player is swiping
                if (Input.touches[0].position.x <= _touchStartPosition.x - _pixelToDetect ||
                Input.touches[0].position.x >= _touchStartPosition.x + _pixelToDetect ||
                Input.touches[0].position.y >= _touchStartPosition.y + _pixelToDetect)
                {
                    _fingerDown = false;
                    OnSwipe();
                    if (Debug.isDebugBuild)
                    {
                        Debug.Log("Swipe");
                    }
                }

                // Detect if player is removing their touch input
                if (Input.GetTouch(0).phase == TouchPhase.Ended ||
                Input.GetTouch(0).phase == TouchPhase.Canceled ||
                Input.touches[0].position.y <= _touchStartPosition.y - _pixelToDetect ||
                Time.timeScale == 0)
                {
                    _fingerDown = false;
                }
            }
        }

        private void OnSwipe()
        {
            // getting release finger position
            _touchEndPosition = Input.GetTouch(0).position;

            // calculating swipe direction in 2D space
            _swipeDirection = _touchStartPosition - _touchEndPosition;
            _swipeDirection = Vector3.Normalize(_swipeDirection);
            
            PublishSubscribe.Instance.Publish<MessageTrashThrowing>(new MessageTrashThrowing(_swipeDirection));
        }
    }
}