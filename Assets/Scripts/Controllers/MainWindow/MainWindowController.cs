using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Controllers.MainWindow
{
    public class MainWindowController : Window
    {
        [SerializeField] private Button _moveButton;
        private UnityAction _moveButtonAction;
        public override Action OnClose { get; set; }
        public override void Show(Hashtable args)
        {
            _moveButtonAction = args[Constants.MOVE_BUTTON_ACTION] as UnityAction;
            _moveButton.onClick.AddListener(_moveButtonAction);
        }

        public override void Close()
        {
            _moveButton.onClick.AddListener(_moveButtonAction);
            OnClose?.Invoke();
        }
    }
}