using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.Health
{
    public class HealthController : MonoBehaviour
    {
        private int _health;

        [SerializeField] private TMP_Text _healthText;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageDecreaseHealth>(DecreaseHealth);
            PublishSubscribe.Instance.Subscribe<MessageIncraeseHealth>(IncreaseHealth);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageDecreaseHealth>(DecreaseHealth);
            PublishSubscribe.Instance.Unsubscribe<MessageIncraeseHealth>(IncreaseHealth);
        }

        private void Start()
        {
            _health = 3;
        }

        private void Update()
        {
            //wrap ke dalam if(!GameManager.Instance.IsGameOver){}
            SetUI();
            EmptyHealth();
        }

        //publish ketika sampah tidak masuk ke tong sampah
        public void DecreaseHealth(MessageDecreaseHealth msg)
        {
            _health--;
        }

        //publish ketika dapat buff add health
        public void IncreaseHealth(MessageIncraeseHealth msg)
        {
            _health += msg.AdditionalHealth;
        }

        public void EmptyHealth()
        {
            if(_health <= 0)
            {
                PublishSubscribe.Instance.Publish<MessageOnGameOver>(new MessageOnGameOver(true));
            }
        }

        private void SetUI()
        {
            _healthText.text = "Health : " + _health.ToString();
        }
    }
}

