using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Intruder
{
    public class Friend : BaseIntruder
    {
        private Vector3 _intrudeDirection = Vector3.right;

        private void FixedUpdate()
        {
            Intrude();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DestroyPoint") && _isMove)
            {
                StoreToPool();
            }
            else if (other.gameObject.tag.Substring(0, 5) == "Trash" && _isMove == false)
            {
                _isMove = true;
            }
        }

        public override void Movement()
        {
            transform.Translate(_speed * Time.fixedDeltaTime * Vector3.right);
        }

        public override void Intrude()
        {
            if (_isMove == false)
            {
                if (_currentTime >= _timer)
                {
                    if (_intrudeDirection == Vector3.right)
                    {
                        _intrudeDirection = Vector3.left;
                    }
                    else
                    {
                        _intrudeDirection = Vector3.right;
                    }
                    _currentTime = 0f;
                }

                transform.Translate(_speed * Time.fixedDeltaTime * _intrudeDirection);
                _currentTime += Time.fixedDeltaTime;
            }
            else
            {
                Movement();
            }
        }
    }
}
