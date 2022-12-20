using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TutorialScriptableObject[] _tutorialScenario;
    [SerializeField] private Canvas _tutorialCanvas;
    [SerializeField] private RectTransform _invertMask;
    [SerializeField] private Button _helpButton;
    [SerializeField] private ModalWindowPanel _modalWindowPanel;
    [SerializeField] private UnityEvent _onContinueEvent;
    [SerializeField] private UnityEvent _onCancelEvent;
    //[SerializeField] private UnityEvent _onAlternateEvent;

    [SerializeField] private string _title;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _message;

    // Start is called before the first frame update
    void OnEnable()
    {
        Action continueCallback = null;
        Action cancelCallback = null;
        //Action alternateCallback = null;

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

        

        _modalWindowPanel.Show(_title, _sprite, _message, continueCallback, cancelCallback);

        _tutorialCanvas.gameObject.SetActive(true);
        //_modalWindowPanel.gameObject.SetActive(true);
    }
}
