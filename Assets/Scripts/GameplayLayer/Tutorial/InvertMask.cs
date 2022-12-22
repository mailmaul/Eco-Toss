using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertMask : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }

    public void SetPositionAndScale()
    {
        //Adjust Invert Mask as targeted object to highlight
    }
}
