using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.CameraShake
{
    public class CameraShakeController : MonoBehaviour
    {
        [SerializeField] private float duration = .5f;
        [SerializeField] private float smoothness;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageShakingCamera>(Shake);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageShakingCamera>(Shake);
        }

        IEnumerator ShakeCoroutine()
        {
            Handheld.Vibrate();
            Vector3 startPos = transform.position;
            float elapsedTime = .1f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                transform.position = startPos + Random.insideUnitSphere * smoothness;
                yield return null;
            }

            transform.position = startPos;
        }

        public void Shake(MessageShakingCamera msg)
        {
            StartCoroutine(ShakeCoroutine());
        }
    }

}
