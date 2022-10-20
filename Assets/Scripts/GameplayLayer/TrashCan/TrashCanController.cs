using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.Trash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EcoTeam.EcoToss.TrashCan
{
    public class TrashCanController : MonoBehaviour
    {
        [SerializeField] private Image _indicatorParent;
        [SerializeField] private Image _indicatorPrefab;
        [SerializeField] private Image[] _indicators;
        [SerializeField] private List<TrashController> _trashList = new();
        private List<TrashController> _matchedTrashList = new();
        private int _trashCanCapacity = 6;
        private int _trashCanMaxCapacity = 10;
        private string _trashCanTag;
        private HorizontalLayoutGroup _indicatorParentLayoutGroup;
        private RectTransform _indicatorParentRectTransform;

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

            _indicators = new Image[_trashCanMaxCapacity];
            _indicatorParentLayoutGroup = _indicatorParent.GetComponent<HorizontalLayoutGroup>();
            _indicatorParentRectTransform = _indicatorParent.GetComponent<RectTransform>();

            // spawn TrashCan Indicators
            for (int i = 0; i < _trashCanMaxCapacity; i++)
            {
                _indicators[i] = Instantiate(_indicatorPrefab, _indicatorParent.transform, false);
                if (i >= _trashCanCapacity)
                {
                    _indicators[i].gameObject.SetActive(false);
                }
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(_indicatorParentRectTransform);
            _indicatorParentLayoutGroup.enabled = false;
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
            else
            {
                PublishSubscribe.Instance.Publish<MessageShakingCamera>(new MessageShakingCamera());
            }

            CheckTrashListElements();

            if (_trashList.Count == _trashCanCapacity)
            {
                PublishSubscribe.Instance.Publish<MessageGameOver>(new MessageGameOver(true));
            }
        }

        private void CheckTrashListElements()
        {
            Debug.Log("masuk check");
            for (int i = 0; i < _trashList.Count; i++)
            {
                if (_trashList[i].tag.Substring(5) == _trashCanTag)
                {
                    //Buat indikator jadi hijau
                    _indicators[i].color = Color.green;

                    // Jika dia adalah element pertama atau kedua, maka cukup cek apakah _trashList[i] punya tag yang sama dengan TrashCan
                    if (i == 0)
                    {
                        if (Debug.isDebugBuild) { Debug.Log("match 1"); }
                        _matchedTrashList.Add(_trashList[i]);
                    }
                    // Jika dia adalah element kedua dan seterusnya, maka cek apakah _trashList[i] punya tag yang sama dengan 2 object sebelumnya
                    else if (i >= 1)
                    {
                        if (Debug.isDebugBuild) { Debug.Log("masuk validasi i >= 1"); }
                        // Match-2 validation
                        if (_trashList[i].CompareTag(_trashList[i - 1].tag))
                        {
                            if (Debug.isDebugBuild) { Debug.Log("match-2 valid"); }
                            // Match-3 validation
                            if (i > 1 && _trashList[i].CompareTag(_trashList[i - 2].tag))
                            {
                                if (Debug.isDebugBuild) { Debug.Log("match-3 valid"); }
                                _matchedTrashList.Add(_trashList[i]);

                                // Match-3 bonus score
                                PublishSubscribe.Instance.Publish<MessageAddScore>(new MessageAddScore("Match3"));

                                for (int j = 0; j < _matchedTrashList.Count; j++)
                                {
                                    _trashList.RemoveAt(i - j);
                                    _indicators[i - j].color = Color.white;
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
                else
                {
                    // Buat indikator jadi merah
                    _indicators[i].color = Color.red;
                }
            }
            _matchedTrashList.Clear();
        }

        private void ClearTrashList(MessageClearTrashList message)
        {
            // Buat indikator jadi putih
            for (int i = 0; i < _trashList.Count; i++)
            {
                _indicators[i].color = Color.white;
            }

            _trashList.Clear();
            _matchedTrashList.Clear();
        }

        private void IncreaseTrashCanCapacity(MessageIncreaseTrashCanCapacity message)
        {
            if (_trashCanCapacity < _trashCanMaxCapacity)
            {
                // Buat indikator jadi putih
                _indicatorParentLayoutGroup.enabled = true;
                _indicators[_trashCanCapacity].gameObject.SetActive(true);
                LayoutRebuilder.ForceRebuildLayoutImmediate(_indicatorParentRectTransform);
                _indicatorParentLayoutGroup.enabled = false;

                _trashCanCapacity++;
            }
        }
    }
}