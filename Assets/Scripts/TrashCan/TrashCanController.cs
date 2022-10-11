using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.Trash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.TrashCan
{
    public class TrashCanController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _trashList = new();
        [SerializeField] private List<GameObject> _matchedTrashList = new();
        private string _trashCanTag;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageClearTrashList>(ClearTrashList);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageClearTrashList>(ClearTrashList);
        }

        private void Start()
        {
            _trashCanTag = gameObject.tag.Substring(8);
        }

        private void OnCollisionEnter(Collision collision)
        {
            PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());

            if (_trashCanTag == collision.gameObject.tag.Substring(5))
            {
                PublishSubscribe.Instance.Publish<MessageAddScore>(new MessageAddScore("Normal"));
                _trashList.Add(collision.gameObject);
                CheckTrashListElements(collision.gameObject.tag);
            }
            else
            {
                _trashList.Add(collision.gameObject);
            }
        }

        private void CheckTrashListElements(string collidedGameObjectTag)
        {
            for (int i = 0; i < _trashList.Count; i++)
            {
                // Determine whether collidedGameObjectTag is the same as previous or next index of list
                if ((_trashList[i].CompareTag(collidedGameObjectTag)) &&
                    ((i != 0 && _trashList[i - 1].CompareTag(collidedGameObjectTag)) ||
                        (i != _trashList.Count - 1 && _trashList[i + 1].CompareTag(collidedGameObjectTag)))
                    )
                {
                    _matchedTrashList.Add(_trashList[i]);

                    // if Match-3
                    if (_matchedTrashList.Count == 3)
                    {
                        // Match-3 bonus score
                        PublishSubscribe.Instance.Publish<MessageAddScore>(new MessageAddScore("Match3"));

                        for (int j = 0; j < _matchedTrashList.Count; j++)
                        {
                            _matchedTrashList[j].GetComponent<TrashController>().StoreToPool();
                            _trashList.Remove(_matchedTrashList[j]);
                        }
                        _matchedTrashList.Clear();
                    }
                }
                else
                {
                    _matchedTrashList.Clear();
                }
            }
        }

        private void ClearTrashList(MessageClearTrashList message)
        {
            _trashList.Clear();
            _matchedTrashList.Clear();
        }
    }
}