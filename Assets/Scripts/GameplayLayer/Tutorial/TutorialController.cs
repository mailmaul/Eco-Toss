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
        [SerializeField] private bool _nextTutorialHasSameMask;
        [SerializeField] private bool _prevTutorialHasSameMask;
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

        private void Awake()
        {
            _onContinueEvent.AddListener(TurnOffNext);
            _onCancelEvent.AddListener(TurnOffPrev);
        }

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
                continueCallback = _onContinueEvent.Invoke;
            }
            if (_onCancelEvent.GetPersistentEventCount() > 0)
            {
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
                    //if (_invertMask == null)
                    //{
                    //    break;
                    //}
                    //_invertMask.gameObject.SetActive(false);
                    //Debug.Log("on enable " + gameObject.name + " " + _normalMask.gameObject.name + ":" + _normalMask.gameObject.activeInHierarchy);
                    break;

                case MaskEnum.InvertMask:
                    //_normalMask.gameObject.SetActive(false);
                    _invertMask.gameObject.SetActive(true);
                    //Debug.Log("on enable " + gameObject.name + " " + _invertMask.gameObject.name + ":" + _invertMask.gameObject.activeInHierarchy);
                    break;

                case MaskEnum.None:
                    //_normalMask.gameObject.SetActive(false);
                    //if (_invertMask == null)
                    //{
                    //    break;
                    //}
                    //_invertMask.gameObject.SetActive(false);
                    break;
            }
        }

        void TurnOffNext()
        {
            gameObject.SetActive(false);

            if (_applyMask == MaskEnum.InvertMask && _invertMask != null && !_nextTutorialHasSameMask /*&& _invertMask.gameObject.activeInHierarchy*/) { _invertMask.gameObject.SetActive(false); }
            if (_applyMask == MaskEnum.NormalMask && _normalMask != null && !_nextTutorialHasSameMask /*&& _normalMask.gameObject.activeInHierarchy*/) { _normalMask.gameObject.SetActive(false); }

            //Debug.Log("TurnOffNext " + gameObject.name + " " + _normalMask.gameObject.name + ":" + _normalMask.gameObject.activeInHierarchy);
            //Debug.Log("TurnOffNext " + gameObject.name + " " + _invertMask.gameObject.name + ":" + _invertMask.gameObject.activeInHierarchy);
        }

        void TurnOffPrev()
        {
            gameObject.SetActive(false);

            if (_applyMask == MaskEnum.InvertMask && _invertMask != null && !_prevTutorialHasSameMask /*&& _invertMask.gameObject.activeInHierarchy*/) { _invertMask.gameObject.SetActive(false); }
            if (_applyMask == MaskEnum.NormalMask && _normalMask != null && !_prevTutorialHasSameMask /*&& _normalMask.gameObject.activeInHierarchy*/) { _normalMask.gameObject.SetActive(false); }

            //Debug.Log("TurnOffPrev " + gameObject.name + " " + _normalMask.gameObject.name + ":" + _normalMask.gameObject.activeInHierarchy);
            //Debug.Log("TurnOffPrev " + gameObject.name + " " + _invertMask.gameObject.name + ":" + _invertMask.gameObject.activeInHierarchy);
        }
    }
}