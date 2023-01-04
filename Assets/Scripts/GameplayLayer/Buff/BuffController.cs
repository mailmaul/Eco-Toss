using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EcoTeam.EcoToss.Buff
{
    public class BuffController : MonoBehaviour
    {
        [SerializeField] private List<BaseBuff> _buffList = new();
        [SerializeField] private Image _currentImage;
        [SerializeField] private GameObject _panel;

        private int _randomIndex;
        private float _currentTimerSpin;
        [SerializeField] private float _timerSpin = 1f;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessagePlayBuff>(PlayBuff);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessagePlayBuff>(PlayBuff);
        }

        private void Start()
        {
            _currentImage.sprite = _buffList[0].GetSprite();
            //_currentImage.gameObject.SetActive(false);
            _currentTimerSpin = _timerSpin;
        }

        private void PlayBuff(MessagePlayBuff message)
        {
            StartCoroutine(RandomBuff());
        }

        IEnumerator RandomBuff()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("powerup_roll"));
            _panel.gameObject.SetActive(true);
            //_currentImage.gameObject.SetActive(true);
            while (_currentTimerSpin >= 0)
            {
                _randomIndex = Random.Range(0, _buffList.Count);
                _currentImage.sprite = _buffList[_randomIndex].GetSprite();
                _currentTimerSpin -= Time.deltaTime * 10;
                yield return new WaitForSeconds(.05f);
            }

            _currentTimerSpin = _timerSpin;
            _buffList[_randomIndex].BuffEffect();
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("powerup_dapat"));

            if (Debug.isDebugBuild)
            {
                Debug.Log("get buff: " + _buffList[_randomIndex].name);
            }

            yield return new WaitForSeconds(3);

            _panel.gameObject.SetActive(false);
            //_currentImage.gameObject.SetActive(false);
        }
    }
}