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
        [SerializeField] private TMP_Text _scoreTMP;
        private int _score = 0;
        [SerializeField] private int _normalScore = 2;
        private int _match3Score;
        [SerializeField] private int _scoreMultiplierToActivateBuff = 10;
        private int _previousScoreWhenActivatingBuff = 0;
        
        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageAddScore>(AddScore);
            PublishSubscribe.Instance.Subscribe<MessageRemoveScore>(RemoveScore);
            PublishSubscribe.Instance.Subscribe<MessageActivateDoubleScore>(OnDoubleScoreActivated);
            PublishSubscribe.Instance.Subscribe<MessageDeactivateDoubleScore>(OnDoubleScoreDeactivated);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageAddScore>(AddScore);
            PublishSubscribe.Instance.Unsubscribe<MessageRemoveScore>(RemoveScore);
            PublishSubscribe.Instance.Unsubscribe<MessageActivateDoubleScore>(OnDoubleScoreActivated);
            PublishSubscribe.Instance.Unsubscribe<MessageDeactivateDoubleScore>(OnDoubleScoreDeactivated);
        }

        private void Start()
        {
            _match3Score = _score * 2 + 1;
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

            if (_score % _scoreMultiplierToActivateBuff == 0 || // Does the score reach a multiple of the specified number to activate the Buff
                _score >= _previousScoreWhenActivatingBuff + _scoreMultiplierToActivateBuff) // Does the score past it
            {
                PublishSubscribe.Instance.Publish<MessagePlayBuff>(new MessagePlayBuff());
                _previousScoreWhenActivatingBuff = _score;
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

        private void OnDoubleScoreActivated(MessageActivateDoubleScore message)
        {
            _normalScore *= 2;
            _match3Score *= 2;
        }

        private void OnDoubleScoreDeactivated(MessageDeactivateDoubleScore message)
        {
            _normalScore /= 2;
            _match3Score /= 2;
        }
    }

}
