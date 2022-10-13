using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Buff
{
    public class BuffInstantRemoveTrash : BaseBuff
    {
        public override void BuffEffect()
        {
            PublishSubscribe.Instance.Publish<MessageClearTrashList>(new MessageClearTrashList());
        }
    }
}