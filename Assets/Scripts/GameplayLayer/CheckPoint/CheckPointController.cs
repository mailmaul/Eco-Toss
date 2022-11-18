using Agate.MVC.Core;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;

namespace EcoTeam.EcoToss.CheckPoint
{
    public class CheckPointController : PoolObject
    {
        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageCheckPointDestroy>(DestroyCheckPoint);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageCheckPointDestroy>(DestroyCheckPoint);
        }

        public void DestroyCheckPoint(MessageCheckPointDestroy msg)
        {
            if (gameObject == msg.CheckPointObject)
            {
                StoreToPool();
            }
        }
    }
}

