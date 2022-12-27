using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using UnityEngine;
using System.Collections.Generic;

namespace EcoTeam.EcoToss.WindArea
{
    public class WindAreaController : MonoBehaviour
    {
        [SerializeField] private Vector3[] _windDirections = new Vector3[2];
        [SerializeField] private List<float> _windStrengthList = new List<float>();

        private float _windStrength;
        private Vector3 _windDirection;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSetRandomPropertiesWindArea>(RandomPropertiesValue);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSetRandomPropertiesWindArea>(RandomPropertiesValue);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log("Wind affecting " + other.name);
            }
            if (other.CompareTag("TrashOrganic") || other.CompareTag("TrashNonOrganic") || other.CompareTag("TrashDangerous"))
            {
                WindForce(other.gameObject);
            }
        }

        public void WindForce(GameObject obj)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddForce(_windStrength * _windDirection);
                if (Debug.isDebugBuild)
                {
                    Debug.Log("Str : " + _windStrength + ", Dir : " + _windDirection);
                }
            }
        }

        //Dipanggil pada script TrashController saat sampah collide with ground or intruder
        public void RandomPropertiesValue(MessageSetRandomPropertiesWindArea msg)
        {
            _windStrength = _windStrengthList[Random.Range(0, _windStrengthList.Count)];
            int index = Random.Range(0, _windDirections.Length);
            _windDirection = _windDirections[index];

            string direction = string.Empty;
            if (index == 0)
            {
                direction = "Right";
            }
            else if (index == 1)
            {
                direction = "Left";
            }

            PublishSubscribe.Instance.Publish<MessageShowWindProperties>(new MessageShowWindProperties((Mathf.Round(_windStrength * 100) / 100), direction));
        }
    }
}