using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EcoTeam.EcoToss.WindArea
{
    public class WindUIController : MonoBehaviour
    {
        private Color _baseImageColor;
        [SerializeField] private RectTransform _arrowTransform;
        [SerializeField] private TMP_Text _strengthtext;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageShowWindProperties>(SetUI);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageShowWindProperties>(SetUI);
        }

        private void Start()
        {
            _baseImageColor = GetComponent<Image>().color;
            _baseImageColor = Color.white;
        }

        public void SetUI(MessageShowWindProperties msg)
        {
            _arrowTransform.gameObject.SetActive(true);
            _strengthtext.gameObject.SetActive(true);

            if (msg.Direction == "Left")
            {
                _arrowTransform.localScale = new Vector3(-1, 1, 1);
            }
            else if (msg.Direction == "Right")
            {
                _arrowTransform.localScale = new Vector3(1, 1, 1);
            }

            _strengthtext.SetText(msg.Strength.ToString());
        }
    }
}
