using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Buff
{
    public class BuffDoubleScore : BaseDurationalBuff
    {
        private void Update()
        {
            //CountDown();
        }

        public override void BuffEffect()
        {
            // Double the score the player will get
            PublishSubscribe.Instance.Publish<MessageActivateDoubleScore>(new MessageActivateDoubleScore());
            PublishSubscribe.Instance.Publish<MessageSpawnBuffIcon>(new MessageSpawnBuffIcon(gameObject.name));
            PublishSubscribe.Instance.Publish<MessageDoubleScoreBuffCountdown>(new MessageDoubleScoreBuffCountdown(Duration));
            StartCoroutine(nameof(Debuff));
        }

        private IEnumerator Debuff()
        {
            yield return new WaitForSecondsRealtime(Duration);

            // Return score to normal
            PublishSubscribe.Instance.Publish<MessageDeactivateDoubleScore>(new MessageDeactivateDoubleScore());
        }

        public void CountDown()
        {
            if (Duration < 0) return;
            if(Duration >= 0)
            {
                Duration -= Time.deltaTime;
                PublishSubscribe.Instance.Publish<MessageDoubleScoreBuffCountdown>(new MessageDoubleScoreBuffCountdown(Duration));
            }
        }
    }
}