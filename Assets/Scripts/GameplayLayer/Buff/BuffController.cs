using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.SaveData;
using EcoTeam.EcoToss.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            // Tutorial buff spawn
            if ((Debug.isDebugBuild && SceneManager.GetActiveScene().buildIndex == 2) ||
                (SceneManager.GetActiveScene().buildIndex == 2 && !SaveDataController.Instance.SaveData.HasDoneTutorial))
            //if (SceneManager.GetActiveScene().buildIndex == 2 && !SaveDataController.Instance.SaveData.HasDoneTutorial)
            {
                if (!TutorialValidator.Instance.HasSpawnedBuffFirstTime)
                {
                    TutorialValidator.Instance.SetHasSpawnedBuffFirstTime(true);
                    TutorialValidator.Instance.SetActiveTutorialBuff("FirstBuff", true);
                }
            }

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

            //// Debug purposes
            //_randomIndex = 0;

            StartCoroutine(BuffExplanation());

            _buffList[_randomIndex].BuffEffect();
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("powerup_dapat"));

            if (Debug.isDebugBuild)
            {
                Debug.Log("get buff: " + _buffList[_randomIndex].name);
            }

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                yield return new WaitForSeconds(0.75f);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }

            _panel.gameObject.SetActive(false);
            //_currentImage.gameObject.SetActive(false);
        }

        IEnumerator BuffExplanation()
        {
            yield return new WaitForSecondsRealtime(1);

            // Tutorial buff effect
            if ((Debug.isDebugBuild && SceneManager.GetActiveScene().buildIndex == 2) ||
                (SceneManager.GetActiveScene().buildIndex == 2 && !SaveDataController.Instance.SaveData.HasDoneTutorial))
            //if (SceneManager.GetActiveScene().buildIndex == 2 && !SaveDataController.Instance.SaveData.HasDoneTutorial)
            {
                if (_buffList[_randomIndex].name == "BuffDoubleScore")
                {
                    if (!TutorialValidator.Instance.HasSpawnedBuffDoubleScore)
                    {
                        TutorialValidator.Instance.SetHasSpawnedBuffDoubleScore(true);
                        TutorialValidator.Instance.SetActiveTutorialBuff(_buffList[_randomIndex].name, true);
                    }
                }
                else if (_buffList[_randomIndex].name == "BuffInstantRemoveTrash")
                {
                    if (!TutorialValidator.Instance.HasSpawnedBuffCleanBin)
                    {
                        TutorialValidator.Instance.SetHasSpawnedBuffCleanBin(true);
                        TutorialValidator.Instance.SetActiveTutorialBuff(_buffList[_randomIndex].name, true);
                    }
                }
                else if (_buffList[_randomIndex].name == "BuffLargerTrashCanCapacity")
                {
                    if (!TutorialValidator.Instance.HasSpawnedBuffLargerCapacity)
                    {
                        TutorialValidator.Instance.SetHasSpawnedBuffLargerCapacity(true);
                        TutorialValidator.Instance.SetActiveTutorialBuff(_buffList[_randomIndex].name, true);
                    }
                }
            }
        }
    }
}