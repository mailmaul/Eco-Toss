using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.WindArea
{
    public class WindUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageShowWindProperties>(SetUI);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageShowWindProperties>(SetUI);
        }

        public void SetUI(MessageShowWindProperties msg)
        {
            _text.SetText("Wind Strength : " + msg.Strength + " Dir : " + msg.Direction);
        }
    }
}
