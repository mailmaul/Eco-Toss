using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.GameManager;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.Health
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private int _health;

        [SerializeField] private TMP_Text _healthText;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageDecreaseHealth>(DecreaseHealth);
            PublishSubscribe.Instance.Subscribe<MessageIncreaseHealth>(IncreaseHealth);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageDecreaseHealth>(DecreaseHealth);
            PublishSubscribe.Instance.Unsubscribe<MessageIncreaseHealth>(IncreaseHealth);
        }

        private void Update()
        {
            if (!GameManagerController.Instance.IsGameOver)
            {
                SetUI();
                EmptyHealth();
            }
        }

        //publish ketika sampah tidak masuk ke tong sampah
        public void DecreaseHealth(MessageDecreaseHealth msg)
        {
            _health--;
        }

        //publish ketika dapat buff add health
        public void IncreaseHealth(MessageIncreaseHealth msg)
        {
            _health += msg.AdditionalHealth;
        }

        public void EmptyHealth()
        {
            if(_health <= 0)
            {
                PublishSubscribe.Instance.Publish<MessageGameOver>(new MessageGameOver(true));
            }
        }

        private void SetUI()
        {
            _healthText.SetText("Health : " + _health);
        }
    }
}

