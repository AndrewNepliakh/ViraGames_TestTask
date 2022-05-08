using System;
using System.Collections;
using Controllers;
using Controllers.MainWindow;
using UnityEngine.Events;

namespace Managers
{
    public class RotateState : IState
    {
        private IUIManager _uiManager;
        private IStateManager _stateManager;
        private IScenesManager _scenesManager;

        private bool _isStarted;

        private IRotatingScene _rotatingScene;
        private IRotatableWindow _rotateWindow;
        
        public void Enter(Hashtable args = null)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _stateManager = args[Constants.STATE_MANAGER] as StateManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;
            
            _rotatingScene = _scenesManager.CreateScene<RotatingSceneController>(Constants.ROTATING_SCENE_PATH) as IRotatingScene;
            _rotateWindow = _uiManager.ShowWindow<RotateWindowController>(Constants.ROTATE_WINDOW_PATH) as IRotatableWindow;
            
            Action<Hashtable> startAction = _rotateWindow.SwitchMoveButton;
                
            var rotatingSceneArgs = new Hashtable
            {
                {Constants.START_POINTERS_SETTINGS_ACTION, startAction},
                {Constants.COMPLETE_POINTERS_SETTINGS_ACTION, startAction}
            };
            _rotatingScene.Init(rotatingSceneArgs);
            
            UnityAction moveAction = _rotatingScene.Cube.Rotate;

            var mainWindowArgs = new Hashtable
            {
                {Constants.MOVE_BUTTON_ACTION, moveAction},
                {Constants.STATE_MANAGER, _stateManager},
                {Constants.UI_MANAGER, _uiManager},
                {Constants.SCENES_MANAGER, _scenesManager},
            };
            _rotateWindow.Show(mainWindowArgs);

            _rotateWindow.OnChangeSpeedValue += _rotatingScene.Cube.SetSpeedRotation;
            _rotateWindow.OnChangeRadiusValue += _rotatingScene.Cube.SetRadiusRotation;
            _rotateWindow.OnChangeDirectionValue += _rotatingScene.Cube.SetDirectionsRotation;
            _rotateWindow.OnChangeAmountRotationsValue += _rotatingScene.Cube.SetAmountRotations;

            _isStarted = true;
        }

        public void Exit()
        {
            _rotateWindow.OnChangeSpeedValue -= _rotatingScene.Cube.SetSpeedRotation;
            _rotateWindow.OnChangeRadiusValue -= _rotatingScene.Cube.SetRadiusRotation;
            _rotateWindow.OnChangeDirectionValue -= _rotatingScene.Cube.SetDirectionsRotation;
            _rotateWindow.OnChangeAmountRotationsValue -= _rotatingScene.Cube.SetAmountRotations;
        }

        public void Update()
        {
            if (!_isStarted) return;
            _rotatingScene.SetPointer();
        }
    }
}