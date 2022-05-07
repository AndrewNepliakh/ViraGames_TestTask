using System;
using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Controllers.MainWindow
{
    public class MainWindowController : Window, ISwitchableButtonWindow
    {
        [SerializeField] private Button _moveButton;
        [SerializeField] private TextMeshProUGUI _text;

        private Color _allowedColor = new Color(0.5f, 1.0f, 0.5f);
        private Color _deniedColor =  new Color(1.0f, 0.5f, 0.5f);
        
        private UnityAction _moveButtonAction;
        public override Action OnClose { get; set; }
        public override void Show(Hashtable args)
        {
            _moveButtonAction = args[Constants.MOVE_BUTTON_ACTION] as UnityAction;
            _moveButton.onClick.AddListener(_moveButtonAction);
        }

        public void SwitchMoveButton(Hashtable args)
        {
            var state = args[Constants.MOVE_BUTTON_STATE] as bool?;
            if (state != null)
            {
                _moveButton.interactable = state.Value;
                _moveButton.image.color = state.Value ? _allowedColor : _deniedColor;
                _text.gameObject.SetActive(state.Value);
            }
        }

        public override void Close()
        {
            _moveButton.onClick.AddListener(_moveButtonAction);
            OnClose?.Invoke();
        }
    }
}