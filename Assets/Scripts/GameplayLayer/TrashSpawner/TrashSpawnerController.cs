using Agate.MVC.Core;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.Trash;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EcoTeam.EcoToss.TrashSpawner
{
    public class TrashSpawnerController : MonoBehaviour
    {
        private PoolingSystem _trashPool = new PoolingSystem(10);

        [SerializeField] private TrashController[] _trashController;
        private bool _hasClearedTrashPool;

        private void Awake()
        {
            // Spawn all type of trash if not tutorial, spawn apple only when in tutorial
            if (SceneManager.GetActiveScene().buildIndex != 2) { _hasClearedTrashPool = true; }

            PublishSubscribe.Instance.Subscribe<MessageTrashSpawn>(MessageTrashSpawnReceived);
            PublishSubscribe.Instance.Subscribe<MessageClearTrashSpawnerPool>(ClearTrashSpawnerPool);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageTrashSpawn>(MessageTrashSpawnReceived);
            PublishSubscribe.Instance.Unsubscribe<MessageClearTrashSpawnerPool>(ClearTrashSpawnerPool);
        }

        private void Start()
        {
            PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
        }

        private void MessageTrashSpawnReceived(MessageTrashSpawn message)
        {
            int randomIndex = Random.Range(0, _trashController.Length);
            IPoolObject createdTrash;

            if (!_hasClearedTrashPool)
            {
                createdTrash = _trashPool.CreateObject(_trashController[0], transform.position);
            }
            else
            {
                createdTrash = _trashPool.CreateObject(_trashController[randomIndex], transform.position);
            }

            PublishSubscribe.Instance.Publish<MessageSetTrashToThrow>(new MessageSetTrashToThrow(createdTrash.gameObject.GetComponent<Rigidbody>()));

            // Debugging purposes
            //IPoolObject createdTrash = _trashPool.CreateObject(_trashController[0], transform.position);
            //createdTrash.transform.position = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y, transform.position.z);

        }

        private void ClearTrashSpawnerPool(MessageClearTrashSpawnerPool message)
        {
            _trashPool.Clear();
            _hasClearedTrashPool = true;
        }
    }
}