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
        private bool _isGameOver = false;

        void Start()
        {
            IsWindSpawn = false;
        }

        public void OnGameOver(MessageGameOver msg)
        {
            _isGameOver = msg.IsGameOver;
            Time.timeScale = 0;
        }

        public void OnWindSpawn(bool msg)
        {
            IsWindSpawn = msg;
        }
    }
}

