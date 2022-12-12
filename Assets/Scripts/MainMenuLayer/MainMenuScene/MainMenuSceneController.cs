using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EcoTeam.EcoToss.MainMenu
{
    public class MainMenuSceneController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _creditButton;
        [SerializeField] private GameObject _creditPanel;
        [SerializeField] private Button _backButton;
        [SerializeField] private GameObject[] _tutorialPanels;
        [SerializeField] private GameObject _navigationButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _previousButton;

        void Start()
        {
            Input.backButtonLeavesApp = true;
            _playButton.onClick.RemoveAllListeners();
            _tutorialButton.onClick.RemoveAllListeners();
            _creditButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
            _previousButton.onClick.RemoveAllListeners();

            _playButton.onClick.AddListener(GoToGameplay);
            _tutorialButton.onClick.AddListener(GoToTutorial);
            _creditButton.onClick.AddListener(SetActiveCreditPanel);
            _backButton.onClick.AddListener(SetActiveCreditPanel);
            _continueButton.onClick.AddListener(ContinueTutorial);
            _previousButton.onClick.AddListener(PreviousTutorial);
        }

        private void GoToGameplay()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));
            SceneManager.LoadScene(1);
        }

        private void SetActiveCreditPanel()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            if (_creditPanel.activeInHierarchy)
            {
                _creditPanel.SetActive(false);
                _mainPanel.SetActive(true);
            }
            else
            {
                _creditPanel.SetActive(true);
                _mainPanel.SetActive(false);
            }
        }

        private void GoToTutorial()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            _tutorialPanels[0].SetActive(true);
            _navigationButton.SetActive(true);
            _mainPanel.SetActive(false);
        }

        private void PreviousTutorial()
        {
            for(int i = 0; i < _tutorialPanels.Length; i++)
            {
                if (i == 0 && _tutorialPanels[i].activeInHierarchy)
                {
                    _mainPanel.SetActive(true);
                    _navigationButton.SetActive(false);
                    _tutorialPanels[i].SetActive(false);
                    break;
                }
                else if (i >= 0 && _tutorialPanels[i].activeInHierarchy)
                {
                    _tutorialPanels[i - 1].SetActive(true);
                    _tutorialPanels[i].SetActive(false);
                    break;
                }
            }
        }

        private void ContinueTutorial()
        {
            for (int i = 0; i < _tutorialPanels.Length; i++)
            {
                if (i == _tutorialPanels.Length - 1 && _tutorialPanels[i].activeInHierarchy)
                {
                    _mainPanel.SetActive(true);
                    _navigationButton.SetActive(false);
                    _tutorialPanels[i].SetActive(false);
                    break;
                }
                else if (i < _tutorialPanels.Length - 1 && _tutorialPanels[i].activeInHierarchy)
                {
                    _tutorialPanels[i + 1].SetActive(true);
                    _tutorialPanels[i].SetActive(false);
                    break;
                }
            }
        }
    }
}