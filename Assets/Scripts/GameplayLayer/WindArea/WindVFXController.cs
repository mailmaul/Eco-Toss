using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections.Generic;
using UnityEngine;

public class WindVFXController : MonoBehaviour
{
    [System.Serializable]
    public struct WindVFXObject
    {
        public GameObject wind;
        public Vector3 position;
    }

    [SerializeField] private List<WindVFXObject> _windVFXList = new List<WindVFXObject>();
    [SerializeField] private GameObject[] _windVFXInHierarchy = new GameObject[6];

    //nilai disesuaikan dengan _windStrengthList pada script WindAreaController.cs
    private const float _slowWindStregth = 2f;
    private const float _fastWindStrength = 5f;

    private void Awake()
    {
        PublishSubscribe.Instance.Subscribe<MessageShowWindProperties>(ShowWindVFX);
    }

    private void OnDestroy()
    {
        PublishSubscribe.Instance.Unsubscribe<MessageShowWindProperties>(ShowWindVFX);
    }

    private void Start()
    {
        InstantiateWindVFX();
    }

    private void InstantiateWindVFX()
    {
        for (int i = 0; i < _windVFXList.Count; i++)
        {
            GameObject windVFX = Instantiate(_windVFXList[i].wind, _windVFXList[i].position, _windVFXList[i].wind.transform.rotation);
            windVFX.transform.SetParent(transform);
            _windVFXInHierarchy[i] = windVFX;
            _windVFXInHierarchy[i].SetActive(false);
        }
    }
    private void HideWindVFX()
    {
        for (int i = 0; i < _windVFXInHierarchy.Length; i++)
        {
            if (_windVFXInHierarchy[i].activeInHierarchy)
            {
                _windVFXInHierarchy[i].SetActive(false);
                return;
            }
        }
    }

    public void ShowWindVFX(MessageShowWindProperties msg)
    {
        HideWindVFX();

        if (msg.Strength == _slowWindStregth)
        {
            if(msg.Direction == "Right")
            {
                if (!_windVFXInHierarchy[0].activeInHierarchy)
                {
                    _windVFXInHierarchy[0].SetActive(true);
                }
            } 
            else if(msg.Direction == "Left")
            {
                if (!_windVFXInHierarchy[2].activeInHierarchy)
                {
                    _windVFXInHierarchy[2].SetActive(true);
                }
            }
        }
        else
        {
            if (msg.Direction == "Right")
            {
                if (!_windVFXInHierarchy[1].activeInHierarchy)
                {
                    _windVFXInHierarchy[1].SetActive(true);
                }
            }
            else if (msg.Direction == "Left")
            {
                if (!_windVFXInHierarchy[3].activeInHierarchy)
                {
                    _windVFXInHierarchy[3].SetActive(true);
                }
            }
        }
    }
}
