using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Intruder
{
    public class Friend : BaseIntruder
    {
        private void Start()
        {
            _isMove = true;
        }

        private void FixedUpdate()
        {
            if (_currentTime > _timer)
            {
                Intrude();
                _currentTime = 0f;
            }
            else
            {
                Movement();
            }
            _currentTime += Time.fixedDeltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Substring(0, 5) == "Trash")
            {
                StoreToPool();
            }
        }

        public override void Movement()
        {
            if (_isMove)
            {
                transform.Translate(_speed * Time.fixedDeltaTime * Vector3.right);
            }
            else
            {
                transform.Translate(_speed * Time.deltaTime * Vector3.left);
            }
        }

        public override void Intrude()
        {
            if (_isMove)
            {
                _isMove = false;
            }
            else
            {
                _isMove = true;
            }
        }
    }
}
