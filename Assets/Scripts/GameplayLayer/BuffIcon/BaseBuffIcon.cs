using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EcoTeam.EcoToss.BuffIcon
{
    public abstract class BaseBuffIcon : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _text;

        protected virtual void Start()
        {

        }
    }
}
