using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;

namespace EcoTeam.EcoToss.Intruder
{
    public class Friend : Intruder
    {
        private const float _timer = 5f;
        private float _currentTime;
        public bool _isMove = true;

        private void Update()
        {
            if (_isMove)
            {
                MovementRight();
               // MovementLeft();
            }

            if (!_isMove)
            {
                MovementLeft();
            }

        }

        public override void MovementRight()
        {
            transform.Translate(new Vector3(1f, 0, 0) * (Time.deltaTime*2));
            Debug.Log("Movement Right");
            
        }

        public override void MovementLeft()
        {
            transform.Translate(new Vector3(-1f, 0, 0) * (Time.deltaTime*2));
            Debug.Log("Movement Left");
            
            
        }

        //called by event when trash hit intruder
        public override void OnHit(MessageOnHitIntruder msg)
        {
            _isMove = true;
            Debug.Log("Teman kena lempar " + _isMove);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("RightCheckPoint"))
            {
                _isMove = false;
            }

            if (other.gameObject.CompareTag("LeftCheckPoint"))
            {
                _isMove = true;
            }
        }
    }
}
