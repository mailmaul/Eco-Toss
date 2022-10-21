using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.Intruder
{
    public class Chicken : BaseIntruder
    {
        [SerializeField] private float _stunTime;
        [SerializeField] private Animator _animator;
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
                PublishSubscribe.Instance.Publish<MessageDecreaseHealth>(new MessageDecreaseHealth());
                PublishSubscribe.Instance.Publish<MessageShakingCamera>(new MessageShakingCamera());
                _currentTime = 0;
            }

            _currentTime += Time.deltaTime;
        }

        public override void Movement()
        {
            transform.Translate(new Vector3(_speed, 0, 0) * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CheckPoint"))
            {
                if (!_isMove) return;
                _isMove = false;
                PublishSubscribe.Instance.Publish<MessageCheckPointDestroy>(new MessageCheckPointDestroy(other.gameObject));
            }
            else if (other.CompareTag("DestroyPoint"))
            {
                StoreToPool();
            }
            else if (other.gameObject.tag.Substring(0, 5) == "Trash")
            {
                _isMove = true;
                if (Debug.isDebugBuild) { Debug.Log("Ayam kena lempar"); }
            }
        }

            IEnumerator Stun (){
            _speed = 0;
            
            

            yield return new WaitForSeconds(_stunTime);
            transform.Rotate(new Vector3(0,90,0));
            
            _animator.SetBool("isWalk", true);
            _speed = 5;
            _isMove = true;
        }
    }
}
