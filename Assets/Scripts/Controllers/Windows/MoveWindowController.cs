using System;
using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers.MainWindow
{
    public class MoveWindowController : Window, ISwitchableButtonWindow
    {
        public override void Show(Hashtable args)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _stateManager = args[Constants.STATE_MANAGER] as StateManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;
            _moveButtonAction = args[Constants.MOVE_BUTTON_ACTION] as UnityAction;

            _speedInputField.text = Constants.DEFAULT_CUBE_SPEED;
            
            _moveButton.onClick.AddListener(_moveButtonAction);
            _speedInputField.onEndEdit.AddListener(UpdateSpeed);
            _nextStateButton.onClick.AddListener(OnNextStateButtonClicked);
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
            var args = new Hashtable
            {
                {Constants.UI_MANAGER, _uiManager},
                {Constants.STATE_MANAGER, _stateManager},
                {Constants.SCENES_MANAGER, _scenesManager}
            };
            
            _stateManager.EnterState<RotateState>(args);
        }
    }
}