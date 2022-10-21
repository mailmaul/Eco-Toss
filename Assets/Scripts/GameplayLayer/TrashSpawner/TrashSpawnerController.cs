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
        private PoolingSystem _trashPool = new PoolingSystem(10);

        [SerializeField] private TrashController[] _trashController;

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
            int randomIndex = Random.Range(0, _trashController.Length);
            IPoolObject createdTrash = _trashPool.CreateObject(_trashController[randomIndex], transform.position);
            PublishSubscribe.Instance.Publish<MessageSetTrashToThrow>(new MessageSetTrashToThrow(createdTrash.gameObject.GetComponent<Rigidbody>()));

            // Debugging purposes
            //IPoolObject createdTrash = _trashPool.CreateObject(_trashController[0], transform.position);
            //createdTrash.transform.position = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y, transform.position.z);
        }
    }
}