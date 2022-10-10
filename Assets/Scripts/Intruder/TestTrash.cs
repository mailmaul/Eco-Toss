using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

//For debugging, delete soon
public class TestTrash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PublishSubscribe.Instance.Publish<MessageOnHitIntruder>(new MessageOnHitIntruder());
    }
}
