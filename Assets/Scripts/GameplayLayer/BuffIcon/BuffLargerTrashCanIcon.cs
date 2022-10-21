using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

public class BuffLargerTrashCanIcon : BaseBuffIcon
{
    private int _count;

    private void Awake()
    {
        PublishSubscribe.Instance.Subscribe<MessageIncreaseCountBuff>(AddCount);
    }

    private void OnDestroy()
    {
        PublishSubscribe.Instance.Subscribe<MessageIncreaseCountBuff>(AddCount);
    }

    protected override void Start()
    {
        base.Start();
        PublishSubscribe.Instance.Publish<MessageIncreaseCountBuff>(new MessageIncreaseCountBuff());
    }

    public void AddCount(MessageIncreaseCountBuff msg)
    {
        _count++;
        _text.SetText("x" + _count);
    }
}