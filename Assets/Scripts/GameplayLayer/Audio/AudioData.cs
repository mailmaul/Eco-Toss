using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Audio
{
    [CreateAssetMenu(fileName ="AudioData", menuName ="Audio Data")]
    [System.Serializable]
    public class AudioData : ScriptableObject
    {
        public AudioClip bgm;
        public bool isBgmPlay;
        public bool isSfxPlay;
    }
}
