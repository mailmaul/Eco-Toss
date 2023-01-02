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
        //[SerializeField] private TMP_Text _alternateTMP;

        private Action _onConfirmAction;
        private Action _onDeclineAction;
        private Action _onAlternateAction;

        public void Confirm()
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));
            _onConfirmAction?.Invoke();
        }

        public void Decline()
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));
            _onDeclineAction?.Invoke();
        }

        public void Alternate()
        {
            PublishSubscribe.Instance.Publish<MessagePlaySFX>(new MessagePlaySFX("ui_button"));
            _onAlternateAction?.Invoke();
        }

        public void Show(TextAnchor alignment, string title, Sprite imageToShow, string message, string confirmButtonText, string declineButtonText, Action confirmAction, Action declineAction, Action alternateAction = null)
        {
            //if (IsVertical)
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
            bool hasNoConfirmButtonText = string.IsNullOrEmpty(confirmButtonText);
            if (hasNoConfirmButtonText)
            {
                _confirmTMP.SetText("Next");
            }
            else
            {
                _confirmTMP.SetText(confirmButtonText);
            }

            bool hasDecline = (declineAction != null);
            _declineButton.gameObject.SetActive(hasDecline);
            _onDeclineAction = declineAction;
            bool hasNoDeclineButtonText = string.IsNullOrEmpty(declineButtonText);
            if (hasNoDeclineButtonText)
            {
                _declineTMP.SetText("Prev");
            }
            else
            {
                _declineTMP.SetText(declineButtonText);
            }

            bool hasAlternate = (alternateAction != null);
            _alternateButton.gameObject.SetActive(hasAlternate);
            _onAlternateAction = alternateAction;
            //bool hasNoAlternateButtonText = string.IsNullOrEmpty(alternateButtonText);
            //if (hasNoAlternateButtonText)
            //{
            //    _alternateTMP = default;
            //}
            //else
            //{
            //    _alternateTMP.SetText(declineButtonText);
            //}

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