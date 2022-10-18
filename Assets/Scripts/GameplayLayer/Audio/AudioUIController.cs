using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.SaveData;

namespace EcoTeam.EcoToss.Audio
{
    public class AudioUIController : MonoBehaviour
    {
        [SerializeField] private AudioData _audioData;

        [Header("UI")]
        [SerializeField] private Toggle _bgmToggle;
        [SerializeField] private Toggle _sfxToggle;

        private void Start()
        {
           _bgmToggle.isOn = _audioData.isBgmPlay;
           _sfxToggle.isOn = _audioData.isSfxPlay;
            SetToggleListener();
        }

        private void SetToggleListener()
        {
            _bgmToggle.onValueChanged.RemoveAllListeners();
            _bgmToggle.onValueChanged.AddListener((b) => SetBGMToggle(_audioData.isBgmPlay));
            _sfxToggle.onValueChanged.RemoveAllListeners();
            _sfxToggle.onValueChanged.AddListener((b) => SetSFXToggle(_audioData.isSfxPlay));
        }

        public void SetBGMToggle(bool play)
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            _bgmToggle.isOn = !play;
            _audioData.isBgmPlay = !play;
            SaveAudioDataController.Instance.SetData(_audioData);
            PublishSubscribe.Instance.Publish<MessagePlayBGM>(new MessagePlayBGM("bgm"));
        }

        public void SetSFXToggle(bool play)
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            _sfxToggle.isOn = !play;
            _audioData.isSfxPlay = !play;
            SaveAudioDataController.Instance.SetData(_audioData);
        }

        private void OnApplicationQuit()
        {
            SaveAudioDataController.Instance.SetData(_audioData);
        }
    }
}
