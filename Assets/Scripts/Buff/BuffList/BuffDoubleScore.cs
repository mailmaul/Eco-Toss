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
            // Double the score the player will get
            PublishSubscribe.Instance.Publish<MessageActivateDoubleScore>(new MessageActivateDoubleScore());

            base.BuffEffect();
        }

        protected override IEnumerator Debuff()
        {
            return base.Debuff();

            Debug.Log("debuff double score berjalan");

            // Return score to normal
            //PublishSubscribe.Instance.Publish<MessageDeactivateDoubleScore>(new MessageDeactivateDoubleScore());
        }
    }
}