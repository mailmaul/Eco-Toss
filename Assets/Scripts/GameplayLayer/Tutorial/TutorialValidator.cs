using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        public bool HasGoneToCorrectCan { private set; get; }
        public bool HasGoneToWrongCan { private set; get; }
        public bool HasMatch3 { private set; get; }
        public bool HasSpawnedWindArea { private set; get; }
        public bool HasSpawnedIntruder1 { private set; get; }
        public bool HasSpawnedIntruder2 { private set; get; }
        public bool HasSpawnedBuffFirstTime { private set; get; }
        public bool HasSpawnedBuffDoubleScore { private set; get; }
        public bool HasSpawnedBuffCleanCan { private set; get; }
        public bool HasSpawnedBuffLargerCapacity { private set; get; }
        #endregion

        #region Reference fields
        [SerializeField] private Canvas _tutorialCanvas;

        [Space()]
        [Header("Throwing Trash")]
        [SerializeField] private TutorialController _tutorialPrepareToThrow;
        [SerializeField] private TutorialController _tutorialThrowTrash;

        [Space()]
        [Header("Correct Can")]
        [SerializeField] private TutorialController _tutorialCorrectCanTryAgain;
        [SerializeField] private TutorialController _tutorialCorrectCan;

        [Space()]
        [Header("Wrong Can")]
        [SerializeField] private TutorialController _tutorialWrongCanTryAgain;
        [SerializeField] private TutorialController _tutorialWrongCan;

        [Space()]
        [Header("Match-3")]
        [SerializeField] private TutorialController _tutorialMatch3TryAgain;
        [SerializeField] private TutorialController _tutorialMatch3;

        [Space()]
        [Header("Hit Ground")]
        [SerializeField] private TutorialController _tutorialHitGroundTryAgain;
        [SerializeField] private TutorialController _tutorialHitGround;

        [Space()]
        [Header("Buff")]
        [SerializeField] private TutorialController _tutorialBuff;
        [SerializeField] private TutorialController _tutorialBuffDoubleScore;
        [SerializeField] private TutorialController _tutorialBuffCleanCan;
        [SerializeField] private TutorialController _tutorialBuffLargerCapacity;

        [Space()]
        [Header("Intrusion")]
        [SerializeField] private TutorialController _tutorialWind;
        [SerializeField] private TutorialController _tutorialIntruder1;
        [SerializeField] private TutorialController _tutorialIntruder2;

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
        public void SetHasGoneToCorrectCan(bool state) => HasGoneToCorrectCan = state;
        public void SetHasGoneToWrongCan(bool state) => HasGoneToWrongCan = state;
        public void SetHasMatch3(bool state) => HasMatch3 = state;
        public void SetHasSpawnedWindArea(bool state) => HasSpawnedWindArea = state;
        public void SetHasSpawnedIntruder1(bool state) => HasSpawnedIntruder1 = state;
        public void SetHasSpawnedIntruder2(bool state) => HasSpawnedIntruder2 = state;
        public void SetHasSpawnedBuffFirstTime(bool state) => HasSpawnedBuffFirstTime = state;
        public void SetHasSpawnedBuffDoubleScore(bool state) => HasSpawnedBuffDoubleScore = state;
        public void SetHasSpawnedBuffCleanCan(bool state) => HasSpawnedBuffCleanCan = state;
        public void SetHasSpawnedBuffLargerCapacity(bool state) => HasSpawnedBuffLargerCapacity = state;
        #endregion

        #region SetActive tutorial objects methods
        public void SetActiveTutorialPrepareToThrow(bool state)
        {
            _tutorialPrepareToThrow.gameObject.SetActive(state);
        }

        public void SetActiveTutorialThrowTrash(bool state)
        {
            if (!state) { _tutorialCanvas.gameObject.SetActive(state); }
            _tutorialThrowTrash.gameObject.SetActive(state);
        }

        public void SetActiveTutorialCorrectCanTryAgain(bool state) => _tutorialCorrectCanTryAgain.gameObject.SetActive(state);
        public void SetActiveTutorialCorrectCan(bool state) => _tutorialCorrectCan.gameObject.SetActive(state);
        public void SetActiveTutorialWrongCanTryAgain(bool state) => _tutorialWrongCanTryAgain.gameObject.SetActive(state);
        public void SetActiveTutorialWrongCan(bool state) => _tutorialWrongCan.gameObject.SetActive(state);
        public void SetActiveTutorialMatch3TryAgain(bool state) => _tutorialMatch3TryAgain.gameObject.SetActive(state);
        public void SetActiveTutorialMatch3(bool state) => _tutorialMatch3.gameObject.SetActive(state);
        public void SetActiveTutorialHitGroundTryAgain(bool state) => _tutorialHitGroundTryAgain.gameObject.SetActive(state);
        public void SetActiveTutorialHitGround(bool state) => _tutorialHitGround.gameObject.SetActive(state);

        public void SetActiveTutorialBuff(string name, bool state)
        {
            switch (name)
            {
                case "FirstBuff":
                    _tutorialBuff.gameObject.SetActive(state);
                    break;
                case "BuffDoubleScore":
                    _tutorialBuffDoubleScore.gameObject.SetActive(state);
                    break;

                case "BuffInstantRemoveTrash":
                    _tutorialBuffCleanCan.gameObject.SetActive(state);
                    break;

                case "BuffLargerTrashCanCapacity":
                    _tutorialBuffLargerCapacity.gameObject.SetActive(state);
                    break;
            }

            //StartCoroutine(SetActiveTutorialCanvas(false));
        }

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

            //StartCoroutine(SetActiveTutorialCanvas(false));
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

        //public void InsertReference()
        //{
        //    TutorialController[] tutorialControllers = GetComponents<TutorialController>();

        //    Transform parent = gameObject.transform;

        //    foreach (Transform child in parent)
        //    {
        //        for (int i = 0; i < tutorialControllers.Length; i++)
        //        {
        //            //if (tutorialControllers[i].name == child.name)
        //            //{
        //            //    tutorialControllers[i] = child.GetComponents<TutorialController>();
        //            //}
        //        }
        //    }
        //}
    }

    //[CustomEditor(typeof(TutorialValidator))]
    //public class InspectorTutorialValidator : Editor
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        base.OnInspectorGUI();

    //        if (GUILayout.Button("InsertReference"))
    //        {

    //        }
    //    }
    //}
}