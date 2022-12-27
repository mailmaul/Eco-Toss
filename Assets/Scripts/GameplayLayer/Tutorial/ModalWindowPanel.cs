using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EcoTeam.EcoToss.Tutorial
{
    public class ModalWindowPanel : MonoBehaviour
    {
        [Header("Parent")]
        [SerializeField] private VerticalLayoutGroup _modalWindowPanel;

        [Header("Header")]
        [SerializeField] private Transform _headerArea;
        [SerializeField] private TextMeshProUGUI _HeaderTMP;

        [Header("Content")]
        [SerializeField] private Transform _bodyArea;
        [Space()]
        [SerializeField] private Transform _horizontalLayoutArea;
        [SerializeField] private Image _horizontalBodyImage;
        [SerializeField] private TextMeshProUGUI _horizontalBodyTMP;
        [Space()]
        [SerializeField] private Transform _verticalLayoutArea;
        [SerializeField] private Image _verticalBodyImage;
        [SerializeField] private TextMeshProUGUI _verticalBodyTMP;

        [Header("Footer")]
        [SerializeField] private Transform _footerArea;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private TextMeshProUGUI _confirmTMP;
        [SerializeField] private Button _declineButton;
        [SerializeField] private TextMeshProUGUI _declineTMP;
        [SerializeField] private Button _alternateButton;
        [SerializeField] private TextMeshProUGUI _alternateTMP;

        private Action _onConfirmAction;
        private Action _onDeclineAction;
        private Action _onAlternateAction;

        public void Confirm()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));
            _onConfirmAction?.Invoke();
            //Close();
        }

        public void Decline()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));
            _onDeclineAction?.Invoke();
            //Close();
        }

        public void Alternate()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));
            _onAlternateAction?.Invoke();
            //Close();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Show(string title, Sprite imageToShow, string message, string confirmMessage, string declineMessage, string alternateMessage, Action confirmAction, Action declineAction, Action alternateAction = null)
        {
            //if (message.IsVertical)
            //{
            //    _horizontalLayoutArea.gameObject.SetActive(false);
            //    _verticalLayoutArea.gameObject.SetActive(true);
            //    _verticalBodyImage.sprite = message.ImageToShow;
            //    _verticalBodyTMP.SetText(message.Message);
            //}
            //else
            //{
            //    _horizontalLayoutArea.gameObject.SetActive(true);
            //    _verticalLayoutArea.gameObject.SetActive(false);
            //    _horizontalBodyImage.sprite = message.ImageToShow;
            //    _horizontalBodyTMP.SetText(message.Message);
            //}

            _horizontalLayoutArea.gameObject.SetActive(false);

            //bool hasTitle = string.IsNullOrEmpty(title);
            //_headerArea.gameObject.SetActive(hasTitle);
            _HeaderTMP.SetText(title);

            _verticalBodyImage.sprite = imageToShow;
            _verticalBodyTMP.SetText(message);

            _onConfirmAction = confirmAction;
            _confirmTMP.SetText(confirmMessage);

            bool hasDecline = (declineAction != null);
            _declineButton.gameObject.SetActive(hasDecline);
            _declineTMP.SetText(declineMessage);
            _onDeclineAction = declineAction;

            bool hasAlternate = (alternateAction != null);
            _alternateButton.gameObject.SetActive(hasAlternate);
            _alternateTMP.SetText(alternateMessage);
            _onAlternateAction = alternateAction;
        }


        public void Show(TextAnchor alignment, string title, Sprite imageToShow, string message, Action confirmAction, Action declineAction, Action alternateAction = null)
        {
            //if (message.IsVertical)
            //{
            //    _horizontalLayoutArea.gameObject.SetActive(false);
            //    _verticalLayoutArea.gameObject.SetActive(true);
            //    _verticalBodyImage.sprite = message.ImageToShow;
            //    _verticalBodyTMP.SetText(message.Message);
            //}
            //else
            //{
            //    _horizontalLayoutArea.gameObject.SetActive(true);
            //    _verticalLayoutArea.gameObject.SetActive(false);
            //    _horizontalBodyImage.sprite = message.ImageToShow;
            //    _horizontalBodyTMP.SetText(message.Message);
            //}

            _horizontalLayoutArea.gameObject.SetActive(false);

            bool hasNoTitle = string.IsNullOrEmpty(title);
            _headerArea.gameObject.SetActive(!hasNoTitle);
            _HeaderTMP.SetText(title);

            bool hasImage = (imageToShow != null);
            _verticalBodyImage.gameObject.SetActive(hasImage);
            _verticalBodyImage.sprite = imageToShow;
            _verticalBodyTMP.SetText(message);

            bool hasConfirm = (confirmAction != null);
            _confirmButton.gameObject.SetActive(hasConfirm);
            _onConfirmAction = confirmAction;
            //_confirmTMP.SetText(confirmMessage);

            bool hasDecline = (declineAction != null);
            _declineButton.gameObject.SetActive(hasDecline);
            _onDeclineAction = declineAction;
            //_declineTMP.SetText(declineMessage);

            bool hasAlternate = (alternateAction != null);
            _alternateButton.gameObject.SetActive(hasAlternate);
            _onAlternateAction = alternateAction;
            //_alternateTMP.SetText(alternateMessage);

            if (!hasConfirm && !hasDecline && !hasAlternate)
            {
                _footerArea.gameObject.SetActive(false);
            }
            else
            {
                _footerArea.gameObject.SetActive(true);
            }

            _modalWindowPanel.childAlignment = alignment;
        }
    }
}