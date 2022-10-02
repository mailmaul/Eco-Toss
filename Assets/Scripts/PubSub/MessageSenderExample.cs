using Agate.MVC.Core;
using Kelompok6.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSenderExample : MonoBehaviour
{
    private void PublishMessageWithParameter()
    {
        PublishSubscribe.Instance.Publish<ExampleMessage>(new ExampleMessage("1", 10));
    }

    private void PublishMessageWithoutParameter()
    {
        PublishSubscribe.Instance.Publish<ExampleMessage>(new ExampleMessage());
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
