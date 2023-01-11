using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.SaveData;
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
        [SerializeField] private Button _creditBackButton;

        void Start()
        {
            Input.backButtonLeavesApp = true;
            _playButton.onClick.RemoveAllListeners();
            _tutorialButton.onClick.RemoveAllListeners();
            _creditButton.onClick.RemoveAllListeners();
            _creditBackButton.onClick.RemoveAllListeners();

            _playButton.onClick.AddListener(GoToGameplay);
            _tutorialButton.onClick.AddListener(GoToTutorial);
            _creditButton.onClick.AddListener(SetActiveCreditPanel);
            _creditBackButton.onClick.AddListener(SetActiveCreditPanel);
        }

        private void GoToGameplay()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));
            if (SaveDataController.Instance.SaveData.HasDoneTutorial)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(2);
            }
        }

        private void GoToTutorial()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            SceneManager.LoadScene(2);
        }

        private void SetActiveCreditPanel()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            if (!_creditPanel.activeInHierarchy)
            {
                _creditPanel.SetActive(true);
                _mainPanel.SetActive(false);
            }
            else
            {
                _creditPanel.SetActive(false);
                _mainPanel.SetActive(true);
            }
        }
    }
}