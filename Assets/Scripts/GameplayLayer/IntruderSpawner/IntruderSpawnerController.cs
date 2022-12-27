using Agate.MVC.Core;
using EcoTeam.EcoToss.Intruder;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.SaveData;
using EcoTeam.EcoToss.Tutorial;
using System.Collections.Generic;
using UnityEngine;

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

        //// Debug purposes
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        PublishSubscribe.Instance.Publish<MessageSpawnIntruder>(new MessageSpawnIntruder());
        //    }
        //}

        // Publish pada progression score tertentu
        public void Spawn(MessageSpawnIntruder msg)
        {
            int randomIndex = Random.Range(0, _intrudersList.Count);
            string intruderName = _intrudersList[randomIndex].name;

            _pool.CreateObject(_intrudersList[randomIndex], transform.position, transform);

            if (intruderName == "Scramble")
            {
                PublishSubscribe.Instance.Publish<MessageCheckPointSpawn>(new MessageCheckPointSpawn());
            }

            // Tutorial Intruder
            if (!SaveDataController.Instance.SaveData.HasDoneTutorial)
            {
                if (_intrudersList[randomIndex].name == "Patrol")
                {
                    if (!TutorialValidator.Instance.HasSpawnedIntruder1)
                    {
                        TutorialValidator.Instance.SetHasSpawnedIntruder1(true);
                        TutorialValidator.Instance.SetActiveTutorialIntruder(intruderName, true);
                    }
                }
                else if (_intrudersList[randomIndex].name == "Scramble")
                {
                    if (!TutorialValidator.Instance.HasSpawnedIntruder2)
                    {
                        TutorialValidator.Instance.SetHasSpawnedIntruder2(true);
                        TutorialValidator.Instance.SetActiveTutorialIntruder(intruderName, true);
                    }
                }
            }
        }
    }
}
