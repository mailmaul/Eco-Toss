using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.Intruder
{
    public class Chicken : BaseIntruder
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _stunTime;
        [SerializeField] private bool _isAttack;
        private void Start()
        {
            _isMove = true;
            _isAttack = false;
        }

        private void Update()
        {
            if (_isMove)
            {
                Movement();
            }

            else if (!_isMove)
            {
                Intrude();
            }
        }

        public override void Intrude()
        {
            _isAttack = true;
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
            transform.Translate(new Vector3(0, 0, _speed) * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Substring(0, 5) == "Trash")
            {
                _animator.SetTrigger("ketimpuk");

                StartCoroutine(Stun());
                if (Debug.isDebugBuild) { Debug.Log("Ayam kena lempar"); }
            }
            else if (other.CompareTag("CheckPoint"))
            {
                if (!_isMove) return;
                _isMove = false;
                transform.Rotate(new Vector3(0,-90,0));
                _animator.SetTrigger("acaksampah");
                PublishSubscribe.Instance.Publish<MessageCheckPointDestroy>(new MessageCheckPointDestroy(other.gameObject));
                
            }
            else if (other.CompareTag("DestroyPoint"))
            {
                StoreToPool();
            }
        }

            IEnumerator Stun (){
            _speed = 0;

            yield return new WaitForSeconds(_stunTime);
            if (_isAttack){
                transform.Rotate(new Vector3(0, 90, 0));
            }

            _animator.SetBool("isWalk", true);
            _speed = 5;
            _isMove = true;
        }
    }
}
