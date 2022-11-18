using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
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
        public bool IsGameOver = false;
        [SerializeField] private GameObject _gameOverPanel;

        void Start()
        {
            IsWindSpawn = false;
            Time.timeScale = 1;
        }

        public void OnGameOver(MessageGameOver msg)
        {
            IsGameOver = msg.IsGameOver;
            Time.timeScale = 0;
            _gameOverPanel.SetActive(true);
        }

        public void OnWindSpawn(bool msg)
        {
            IsWindSpawn = msg;
        }
    }
}

