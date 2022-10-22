using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.Intruder;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.IntruderSpawner
{
    public class IntruderSpawnerController : MonoBehaviour
    {
        private PoolingSystem _pool = new PoolingSystem(5);

        [SerializeField] private List<BaseIntruder> _intrudersList = new List<BaseIntruder>();

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnIntruder>(Spawn);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnIntruder>(Spawn);
        }

        //private void Update()
        //{
        //    //for testing
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        PublishSubscribe.Instance.Publish<MessageSpawnIntruder>(new MessageSpawnIntruder());
        //    }
        //}

        //publish pada progression score tertentu
        public void Spawn(MessageSpawnIntruder msg)
        {
            int randomIndex = Random.Range(0, _intrudersList.Count);
            _pool.CreateObject(_intrudersList[randomIndex], transform.position, transform);
            if(_intrudersList[randomIndex].name == "Chicken")
            {
                PublishSubscribe.Instance.Publish<MessageCheckPointSpawn>(new MessageCheckPointSpawn());
            }
        }
    }
}
