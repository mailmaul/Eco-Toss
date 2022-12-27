using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.SaveData;
using EcoTeam.EcoToss.Tutorial;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EcoTeam.EcoToss.GameplayScene
{
    public class GameplaySceneController : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartButton;

        private void Start()
        {
            Input.backButtonLeavesApp = false;
            _backButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();

            _backButton.onClick.AddListener(GoToMainMenu);
            _mainMenuButton.onClick.AddListener(GoToMainMenu);
            _restartButton.onClick.AddListener(ReloadScene);

            PublishSubscribe.Instance.Publish<MessagePlayBGM>(new MessagePlayBGM("bgm_ingame"));
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                GoToMainMenu();
            }
        }

        private void GoToMainMenu()
        {
            // Tutorial Mark as Done Prompt
            if (!SaveDataController.Instance.SaveData.HasDoneTutorial)
            {
                TutorialValidator.Instance.SetActiveTutorialDone(true);
            }
            else
            {
                PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));
                SceneManager.LoadScene(0);
            }
        }

        private void GoToGameplay()
        {
            SceneManager.LoadScene(1);
        }

        private void ReloadScene()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));

            // Tutorial Mark as Done Prompt
            if (!SaveDataController.Instance.SaveData.HasDoneTutorial)
            {
                TutorialValidator.Instance.MarkTutorialAsDone(true);
                GoToGameplay();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}