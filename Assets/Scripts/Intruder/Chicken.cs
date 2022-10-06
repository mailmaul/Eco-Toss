using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Intruder
{
    public class Chicken : MonoBehaviour
    {
        private const float _castingTime = 5f;
        private float _time;
        private bool _isIntrude = false;
        private bool _isMove = true;

        private void Update()
        {
            if (_isMove)
            {
                Movement();
            }

            if (!_isIntrude && !_isMove)
            {
                Intrude();
            }
        }

        public void Intrude()
        {
            if (_time > _castingTime)
            {
                //mengacaukan tempat sampah (animasi tempat sampah berantakan dan ayam pergi)
                Debug.Log("Mengacaukan tempat sampah");
                _isIntrude = true;
                _isMove = true;
                _time = 0;
                return;
            }

            _time += Time.deltaTime;
        }

        public void Movement()
        {
            transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime);
        }

        //called by event when trash hit intruder
        public void OnHit()
        {
            _isIntrude = true;
            _isMove = true;
            Debug.Log("Ayam kena lempar");
        }

        private void OnTriggerEnter(Collider other)
        {
            _isMove = false;
        }
    }
}
