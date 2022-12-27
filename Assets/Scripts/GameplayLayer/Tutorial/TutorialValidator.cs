using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EcoTeam.EcoToss.Tutorial
{
    public class TutorialValidator : MonoBehaviour
    {
        #region Singleton
        public static TutorialValidator Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        #endregion

        #region Bool fields
        public bool HasTapped { private set; get; }
        public bool HasThrownTrash { private set; get; }
        public bool HasHitGround { private set; get; }
        public bool HasGoneToCorrectBin { private set; get; }
        public bool HasGoneToWrongBin { private set; get; }
        public bool HasMatch3 { private set; get; }
        public bool HasSpawnedWindArea { private set; get; }
        public bool HasSpawnedIntruder1 { private set; get; }
        public bool HasSpawnedIntruder2 { private set; get; }
        public bool HasSpawnedBuffDoubleScore { private set; get; }
        public bool HasSpawnedBuffCleanBin { private set; get; }
        public bool HasSpawnedBuffLargerCapacity { private set; get; }
        #endregion

        #region Reference fields
        [SerializeField] private Canvas _tutorialCanvas;

        [Space()]
        [Header("Sequential Order")]
        [SerializeField] private TutorialController _tutorialPrepareToThrow;
        [SerializeField] private TutorialController _tutorialThrowTrash;
        [SerializeField] private TutorialController _tutorialHitGround;
        [SerializeField] private TutorialController _tutorialCorrectBin;
        [SerializeField] private TutorialController _tutorialWrongBin;
        
        [Space()]
        [Header("No Sequential Order")]
        [SerializeField] private TutorialController _tutorialMatch3;
        [SerializeField] private TutorialController _tutorialWind;
        [SerializeField] private TutorialController _tutorialIntruder1;
        [SerializeField] private TutorialController _tutorialIntruder2;
        [SerializeField] private TutorialController _tutorialBuffDoubleScore;
        [SerializeField] private TutorialController _tutorialBuffCleanBin;
        [SerializeField] private TutorialController _tutorialBuffLargerCapacity;

        [Space()]
        [Header("Done")]
        [SerializeField] private TutorialController _tutorialDone;
        #endregion

        private void Start()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnWindArea>(SetActiveTutorialWind);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnWindArea>(SetActiveTutorialWind);
        }

        #region Set bool state methods
        public void SetHasTapped(bool state) => HasTapped = state;
        public void SetHasThrownTrash(bool state) => HasThrownTrash = state;
        public void SetHasHitGround(bool state) => HasHitGround = state;
        public void SetHasGoneToCorrectBin(bool state) => HasGoneToCorrectBin = state;
        public void SetHasGoneToWrongBin(bool state) => HasGoneToWrongBin = state;
        public void SetHasMatch3(bool state) => HasMatch3 = state;
        public void SetHasSpawnedWindArea(bool state) => HasSpawnedWindArea = state;
        public void SetHasSpawnedIntruder1(bool state) => HasSpawnedIntruder1 = state;
        public void SetHasSpawnedIntruder2(bool state) => HasSpawnedIntruder2 = state;
        public void SetHasSpawnedBuffDoubleScore(bool state) => HasSpawnedBuffDoubleScore = state;
        public void SetHasSpawnedBuffCleanBin(bool state) => HasSpawnedBuffCleanBin = state;
        public void SetHasSpawnedBuffLargerCapacity(bool state) => HasSpawnedBuffLargerCapacity = state;
        #endregion

        #region SetActive tutorial objects methods
        public void SetActiveTutorialPrepareToThrow(bool state)
        {
            _tutorialPrepareToThrow.gameObject.SetActive(state);
        }

        public void SetActiveTutorialThrowTrash(bool state)
        {
            if (!state)
            {
                _tutorialCanvas.gameObject.SetActive(state);
            }

            _tutorialThrowTrash.gameObject.SetActive(state);
        }

        public void SetActiveTutorialHitGround(bool state) => _tutorialHitGround.gameObject.SetActive(state);
        public void SetActiveTutorialCorrectBin(bool state) => _tutorialCorrectBin.gameObject.SetActive(state);
        public void SetActiveTutorialWrongBin(bool state) => _tutorialWrongBin.gameObject.SetActive(state);
        public void SetActiveTutorialMatch3(bool state) => _tutorialMatch3.gameObject.SetActive(state);

        public void SetActiveTutorialWind(MessageSpawnWindArea message)
        {
            if (!HasSpawnedWindArea)
            {
                HasSpawnedWindArea = true;
                _tutorialWind.gameObject.SetActive(true);
            }
        }

        public void SetActiveTutorialIntruder(string name, bool state)
        {
            switch (name)
            {
                case "Patrol":
                    _tutorialIntruder1.gameObject.SetActive(state);
                    break;

                case "Scramble":
                    _tutorialIntruder2.gameObject.SetActive(state);
                    break;
            }

            StartCoroutine(SetActiveTutorialCanvas(false));
        }

        public void SetActiveTutorialBuff(string name, bool state)
        {
            switch (name)
            {
                case "BuffDoubleScore":
                    _tutorialBuffDoubleScore.gameObject.SetActive(state);
                    break;

                case "BuffInstantRemoveTrash":
                    _tutorialBuffCleanBin.gameObject.SetActive(state);
                    break;

                case "BuffLargerTrashCanCapacity":
                    _tutorialBuffLargerCapacity.gameObject.SetActive(state);
                    break;
            }

            StartCoroutine(SetActiveTutorialCanvas(false));
        }

        public void SetActiveTutorialDone(bool state)
        {
            _tutorialDone.gameObject.SetActive(state);
        }
        #endregion

        IEnumerator SetActiveTutorialCanvas(bool state)
        {
            yield return new WaitForSecondsRealtime(5);
            _tutorialCanvas.gameObject.SetActive(state);
        }

        public void MarkTutorialAsDone(bool state)
        {
            SaveDataController.Instance.SaveData.HasDoneTutorial = true;
            SaveDataController.Instance.Save();
            SceneManager.LoadScene(0);
        }
    }
}