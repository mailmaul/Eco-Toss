using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Buff
{
    public class BaseDurationalBuff : BaseBuff
    {
        protected float Duration = 10;

        // Customize duration for buff
        protected virtual float SetDuration
        {
            get { return Duration; }
            set { Duration = value; }
        }

        public override void BuffEffect()
        {
            base.BuffEffect();
            StartCoroutine(nameof(Debuff));
        }

        protected virtual IEnumerator Debuff()
        {
            yield return new WaitForSecondsRealtime(Duration);
        }
    }
}