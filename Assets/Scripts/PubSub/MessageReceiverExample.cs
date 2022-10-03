using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageReceiverExample : MonoBehaviour
{
    private void Awake()
    {
        PublishSubscribe.Instance.Subscribe<ExampleMessage>(OnMessageReceived);
    }
    private void OnDestroy()
    {
        PublishSubscribe.Instance.Unsubscribe<ExampleMessage>(OnMessageReceived);
    }
    public void OnMessageReceived(ExampleMessage message)
    {
        Debug.Log("Pesan : " + message.IDMessage + "|" + message.BodyMessage);
    }
}
