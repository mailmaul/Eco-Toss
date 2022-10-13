using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageReceiverExample : MonoBehaviour
{
    private void Awake()
    {
        PublishSubscribe.Instance.Subscribe<MessageExample>(OnMessageReceived);
    }
    private void OnDestroy()
    {
        PublishSubscribe.Instance.Unsubscribe<MessageExample>(OnMessageReceived);
    }
    public void OnMessageReceived(MessageExample message)
    {
        Debug.Log("Pesan : " + message.IDMessage + "|" + message.BodyMessage);
    }
}
