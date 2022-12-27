using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.SaveData;
using EcoTeam.EcoToss.Tutorial;
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

                // Tutorial throw trash start
                if (!SaveDataController.Instance.SaveData.HasDoneTutorial)
                {
                    if (!TutorialValidator.Instance.HasTapped)
                    {
                        TutorialValidator.Instance.SetHasTapped(true);
                        TutorialValidator.Instance.SetActiveTutorialPrepareToThrow(false);
                        TutorialValidator.Instance.SetActiveTutorialThrowTrash(true);
                    }
                }
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
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
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

            // Tutorial throw trash end
            if (!SaveDataController.Instance.SaveData.HasDoneTutorial)
            {
                if (!TutorialValidator.Instance.HasThrownTrash)
                {
                    TutorialValidator.Instance.SetHasThrownTrash(true);
                    TutorialValidator.Instance.SetActiveTutorialThrowTrash(false);
                }
            }
        }
    }
}