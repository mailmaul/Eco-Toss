using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.ObjectPooling;

namespace EcoTeam.EcoToss.Intruder
{
    public class Chicken : BaseIntruder
    {
        private const float _timer = 5f;
        private float _currentTime;
        private bool _isMove;

        [Header("Properties")]
        [SerializeField] private float _speed;

        private void Start()
        {
            OnCreate();
        }

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
                //mengacaukan tempat sampah (animasi tempat sampah berantakan)
                Debug.Log("Kurangi poin player"); //publish event to decrease score
                _currentTime = 0;
            }

            _currentTime += Time.deltaTime;
        }

        public override void Movement()
        {
            transform.Translate(new Vector3(_speed, 0, 0) * Time.deltaTime);
        }

        //called by event when trash hit intruder
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Substring(0, 5) == "Trash")
            {
                _isMove = true;
                Debug.Log("Ayam kena lempar");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("CheckPoint"))
            {
                _isMove = false;
            }

            if (other.gameObject.CompareTag("DestroyPoint"))
            {
                StoreToPool();
            }
        }

        public override void OnCreate()
        {
            _isMove = true;
        }
    }
}
