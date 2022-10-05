using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.WindArea
{
    public class WindAreaController : MonoBehaviour
    {
        [SerializeField] private float _maxWindStrength;
        [SerializeField] private List<Vector3> _windDirections = new List<Vector3>();

        private float _windStrength;
        private Vector3 _windDirection;

        private void Start()
        {
            RandomPropertiesValue();
        }

        private void OnTriggerEnter(Collider other)
        {
            WindForce(other.gameObject);
        }

        public void WindForce(GameObject obj)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddForce(_windStrength * _windDirection);
                Debug.Log("Str : " + _windStrength + ", Dir : " + _windDirection);
            }
        }

        //Dipanggil ketika sampah jatuh setelah dilempar
        public void RandomPropertiesValue()
        {
            _windStrength = Random.Range(0.5f, _maxWindStrength);
            int index = Random.Range(0, _windDirections.Count);
            _windDirection = _windDirections[index];
        }
    }
}