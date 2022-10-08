using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace EcoTeam.EcoToss.Score
{
    public class ScoreController : MonoBehaviour
    {
        private TMP_Text _scoreTMP;
        private int _score = 0;
        [SerializeField] private int _normalScore = 1;
        [SerializeField] private int _match3Score = 10;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageAddScore>(AddScore);
            PublishSubscribe.Instance.Subscribe<MessageRemoveScore>(RemoveScore);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageAddScore>(AddScore);
            PublishSubscribe.Instance.Unsubscribe<MessageRemoveScore>(RemoveScore);
        }

        private void Start()
        {
            _scoreTMP = GetComponent<TMP_Text>();
            _scoreTMP.SetText($"{_score}");
        }

        private void AddScore(MessageAddScore message)
        {
            switch (message.Amount)
            {
                case "Normal":
                    _score += _normalScore;
                    break;
                case "Match3":
                    _score += _match3Score;
                    break;
            }

            _scoreTMP.SetText($"{_score}");

            if (Debug.isDebugBuild)
            {
                Debug.Log("Skor bertambah jadi: " + _score);
            }
        }

        private void RemoveScore(MessageRemoveScore message)
        {
            switch (message.Amount)
            {
                case "Normal":
                    _score -= _normalScore;
                    break;
            }

            _scoreTMP.SetText($"{_score}");

            if (Debug.isDebugBuild)
            {
                Debug.Log("Skor berkurang jadi: " + _score);
            }
        }
    }

}
