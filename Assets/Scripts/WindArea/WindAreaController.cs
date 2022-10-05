using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.WindArea
{
    public class WindAreaController : MonoBehaviour
    {
        [SerializeField] private float _maxWindStrength;
        [SerializeField] private List<Vector3> _windDirections = new List<Vector3>();

        private void OnTriggerEnter(Collider other)
        {
            WindForce(other.gameObject);
        }

        public void WindForce(GameObject obj)
        {
            float strength = Random.Range(0.1f, _maxWindStrength);
            int index = Random.Range(0, _windDirections.Count);

            Rigidbody rb = obj.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddForce(strength * _windDirections[index]);
                Debug.Log("Str : " + strength + ", Dir : " + _windDirections[index]);
            }
        }
    }
}