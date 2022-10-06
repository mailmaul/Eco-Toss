using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Intruder
{
    public class Chicken : MonoBehaviour
    {
        private const float _timer = 5f;
        private float _currentTime;
        private bool _isMove = true;

        private void Update()
        {
            if (_isMove)
            {
                Movement();
            }

            if (!_isMove)
            {
                Intrude();
            }
        }

        public void Intrude()
        {
            if(_currentTime > _timer)
            {
                //mengacaukan tempat sampah (animasi tempat sampah berantakan dan ayam pergi)
                Debug.Log("Kurangi poin player");
                _currentTime = 0;
            }

            _currentTime += Time.deltaTime;
        }

        public void Movement()
        {
            transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime);
        }

        //called by event when trash hit intruder
        public void OnHit()
        {
            _isMove = true;
            Debug.Log("Ayam kena lempar");
        }

        private void OnTriggerEnter(Collider other)
        {
            _isMove = false;
        }
    }
}
