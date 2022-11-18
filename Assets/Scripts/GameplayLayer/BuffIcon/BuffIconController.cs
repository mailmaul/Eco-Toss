using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.BuffIcon
{
    public class BuffIconController : MonoBehaviour
    {
        [SerializeField] private List<BaseBuffIcon> _buffList = new List<BaseBuffIcon>();

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnBuffIcon>(Spawn);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnBuffIcon>(Spawn);
        }

        public void Spawn(MessageSpawnBuffIcon msg)
        {
            if (msg.Name == "BuffLargerTrashCanCapacity")
            {
                if (FindObjectOfType<BuffLargerTrashCanIcon>())
                {
                    PublishSubscribe.Instance.Publish<MessageIncreaseCountBuff>(new MessageIncreaseCountBuff());
                    return;
                }
            }

            for (int i = 0; i < _buffList.Count; i++)
            {
                if (_buffList[i].name == msg.Name)
                {
                    Instantiate(_buffList[i].gameObject, transform);
                }
            }
        }
    }
}
