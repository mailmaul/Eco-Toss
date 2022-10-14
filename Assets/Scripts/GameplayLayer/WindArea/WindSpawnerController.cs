using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.GameManager;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.WindArea
{
    public class WindSpawnerController : MonoBehaviour
    {
        [SerializeField] private WindAreaController prefab;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnWindArea>(Spawn);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnWindArea>(Spawn);
        }

        private void Start()
        {
            prefab = Resources.Load<WindAreaController>("Prefabs/WindArea/WindArea");
        }

        private void Update()
        {
            //for testing, delete soon
            if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.Space))
            {
                Spawn(new MessageSpawnWindArea());
            }
        }

        //publish di progression tertentu
        public void Spawn(MessageSpawnWindArea msg)
        {
            Instantiate(prefab, transform);
            GameManagerController.Instance.OnWindSpawn(true);
            PublishSubscribe.Instance.Publish<MessageSetRandomPropetiesWindArea>(new MessageSetRandomPropetiesWindArea());
        }

    }

}
