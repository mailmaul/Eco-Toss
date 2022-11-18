using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using UnityEngine;

//For debugging, delete soon
public class TestTrash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Intruder"))
        {
            PublishSubscribe.Instance.Publish<MessageOnHitIntruder>(new MessageOnHitIntruder());
        }
    }
}
