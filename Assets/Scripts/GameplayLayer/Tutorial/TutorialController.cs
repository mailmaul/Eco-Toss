using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EcoTeam.EcoToss.Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        enum MaskEnum
        {
            None,
            NormalMask,
            InvertMask,
        }

        [SerializeField] private Canvas _tutorialCanvas;
        [SerializeField] private RectTransform _normalMask;
        [SerializeField] private RectTransform _invertMask;
        [SerializeField] private MaskEnum _applyMask;
        [SerializeField] private bool _applyPause;
        [SerializeField] private ModalWindowPanel _modalWindowPanel;
        [SerializeField] private UnityEvent _onContinueEvent;
        [SerializeField] private UnityEvent _onCancelEvent;
        //[SerializeField] private UnityEvent _onAlternateEvent;

        [SerializeField] private TextAnchor _modalWindowAlignment;
        [SerializeField] private string _title;
        [SerializeField] private Sprite _sprite;
        [SerializeField][TextArea(10, 15)] private string _message;
        [SerializeField] private string _confirmButtonText;
        [SerializeField] private string _declineButtonText;
        //[SerializeField] private string _alternateButtonText;

        void OnEnable()
        {
            Action continueCallback = null;
            Action cancelCallback = null;
            //Action alternateCallback = null;

            if (_applyPause)
            {
                Time.timeScale = 0;
            }

            if (_onContinueEvent.GetPersistentEventCount() > 0)
            {
                gameObject.SetActive(false);
                continueCallback = _onContinueEvent.Invoke;
            }
            if (_onCancelEvent.GetPersistentEventCount() > 0)
            {
                gameObject.SetActive(false);
                cancelCallback = _onCancelEvent.Invoke;
            }
            //if (_onAlternateEvent.GetPersistentEventCount() > 0)
            //{
            //    alternateCallback = _onAlternateEvent.Invoke;
            //}

            _modalWindowPanel.Show(_modalWindowAlignment, _title, _sprite, _message, _confirmButtonText, _declineButtonText, continueCallback, cancelCallback);

            _tutorialCanvas.gameObject.SetActive(true);

            switch (_applyMask)
            {
                case MaskEnum.NormalMask:
                    _normalMask.gameObject.SetActive(true);
                    _invertMask.gameObject.SetActive(false);
                    break;

                case MaskEnum.InvertMask:
                    _normalMask.gameObject.SetActive(false);
                    _invertMask.gameObject.SetActive(true);
                    break;

                case MaskEnum.None:
                    _normalMask.gameObject.SetActive(false);
                    _invertMask.gameObject.SetActive(false);
                    break;
            }
        }
    }
}