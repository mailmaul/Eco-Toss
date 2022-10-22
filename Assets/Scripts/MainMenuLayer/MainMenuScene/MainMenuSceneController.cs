using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.MainMenu
{
    public class MainMenuSceneController : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _htpButton;
        [SerializeField] private Button _intruderButton;
        [SerializeField] private Button _powerButton;
        [SerializeField] private Button _mainmenuButton;

        void Start()
        {
            Input.backButtonLeavesApp = true;
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(GoToGameplay);
            _htpButton.onClick.AddListener(GoToHTP);
            _intruderButton.onClick.AddListener(GoToHTP2);
            _powerButton.onClick.AddListener(GoToHTP3);
            _mainmenuButton.onClick.AddListener(GoToMainMenu);
        }   

        private void GoToGameplay()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));
            SceneManager.LoadScene(1);
        }

        private void GoToHTP()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            SceneManager.LoadScene(2);
        }

        private void GoToHTP2()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            SceneManager.LoadScene(3);
        }

        private void GoToHTP3()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            SceneManager.LoadScene(4);
        }

        private void GoToMainMenu()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            SceneManager.LoadScene(0);
        }
    }
}