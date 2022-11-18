using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;

namespace EcoTeam.EcoToss.Buff
{
    public class BuffLargerTrashCanCapacity : BaseBuff
    {
        public override void BuffEffect()
        {
            PublishSubscribe.Instance.Publish<MessageIncreaseTrashCanCapacity>(new MessageIncreaseTrashCanCapacity());
            PublishSubscribe.Instance.Publish<MessageSpawnBuffIcon>(new MessageSpawnBuffIcon(gameObject.name));
        }
    }
}