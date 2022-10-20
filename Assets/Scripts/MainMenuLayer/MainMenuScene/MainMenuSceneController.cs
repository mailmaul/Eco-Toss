using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EcoTeam.EcoToss.Audio;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.MainMenu
{
    public class MainMenuSceneController : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        void Start()
        {
            Input.backButtonLeavesApp = true;
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(GoToGameplay);
        }

        private void GoToGameplay()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("button_click"));
            SceneManager.LoadScene(1);
        }
    }
}