using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSenderExample : MonoBehaviour
{
    private void PublishMessageWithParameter()
    {
        PublishSubscribe.Instance.Publish<MessageExample>(new MessageExample("1", 10));
    }

    private void PublishMessageWithoutParameter()
    {
        PublishSubscribe.Instance.Publish<MessageExample>(new MessageExample());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PublishMessageWithParameter();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PublishMessageWithoutParameter();
        }
    }
}
