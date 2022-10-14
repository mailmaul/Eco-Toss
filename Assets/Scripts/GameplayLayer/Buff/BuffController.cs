using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Buff
{
    public class BuffController : MonoBehaviour
    {
        [SerializeField] private List<BaseBuff> _buffList = new();
        private int _randomIndex;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessagePlayBuff>(PlayBuff);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessagePlayBuff>(PlayBuff);
        }

        private void PlayBuff(MessagePlayBuff message)
        {
            _randomIndex = Random.Range(0, _buffList.Count);

            _buffList[_randomIndex].BuffEffect();

            if (Debug.isDebugBuild)
            {
                Debug.Log("get buff: " + _buffList[_randomIndex].name);
            }
        }
    }
}