using Agate.MVC.Core;
using EcoTeam.EcoToss.GameManager;
using EcoTeam.EcoToss.HighScore;
using EcoTeam.EcoToss.PubSub;
using TMPro;
using UnityEngine;


namespace EcoTeam.EcoToss.Score
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreTMP;
        [SerializeField] private TMP_Text _finalScoreTMP;
        [SerializeField] private TMP_Text _highScoreTMP;
        private int _score = 0;
        private float _maxScore;
        private float _previousMaxScoreBuff;
        [SerializeField] private int _normalAddScore = 2;
        private int _match3Score;
        [SerializeField] private int _firstScoreToActivateBuff = 10;
        [SerializeField] private float _scoreMultiplierToActivateBuff = 1.5f;
        private int _previousScoreWhenActivatingBuff;
        [SerializeField] private int _scoreMultiplierToSpawnIntruder = 5;
        private int _previousScoreWhenSpawningIntruder = 0;
        private bool _gotFirstBuff = false;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageAddScore>(AddScore);
            PublishSubscribe.Instance.Subscribe<MessageGameOver>(UpdateFinalScore);
            PublishSubscribe.Instance.Subscribe<MessageActivateDoubleScore>(OnDoubleScoreActivated);
            PublishSubscribe.Instance.Subscribe<MessageDeactivateDoubleScore>(OnDoubleScoreDeactivated);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageAddScore>(AddScore);
            PublishSubscribe.Instance.Unsubscribe<MessageGameOver>(UpdateFinalScore);
            PublishSubscribe.Instance.Unsubscribe<MessageActivateDoubleScore>(OnDoubleScoreActivated);
            PublishSubscribe.Instance.Unsubscribe<MessageDeactivateDoubleScore>(OnDoubleScoreDeactivated);
        }

        private void Start()
        {
            _match3Score = _normalAddScore * 2 + 1;
            _scoreTMP.SetText($"Score: {_score}");
            _previousScoreWhenActivatingBuff = _firstScoreToActivateBuff;
            _previousMaxScoreBuff = 0;
            _maxScore = _firstScoreToActivateBuff;
            PublishSubscribe.Instance.Publish<MessageSetProgressBarFill>(new MessageSetProgressBarFill(_score, _maxScore, _previousMaxScoreBuff));
        }

        private void AddScore(MessageAddScore message)
        {
            switch (message.Amount)
            {
                case "Normal":
                    _score += _normalAddScore;
                    break;
                case "Match3":
                    _score += _match3Score;
                    break;
            }

            _scoreTMP.SetText($"Score: {_score}");

            if (Debug.isDebugBuild)
            {
                Debug.Log("Skor bertambah jadi: " + _score);
            }

            CheckScoreToActivateBuff();
            CheckScoreToSpawnIntruder();
        }

        private void CheckScoreToActivateBuff()
        {
            // Does the score reach the specified number to activate a Buff
            // First buff = 10
            // Second and beyond buff = 10 + (10 x 1.5) = 25
            if (_gotFirstBuff && _score >= _previousScoreWhenActivatingBuff + (_previousScoreWhenActivatingBuff * _scoreMultiplierToActivateBuff))
            {
                if (Debug.isDebugBuild)
                {
                    Debug.Log("buff kedua dan seterusnya");
                }
                PublishSubscribe.Instance.Publish<MessagePlayBuff>(new MessagePlayBuff());
                _previousScoreWhenActivatingBuff = _score;
                _previousMaxScoreBuff = _maxScore;
                _maxScore = _previousScoreWhenActivatingBuff + (_previousScoreWhenActivatingBuff * _scoreMultiplierToActivateBuff);
            }
            else if (!_gotFirstBuff && _score >= _firstScoreToActivateBuff)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.Log("buff pertama");
                }
                _gotFirstBuff = true;
                PublishSubscribe.Instance.Publish<MessagePlayBuff>(new MessagePlayBuff());
                _previousScoreWhenActivatingBuff = _score;
                _previousMaxScoreBuff = _maxScore;
                _maxScore = _previousScoreWhenActivatingBuff + (_previousScoreWhenActivatingBuff * _scoreMultiplierToActivateBuff);
            }

            PublishSubscribe.Instance.Publish<MessageSetProgressBarFill>(new MessageSetProgressBarFill(_score, _maxScore, _previousMaxScoreBuff));
        }

        private void UpdateFinalScore(MessageGameOver message)
        {
            _finalScoreTMP.SetText($"Your score: {_score}");
            if (_score > HighScoreController.Instance.HighScore)
            {
                HighScoreController.Instance.UpdateHighScore(_score);

            }
            _highScoreTMP.SetText($"HighScore: {HighScoreController.Instance.HighScore}");

        }

        private void CheckScoreToSpawnIntruder()
        {
            // Does the score reach a multiple of the specified number to spawn an intruder
            if (_score >= _previousScoreWhenSpawningIntruder + _scoreMultiplierToSpawnIntruder)
            {
                if (!GameManagerController.Instance.IsWindSpawn)
                {
                    PublishSubscribe.Instance.Publish<MessageSpawnWindArea>(new MessageSpawnWindArea());
                }
                else
                {
                    PublishSubscribe.Instance.Publish<MessageSpawnIntruder>(new MessageSpawnIntruder());
                }
                _previousScoreWhenSpawningIntruder = _score;
            }
        }

        private void OnDoubleScoreActivated(MessageActivateDoubleScore message)
        {
            _normalAddScore *= 2;
            _match3Score *= 2;
        }

        private void OnDoubleScoreDeactivated(MessageDeactivateDoubleScore message)
        {
            _normalAddScore /= 2;
            _match3Score /= 2;
        }
    }

}
