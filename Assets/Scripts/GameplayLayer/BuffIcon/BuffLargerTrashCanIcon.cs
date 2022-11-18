using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;

namespace EcoTeam.EcoToss.BuffIcon
{
    public class BuffLargerTrashCanIcon : BaseBuffIcon
    {
        private int _count;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageIncreaseCountBuff>(AddCount);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Subscribe<MessageIncreaseCountBuff>(AddCount);
        }

        private void Start()
        {
            PublishSubscribe.Instance.Publish<MessageIncreaseCountBuff>(new MessageIncreaseCountBuff());
        }

        public void AddCount(MessageIncreaseCountBuff msg)
        {
            _count++;
            _text.SetText("x" + _count);
        }
    }
}
