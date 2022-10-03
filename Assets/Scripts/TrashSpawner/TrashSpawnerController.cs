using Agate.MVC.Core;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.Trash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.TrashSpawner
{
    public class TrashSpawnerController : MonoBehaviour
    {
        private PoolingSystem _trashPool = new();

        [SerializeField] private TrashController _trashController;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageTrashSpawn>(MessageTrashSpawnReceived);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageTrashSpawn>(MessageTrashSpawnReceived);
        }

        private void Start()
        {
            PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
        }

        private void MessageTrashSpawnReceived(MessageTrashSpawn message)
        {
            IPoolObject createdTrash = _trashPool.CreateObject(_trashController, transform.position, Quaternion.identity);
        }
    }
}