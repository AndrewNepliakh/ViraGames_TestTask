using System;
using System.Collections;
using Controllers;
using Controllers.MainWindow;
using UnityEngine.Events;

namespace Managers
{
    public class RotateState : State
    {
        private IRotatingScene _rotatingScene;
        private IRotatingWindow _rotatingWindow;
        
        public override void Enter(Hashtable args = null)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _stateManager = args[Constants.STATE_MANAGER] as StateManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;
            
            _rotatingScene = _scenesManager.CreateScene<RotatingSceneController>(Constants.ROTATING_SCENE_PATH) as IRotatingScene;
            _rotatingWindow = _uiManager.ShowWindow<RotateWindowController>(Constants.ROTATE_WINDOW_PATH);
            _scene = _rotatingScene;

            Action<Hashtable> actionToSwitchButton = _rotatingWindow.SwitchActionButton;

            var rotatingSceneArgs = new Hashtable
            {
                {Constants.SWITCH_BUTTON_ACTION, actionToSwitchButton}
            };
            _rotatingScene.Init(rotatingSceneArgs);
            
            UnityAction moveAction = _rotatingScene.Cube.Rotate;

            var mainWindowArgs = new Hashtable
            {
                {Constants.MOVE_BUTTON_ACTION, moveAction},
                {Constants.STATE_MANAGER, _stateManager},
                {Constants.UI_MANAGER, _uiManager},
                {Constants.SCENES_MANAGER, _scenesManager}
            };
            _rotatingWindow.Show(mainWindowArgs);

            _rotatingWindow.OnChangeSpeedValue += _rotatingScene.Cube.SetSpeedRotation;
            _rotatingWindow.OnChangeRadiusValue += _rotatingScene.Cube.SetRadiusRotation;
            _rotatingWindow.OnChangeDirectionValue += _rotatingScene.Cube.SetDirectionsRotation;
            _rotatingWindow.OnChangeAmountRotationsValue += _rotatingScene.Cube.SetAmountRotations;

            _isStarted = true;
        }

        public override void Exit()
        {
            _isStarted = false;
            
            _rotatingWindow.OnChangeSpeedValue -= _rotatingScene.Cube.SetSpeedRotation;
            _rotatingWindow.OnChangeRadiusValue -= _rotatingScene.Cube.SetRadiusRotation;
            _rotatingWindow.OnChangeDirectionValue -= _rotatingScene.Cube.SetDirectionsRotation;
            _rotatingWindow.OnChangeAmountRotationsValue -= _rotatingScene.Cube.SetAmountRotations;
            
            _scenesManager.HideScene<RotatingSceneController>();
            _uiManager.CloseWindow<RotateWindowController>();
        }
    }
}