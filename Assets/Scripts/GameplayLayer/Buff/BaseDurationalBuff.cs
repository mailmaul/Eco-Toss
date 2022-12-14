using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Buff
{
    public abstract class BaseDurationalBuff : BaseBuff
    {
        protected float Duration = 10;

        // Customize duration for buff
        protected virtual float SetDuration
        {
            get { return Duration; }
            set { Duration = value; }
        }
    }
}