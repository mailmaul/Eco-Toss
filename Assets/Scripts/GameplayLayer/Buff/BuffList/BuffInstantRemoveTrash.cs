using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;

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