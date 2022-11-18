using Agate.MVC.Core;
using EcoTeam.EcoToss.GameManager;
using EcoTeam.EcoToss.PubSub;
using UnityEngine;

namespace EcoTeam.EcoToss.WindArea
{
    public class WindSpawnerController : MonoBehaviour
    {
        [SerializeField] private WindAreaController _windAreaPrefab;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnWindArea>(Spawn);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnWindArea>(Spawn);
        }

        // Debug purposes
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        Spawn(new MessageSpawnWindArea());
        //    }
        //}

        //publish di progression tertentu
        public void Spawn(MessageSpawnWindArea msg)
        {
            Instantiate(_windAreaPrefab, transform);
            GameManagerController.Instance.OnWindSpawn(true);
            PublishSubscribe.Instance.Publish<MessageSetRandomPropertiesWindArea>(new MessageSetRandomPropertiesWindArea());
        }
    }
}
