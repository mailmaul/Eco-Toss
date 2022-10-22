using EcoTeam.EcoToss.SaveData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EcoTeam.EcoToss.HighScore
{
    public class HighScoreController : MonoBehaviour
    {
        public static HighScoreController Instance;
        public int HighScore;
        private TMP_Text _highScoreTMP;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(base.gameObject);
            }
            else
            {
                LoadHighScore();
                Destroy(base.gameObject);
            }
        }

        private void Start()
        {
            LoadHighScore();
        }

        public void UpdateHighScore(int _score)
        {
            HighScore = _score;
            SaveDataController.Instance.SaveData.SavedHighScore = HighScore;
            SaveDataController.Instance.Save();
        }

        public void LoadHighScore()
        {
            HighScore = SaveDataController.Instance.SaveData.SavedHighScore;
            SetHighScoreText();
        }

        public void SetHighScoreText()
        {
            _highScoreTMP = GameObject.FindGameObjectWithTag("HighScore").GetComponent<TMP_Text>();
            _highScoreTMP.SetText($"High Score: {HighScore}");
        }
    }
}