using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TutorialScenario", menuName = "Tutorial")]
[System.Serializable]
public class TutorialScriptableObject : ScriptableObject
{
    [field: SerializeField] public int ID { private set; get; }
    [field: SerializeField] public bool IsVertical { private set; get; }
    [field: SerializeField] public string Title { private set; get; }
    [field: SerializeField] public Sprite ImageToShow { private set; get; }
    [field: SerializeField] public string Message { private set; get; }
    [field: SerializeField] public string ConfirmMessage { private set; get; }
    [field: SerializeField] public string DeclineMessage { private set; get; }
    [field: SerializeField] public string AlternateMessage { private set; get; }

    [field: SerializeField] public UnityEvent ConfirmAction { private set; get; }
    [field: SerializeField] public UnityEvent DeclineAction { private set; get; }
    [field: SerializeField] public UnityEvent AlternateAction { private set; get; } = null;

    [field: SerializeField] public int InvertMaskWidth { private set; get; }
    [field: SerializeField] public int InvertMaskHeight { private set; get; }
    [field: SerializeField] public int InvertMaskXPosition { private set; get; }
    [field: SerializeField] public int InvertMaskYPosition { private set; get; }
}
