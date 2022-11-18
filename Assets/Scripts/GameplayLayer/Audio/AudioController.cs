using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.SaveData;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Audio
{
    public class AudioController : MonoBehaviour
    {
        private static AudioController _audioController;

        [SerializeField] private AudioData _audioData;

        [SerializeField] private List<AudioClip> _bgmList = new();
        [SerializeField] private List<AudioClip> _sfxList = new();

        private AudioSource _soundBGM;
        private AudioSource _soundSFX;

        private void Awake()
        {
            if (_audioController != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _audioController = this;
                DontDestroyOnLoad(gameObject);
            }

            PublishSubscribe.Instance.Subscribe<MessagePlaySFX>(PlaySFX);
            PublishSubscribe.Instance.Subscribe<MessagePlayBGM>(PlayBGM);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessagePlaySFX>(PlaySFX);
            PublishSubscribe.Instance.Unsubscribe<MessagePlayBGM>(PlayBGM);
        }

        private void Start()
        {
            _soundBGM = gameObject.AddComponent<AudioSource>();
            _soundSFX = gameObject.AddComponent<AudioSource>();
        }

        public void PlayBGM(MessagePlayBGM msg)
        {
            if (_audioData.isBgmPlay)
            {
                for (int i = 0; i < _bgmList.Count; i++)
                {
                    if (_bgmList[i].name == msg.Name)
                    {
                        _soundBGM.clip = _bgmList[i];
                        _audioData.bgm = _bgmList[i];
                        _soundBGM.Play();
                        _soundBGM.volume = .6f;
                        _soundBGM.loop = true;
                        SaveAudioDataController.Instance.SetData(_audioData);
                        return;
                    }
                }
            }
            else
            {
                _soundBGM.volume = 0;
            }
        }

        public void PlaySFX(MessagePlaySFX msg)
        {
            if (!_audioData.isSfxPlay) return;

            for (int i = 0; i < _sfxList.Count; i++)
            {
                if (_sfxList[i].name == msg.Name)
                {
                    _soundSFX.clip = _sfxList[i];
                    _soundSFX.Play();
                    return;
                }
            }
        }
    }
}

