using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Buff
{
    public abstract class BaseBuff : MonoBehaviour
    {
        [SerializeField] protected Sprite _sprite;
        public abstract void BuffEffect();

        public Sprite GetSprite()
        {
            return _sprite;
        }
    }
}