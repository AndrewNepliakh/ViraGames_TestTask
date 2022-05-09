using System;
using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers.MainWindow
{
    public class RotateWindowController :  Window, IRotatableWindow
    {
        public Action<int> OnChangeAmountRotationsValue { get; set; }
        public Action<float> OnChangeRadiusValue { get; set; }
        public Action<Direction> OnChangeDirectionValue { get; set; }
        
        [SerializeField] protected TMP_InputField _amountInputField;
        [SerializeField] protected TMP_InputField _radiusInputField;
        [SerializeField] protected TMP_Dropdown _directionInputField;
        
        public override void Show(Hashtable args)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _stateManager = args[Constants.STATE_MANAGER] as StateManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;
            _moveButtonAction = args[Constants.MOVE_BUTTON_ACTION] as UnityAction;

            _amountInputField.text = Constants.DEFAULT_CUBE_AMOUNT_ROTATION;
            _speedInputField.text = Constants.DEFAULT_CUBE_SPEED_ROTATION;
            _radiusInputField.text = Constants.DEFAULT_CUBE_RADIUS;
            
            _moveButton.onClick.AddListener(_moveButtonAction);
            _amountInputField.onEndEdit.AddListener(UpdateAmountRotation);
            _speedInputField.onEndEdit.AddListener(UpdateSpeed);
            _radiusInputField.onEndEdit.AddListener(UpdateRadius);
            _directionInputField.onValueChanged.AddListener(UpdateDirection);
            _nextStateButton.onClick.AddListener(OnNextStateButtonClicked);
        }

        public void SwitchActionButton(Hashtable args)
        {
            var state = args[Constants.SWITCH_BUTTON_ACTION] as bool?;
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

        private void UpdateRadius(string value)
        {
            if (float.TryParse(value, out var result))
            {
                OnChangeRadiusValue?.Invoke(result);
            }
        }

        private void UpdateAmountRotation(string value)
        {
            if (int.TryParse(value, out var result))
            {
                OnChangeAmountRotationsValue?.Invoke(result);
            }
        }

        private void UpdateDirection(int value)
        {
            OnChangeDirectionValue?.Invoke((Direction)value);
        }

        private void OnNextStateButtonClicked()
        {
            var args = new Hashtable
            {
                {Constants.UI_MANAGER, _uiManager},
                {Constants.STATE_MANAGER, _stateManager},
                {Constants.SCENES_MANAGER, _scenesManager}
            };
            
            _stateManager.EnterState<SpiralState>(args);
        }
    }
}