using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;

namespace EcoTeam.EcoToss.Intruder
{
    public class Chicken : Intruder
    {
        private const float _timer = 5f;
        private float _currentTime;
        public bool _isMove = true;

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

        public override void Intrude()
        {
            if(_currentTime > _timer)
            {
                //mengacaukan tempat sampah (animasi tempat sampah berantakan dan ayam pergi)
                Debug.Log("Kurangi poin player");
                _currentTime = 0;
            }

            _currentTime += Time.deltaTime;
        }

        public override void Movement()
        {
            transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime);
            Debug.Log("Movement");
        }

        //called by event when trash hit intruder
        public override void OnHit(MessageOnHitIntruder msg)
        {
            _isMove = true;
            Debug.Log("Ayam kena lempar " + _isMove);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("CheckPoint"))
            {
                _isMove = false;
            }
        }
    }
}
