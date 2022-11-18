using System.Collections;
using UnityEngine;

namespace EcoTeam.EcoToss.Intruder
{
    public class Friend : BaseIntruder
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _stunTime;
        private Vector3 _intrudeDirection = Vector3.forward;

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
                _animator.SetTrigger("ketimpuk");
                StartCoroutine(Stun());
            }
        }

        private IEnumerator Stun()
        {
            _speed = 0;

            yield return new WaitForSeconds(_stunTime);
            _animator.SetBool("isWalk", true);
            _speed = 5;
        }

        public override void Movement()
        {
            transform.Translate(_speed * Time.fixedDeltaTime * _intrudeDirection);
        }

        public override void Intrude()
        {
            if (_isMove == false)
            {
                if (_currentTime >= _timer)
                {
                    Vector3 newRotation = new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
                    transform.Rotate(newRotation);
                    _intrudeDirection = Vector3.forward;

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
