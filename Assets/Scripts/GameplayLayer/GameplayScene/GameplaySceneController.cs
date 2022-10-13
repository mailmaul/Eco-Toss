using System.Collections;
using System.Collections.Generic;
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
            _restartButton.onClick.AddListener(ReloadGameplayScene);
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
            SceneManager.LoadScene(0);
        }

        private void ReloadGameplayScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}