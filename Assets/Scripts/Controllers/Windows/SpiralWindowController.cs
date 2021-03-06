using System;
using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers.MainWindow
{
    public class SpiralWindowController : Window, ISpiralWindow
    {
        public Action<float> OnChangeStepLoopsValue { get; set; }
        public Action<int> OnChangeAmountLoopsValue { get; set; }
        public Action<int> OnChangeAmountRotationsValue { get; set; }
        public Action<float> OnChangeRadiusValue { get; set; }
        public Action<Direction> OnChangeDirectionValue { get; set; }
        
        [SerializeField] protected TMP_InputField _amountLoopsInputField;
        [SerializeField] protected TMP_InputField _stepLoopsInputField;
        [SerializeField] protected TMP_Dropdown _directionInputField;

        public override void Show(Hashtable args)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _stateManager = args[Constants.STATE_MANAGER] as StateManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;
            _moveButtonAction = args[Constants.MOVE_BUTTON_ACTION] as UnityAction;

            _amountLoopsInputField.text = Constants.DEFAULT_CUBE_LOOPS_AMOUNT;
            _speedInputField.text = Constants.DEFAULT_CUBE_SPEED_ROTATION;
            _stepLoopsInputField.text = Constants.DEFAULT_CUBE_STEP_LOOPS_AMOUNT;
            
            _moveButton.onClick.AddListener(_moveButtonAction);
            _amountLoopsInputField.onEndEdit.AddListener(UpdateAmountLoops);
            _speedInputField.onEndEdit.AddListener(UpdateSpeed);
            _stepLoopsInputField.onEndEdit.AddListener(UpdateStepLoops);
            _directionInputField.onValueChanged.AddListener(UpdateDirection);
            _nextStateButton.onClick.AddListener(OnNextStateButtonClicked);
        }

        public override void Close()
        {
            _moveButton.onClick.RemoveListener(_moveButtonAction);
            _amountLoopsInputField.onEndEdit.RemoveListener(UpdateAmountLoops);
            _speedInputField.onEndEdit.RemoveListener(UpdateSpeed);
            _stepLoopsInputField.onEndEdit.RemoveListener(UpdateStepLoops);
            _directionInputField.onValueChanged.RemoveListener(UpdateDirection);
            _nextStateButton.onClick.RemoveListener(OnNextStateButtonClicked);
            
           OnClose?.Invoke();
        }
        
        private void UpdateAmountLoops(string value)
        {
            if (int.TryParse(value, out var result))
            {
                OnChangeAmountLoopsValue?.Invoke(result);
            }
        }
        
        private void UpdateSpeed(string value)
        {
            if (int.TryParse(value, out var result))
            {
                OnChangeSpeedValue?.Invoke(result);
            }
        }

        private void UpdateStepLoops(string value)
        {
            if (float.TryParse(value, out var result))
            {
                OnChangeStepLoopsValue?.Invoke(result);
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
            
            _stateManager.EnterState<MoveState>(args);
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
    }
}