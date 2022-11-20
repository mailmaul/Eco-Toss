using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections.Generic;
using UnityEngine;

public class WindVFXController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _windVFXList = new List<GameObject>();
    [SerializeField] private GameObject[] _windVFXInHierarchy = new GameObject[3];

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
            GameObject windVFX = Instantiate(_windVFXList[i], transform);
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

        if (msg.Strength < 1.5f)
        {
            if (!_windVFXInHierarchy[0].activeInHierarchy)
            {
                _windVFXInHierarchy[0].gameObject.SetActive(true);
            }
        }
        else if (msg.Strength < 3f)
        {
            if (!_windVFXInHierarchy[1].activeInHierarchy)
            {
                _windVFXInHierarchy[1].gameObject.SetActive(true);
            }
        }
        else
        {
            if (!_windVFXInHierarchy[2].activeInHierarchy)
            {
                _windVFXInHierarchy[2].gameObject.SetActive(true);
            }
        }
    }
}
