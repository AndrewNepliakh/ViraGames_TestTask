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
        public Action<int> OnChangeSpeedValue { get; set; }

        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _nextStateButton;
        
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TMP_InputField _speedInputField;

        private Color _allowedColor = new Color(0.5f, 1.0f, 0.5f);
        private Color _deniedColor =  new Color(1.0f, 0.5f, 0.5f);
        
        private UnityAction _moveButtonAction;

        private IStateManager _stateManager;
        public override Action OnClose { get; set; }
        public override void Show(Hashtable args)
        {
            _moveButtonAction = args[Constants.MOVE_BUTTON_ACTION] as UnityAction;
            _stateManager = args[Constants.STATE_MANAGER] as StateManager;

            _moveButton.onClick.AddListener(_moveButtonAction);
            _nextStateButton.onClick.AddListener(OnNextStateButtonClicked);
            _speedInputField.text = Constants.DEFAULT_CUBE_SPEED;
            _speedInputField.onEndEdit.AddListener(UpdateSpeed);
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
            _moveButton.onClick.RemoveListener(_moveButtonAction);
            _speedInputField.onEndEdit.RemoveListener(UpdateSpeed);
            _nextStateButton.onClick.RemoveListener(OnNextStateButtonClicked);
            OnClose?.Invoke();
        }

        private void UpdateSpeed(string value)
        {
            if (int.TryParse(value, out var result))
            {
                OnChangeSpeedValue?.Invoke(result);
            }
        }

        private void OnNextStateButtonClicked()
        {
            _stateManager.EnterState<RoundState>();
        }
    }
}