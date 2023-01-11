using Agate.MVC.Core;
using EcoTeam.EcoToss.GameManager;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.SaveData;
using EcoTeam.EcoToss.Tutorial;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EcoTeam.EcoToss.Trash
{
    public class TrashController : PoolObject
    {
        private Quaternion _defaultRotation;
        private Rigidbody _rigidbody;
        private bool _hasCollided = false;
        private float _stayDuration = 0f;
        private const float _stayMaxDuration = 3f;

        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _rotateSpeed = 200;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _defaultRotation = transform.rotation;
        }

        private void Update()
        {
            if (_rigidbody.isKinematic == false)
            {
                transform.Rotate(_rotateSpeed * Time.deltaTime * _rotation);
            }
        }

        private void OnEnable()
        {
            _stayDuration = 0f;
        }


        public override void StoreToPool()
        {
            transform.SetPositionAndRotation(transform.position, _defaultRotation);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.isKinematic = true;
            _hasCollided = false;
            base.StoreToPool();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log(transform.name + "nabrak" + collision.transform.name);
            }

            if ((collision.gameObject.CompareTag("Ground") ||
                collision.gameObject.tag.Substring(0, 8) == "TrashCan") && _hasCollided == false)
            {
                _hasCollided = true;
                if (collision.gameObject.CompareTag("Ground"))
                {
                    PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("sampah_salah_tanah"));
                    PublishSubscribe.Instance.Publish<MessageShakingCamera>(new MessageShakingCamera());

                    // Jika berada di scene tutorial
                    if (Debug.isDebugBuild && SceneManager.GetActiveScene().buildIndex == 2)
                    {
                        // Ulang step tutorial sampai dia berhasil masuk ke tong sampah yang benar
                        if (!TutorialValidator.Instance.HasGoneToCorrectCan)
                        {
                            TutorialValidator.Instance.SetActiveTutorialCorrectCanTryAgain(true);
                        }
                        // Ulang step tutorial sampai dia berhasil masuk ke tong sampah yang salah
                        else if (!TutorialValidator.Instance.HasGoneToWrongCan)
                        {
                            TutorialValidator.Instance.SetActiveTutorialWrongCanTryAgain(true);
                        }
                        Invoke(nameof(StoreToPool), 0.5f);
                        PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
                        return;
                    }

                    PublishSubscribe.Instance.Publish<MessageDecreaseHealth>(new MessageDecreaseHealth());

                    //if (SceneManager.GetActiveScene().buildIndex == 2 && !SaveDataController.Instance.SaveData.HasDoneTutorial)
                    if ((Debug.isDebugBuild && SceneManager.GetActiveScene().buildIndex == 2) ||
                        (SceneManager.GetActiveScene().buildIndex == 2 && !SaveDataController.Instance.SaveData.HasDoneTutorial))
                    {
                        if (!TutorialValidator.Instance.HasHitGround)
                        {
                            TutorialValidator.Instance.SetHasHitGround(true);
                            TutorialValidator.Instance.SetActiveTutorialHitGroundTryAgain(false);
                            TutorialValidator.Instance.SetActiveTutorialHitGround(true);
                        }
                    }
                        
                    Invoke(nameof(StoreToPool), 0.5f);
                }
                else if (collision.gameObject.tag.Substring(0, 8) == "TrashCan")
                {
                    StoreToPool();
                }

                if (GameManagerController.Instance.IsWindSpawn)
                {
                    PublishSubscribe.Instance.Publish<MessageSetRandomPropertiesWindArea>(new MessageSetRandomPropertiesWindArea());
                }

                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (_stayDuration < _stayMaxDuration)
            {
                _stayDuration += Time.fixedDeltaTime;
            }
            else
            {
                StoreToPool();
                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
                _stayDuration = 0;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log(transform.name + "nabrak" + other.transform.name);
            }

            if (other.gameObject.CompareTag("Intruder") && _hasCollided == false)
            {
                _hasCollided = true;
                StoreToPool();

                if (GameManagerController.Instance.IsWindSpawn)
                {
                    PublishSubscribe.Instance.Publish<MessageSetRandomPropertiesWindArea>(new MessageSetRandomPropertiesWindArea());
                }

                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
            }
        }
    }
}