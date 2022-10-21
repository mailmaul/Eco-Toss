using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

public class BuffDoubleScoreIcon : BaseBuffIcon
{
    private float _duration;
    private bool _isActive;

    private void Awake()
    {
        PublishSubscribe.Instance.Subscribe<MessageDoubleScoreBuffCountdown>(CountDuration);
    }

    private void OnDestroy()
    {
        PublishSubscribe.Instance.Subscribe<MessageDoubleScoreBuffCountdown>(CountDuration);
    }

    protected override void Start()
    {
        base.Start();
        gameObject.SetActive(true);
    }

    private void Update()
    {
        Countdown();
    }

    public void CountDuration(MessageDoubleScoreBuffCountdown msg)
    {
        _duration = msg.Duration;
        _isActive = true;
    }

    public void Countdown()
    {
        if (_isActive)
        {
            if (_duration > 0)
            {
                _duration -= Time.deltaTime;
                _text.SetText(Mathf.RoundToInt(_duration) + "s");
            }
            else
            {
                _isActive = false;
                gameObject.SetActive(false);
            }
        }
    }
}
