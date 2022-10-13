using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EcoTeam.EcoToss.MainMenu
{
    public class MainMenuSceneController : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        // Start is called before the first frame update
        void Start()
        {
            Input.backButtonLeavesApp = true;
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(GoToGameplay);
        }

        private void GoToGameplay()
        {
            SceneManager.LoadScene(1);
        }
    }
}