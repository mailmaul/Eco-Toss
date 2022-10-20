using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.CameraShake
{
    public class CameraShakeController : MonoBehaviour
    {
        [SerializeField] private float _duration = .5f;
        [SerializeField] private float _smoothness;
        private Vector3 _startPos;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageShakingCamera>(Shake);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageShakingCamera>(Shake);
        }

        private void Start()
        {
            _startPos = transform.position;
        }

        IEnumerator ShakeCoroutine()
        {
            float elapsedTime = .1f;

            while (elapsedTime < _duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                transform.position = _startPos + Random.insideUnitSphere * _smoothness;
                yield return null;
            }

            transform.position = _startPos;
        }

        public void Shake(MessageShakingCamera msg)
        {
            StartCoroutine(ShakeCoroutine());
        }
    }

}
