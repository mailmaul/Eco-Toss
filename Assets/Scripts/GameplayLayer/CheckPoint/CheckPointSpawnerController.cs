using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.TrashCan;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.IntruderSpawner;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.CheckPoint
{
    public class CheckPointSpawnerController : MonoBehaviour
    {
        [SerializeField] private Transform _intruderSpawner;
        [SerializeField] private TrashCanController[] _trashCanList;
        [SerializeField] private CheckPointController _checkPoint;
        [SerializeField] private float offset;

        private PoolingSystem _pool = new PoolingSystem(5);

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageCheckPointSpawn>(CheckPointSpawn);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageCheckPointSpawn>(CheckPointSpawn);
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            transform.position = new Vector3(transform.position.x, _intruderSpawner.position.y, _intruderSpawner.position.z);
        }

        public void CheckPointSpawn(MessageCheckPointSpawn msg)
        {
            Debug.Log("Masuk");
            int index = Random.Range(0, _trashCanList.Length);
            Vector3 pos = new Vector3(_trashCanList[index].transform.position.x + offset, transform.position.y, transform.position.z);
            _pool.CreateObject(_checkPoint, pos, transform);
            Debug.Log("Spawn checkpoint");
        }
    }

}
