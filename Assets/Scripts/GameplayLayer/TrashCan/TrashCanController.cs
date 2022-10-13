using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.Trash;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EcoTeam.EcoToss.TrashCan
{
    public class TrashCanController : MonoBehaviour
    {
        [SerializeField] private Image[] _indicators;
        [SerializeField] private TMP_Text _capacityTMP;
        [SerializeField] private TMP_Text _match3TMP;
        [SerializeField] private List<TrashController> _trashList = new();
        [SerializeField] private List<TrashController> _matchedTrashList = new();
        private int _trashCanCapacity = 6;
        private int _trashCanMaxCapacity = 10;
        private string _trashCanTag;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageClearTrashList>(ClearTrashList);
            PublishSubscribe.Instance.Subscribe<MessageIncreaseTrashCanCapacity>(IncreaseTrashCanCapacity);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageClearTrashList>(ClearTrashList);
            PublishSubscribe.Instance.Unsubscribe<MessageIncreaseTrashCanCapacity>(IncreaseTrashCanCapacity);
        }

        private void Start()
        {
            _trashList.Capacity = _trashCanCapacity;
            _trashCanTag = gameObject.tag.Substring(8);
        }

        private void Update()
        {
            _capacityTMP.SetText($"Capacity: {_trashList.Count}/{_trashCanCapacity}");
            _match3TMP.SetText($"Match-3: {_matchedTrashList.Count}/3");
        }

        private void OnCollisionEnter(Collision collision)
        {
            TrashController collisionTrashController = collision.gameObject.GetComponent<TrashController>();
            string collisionTag = collision.gameObject.tag.Substring(5);

            _trashList.Add(collisionTrashController);

            if (_trashCanTag == collisionTag)
            {
                PublishSubscribe.Instance.Publish<MessageAddScore>(new MessageAddScore("Normal"));
            }

            CheckTrashListElements();

            if (_trashList.Count == _trashCanCapacity)
            {
                PublishSubscribe.Instance.Publish<MessageGameOver>(new MessageGameOver(true));
            }
        }

        private void CheckTrashListElements()
        {
            for (int i = 0; i < _trashList.Count; i++)
            {
                if (_trashList[i].tag.Substring(5) == _trashCanTag)
                {
                    // Buat indikator jadi hijau
                    _indicators[i].color = Color.green;

                    // Jika dia adalah element pertama atau kedua, maka cukup cek apakah _trashList[i] punya tag yang sama dengan TrashCan
                    if (i == 0 || i == 1)
                    {
                        _matchedTrashList.Add(_trashList[i]);
                    }

                    // Jika dia adalah element ketiga dan seterusnya, maka cek apakah _trashList[i] punya tag yang sama dengan 2 object sebelumnya
                    else if (i > 1)
                    {
                        // Match-2 validation
                        if (_trashList[i].CompareTag(_trashList[i - 1].tag))
                        {
                            // Match-3 validation
                            if (_trashList[i - 1].CompareTag(_trashList[i - 2].tag))
                            {
                                _matchedTrashList.Add(_trashList[i]);

                                // Match-3 bonus score
                                PublishSubscribe.Instance.Publish<MessageAddScore>(new MessageAddScore("Match3"));

                                for (int j = 0; j < _matchedTrashList.Count; j++)
                                {
                                    //_matchedTrashList[j].StoreToPool();
                                    _trashList.Remove(_matchedTrashList[j]);
                                }
                                _matchedTrashList.Clear();
                            }
                            // Jika Match-2 maka tetap masukkan ke dalam list
                            else
                            {
                                _matchedTrashList.Add(_trashList[i]);
                            }
                        }
                        // Jika tidak Match-2 maka masukkan ke dalam list yang di reset, karena kategori sampah sesuai dengan tempatnya tapi sebelumnya sampahnya salah
                        else
                        {
                            _matchedTrashList.Clear();
                            _matchedTrashList.Add(_trashList[i]);
                        }
                    }
                }
                else if (_trashList[i] == null)
                {
                    // Buat indikator jadi hijau
                    _indicators[i].color = Color.white;
                }
                else
                {
                    // Buat indikator jadi hijau
                    _indicators[i].color = Color.red;

                    _matchedTrashList.Clear();
                }
            }
        }

        private void ClearTrashList(MessageClearTrashList message)
        {
            _trashList.Clear();
            _matchedTrashList.Clear();
        }

        private void IncreaseTrashCanCapacity(MessageIncreaseTrashCanCapacity message)
        {
            if (_trashCanCapacity < _trashCanMaxCapacity)
            {
                // Buat indikator jadi putih
                _indicators[_trashCanCapacity].color = Color.white;

                _trashCanCapacity++;
            }
        }
    }
}