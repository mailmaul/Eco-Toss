using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Buff
{
    public class BuffDoubleScore : BaseDurationalBuff
    {
        public override void BuffEffect()
        {
            base.BuffEffect();

            // Double the score the player will get
            PublishSubscribe.Instance.Publish<MessageActivateDoubleScore>(new MessageActivateDoubleScore());
            StartCoroutine(nameof(Debuff));
        }

        private IEnumerator Debuff()
        {
            yield return new WaitForSecondsRealtime(Duration);

            // Return score to normal
            PublishSubscribe.Instance.Publish<MessageDeactivateDoubleScore>(new MessageDeactivateDoubleScore());
        }
    }
}