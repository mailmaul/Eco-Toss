using Agate.MVC.Core;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.Trash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.GameManager
{
    public class GameManagerController : MonoBehaviour
    {
        #region Singleton
        public static GameManagerController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        #endregion

        private void OnEnable()
        {
            PublishSubscribe.Instance.Subscribe<MessageGameOver>(OnGameOver);
        }

        private void OnDisable()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageGameOver>(OnGameOver);
        }

        public bool IsWindSpawn { get; private set; }
        public bool IsGameOver { get; private set; }

        void Start()
        {
            IsWindSpawn = false;
            PublishSubscribe.Instance.Publish<MessageGameOver>(new MessageGameOver(false));
        }

        public void OnGameOver(MessageGameOver msg)
        {
            IsGameOver = msg.IsGameOver;
        }

        public void OnWindSpawn(bool msg)
        {
            IsWindSpawn = msg;
        }
    }
}

