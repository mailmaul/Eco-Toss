using UnityEngine;
using UnityEngine.UI;
using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;

namespace EcoTeam.EcoToss.ProgressBar
{
    public class ProgressBarController : MonoBehaviour
    {
        [SerializeField] private Image _fill;

        private float _score;
        private float _maxScore;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSetProgressBarFill>(SetValueBar);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSetProgressBarFill>(SetValueBar);
        }

        public void SetValueBar(MessageSetProgressBarFill msg)
        {
            _score = msg.CurrentValue - msg.PreviousMaxValue;
            _maxScore = msg.MaxValue - msg.PreviousMaxValue;
            
            _fill.fillAmount = _score / _maxScore;
        }
    }
}
