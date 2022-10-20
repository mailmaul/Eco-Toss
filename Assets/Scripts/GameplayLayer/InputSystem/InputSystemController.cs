using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.InputSystem
{
    public class InputSystemController : MonoBehaviour
    {
        private Vector2 _touchStartPosition, _touchEndPosition, _swipeDirection;

        void Update()
        {
            // if you touch the screen
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _touchStartPosition = Input.GetTouch(0).position;
            }

            // if you release your finger
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
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
}