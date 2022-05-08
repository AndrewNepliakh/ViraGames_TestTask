using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Managers
{
    public abstract class Window : MonoBehaviour, IWindow
    {
        public Action<int> OnChangeSpeedValue { get; set; }

        [SerializeField] protected Button _moveButton;
        [SerializeField] protected Button _nextStateButton;
        
        [SerializeField] protected TextMeshProUGUI _text;
        [SerializeField] protected TMP_InputField _speedInputField;

        protected Color _allowedColor = new Color(0.5f, 1.0f, 0.5f);
        protected Color _deniedColor =  new Color(1.0f, 0.5f, 0.5f);
        
        protected UnityAction _moveButtonAction;

        protected IUIManager _uiManager;
        protected IStateManager _stateManager;
        protected IScenesManager _scenesManager;
        public Action OnClose { get; set; }
        public abstract void Show(Hashtable args);
        public abstract void Close();
    }
}