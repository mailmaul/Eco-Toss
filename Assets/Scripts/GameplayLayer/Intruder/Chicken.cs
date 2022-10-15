using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.Intruder
{
    public class Chicken : BaseIntruder
    {
        

        private void Start()
        {
            _isMove = true;
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
                PublishSubscribe.Instance.Publish<MessageRemoveScore>(new MessageRemoveScore("Normal"));
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
                if (!_isMove) return;
                _isMove = false;
                PublishSubscribe.Instance.Publish<MessageCheckPointDestroy>(new MessageCheckPointDestroy(other.gameObject));
            }

            if (other.gameObject.CompareTag("DestroyPoint"))
            {
                StoreToPool();
            }
        }
    }
}
