using System;
using System.Collections;
using Controllers;
using Controllers.MainWindow;
using UnityEngine.Events;

namespace Managers
{
    public class SpiralState : State
    {
        private ISpiralScene _spiralScene;
        private ISpiralWindow _spiralWindow;
        
        public override void Enter(Hashtable args = null)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _stateManager = args[Constants.STATE_MANAGER] as StateManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;
            
            _spiralScene = _scenesManager.CreateScene<SpiralSceneController>(Constants.SPIRAL_SCENE_PATH) as ISpiralScene;
            _spiralWindow = _uiManager.ShowWindow<SpiralWindowController>(Constants.SPIRAL_WINDOW_PATH);
            _scene = _spiralScene;

            Action<Hashtable> actionToSwitchButton = _spiralWindow.SwitchActionButton;

            var rotatingSceneArgs = new Hashtable
            {
                {Constants.SWITCH_BUTTON_ACTION, actionToSwitchButton}
            };
            _spiralScene.Init(rotatingSceneArgs);
            
            UnityAction moveAction = _spiralScene.Cube.Spiral;

            var mainWindowArgs = new Hashtable
            {
                {Constants.MOVE_BUTTON_ACTION, moveAction},
                {Constants.STATE_MANAGER, _stateManager},
                {Constants.UI_MANAGER, _uiManager},
                {Constants.SCENES_MANAGER, _scenesManager}
            };
            _spiralWindow.Show(mainWindowArgs);

            _spiralWindow.OnChangeSpeedValue += _spiralScene.Cube.SetSpeed;
            _spiralWindow.OnChangeStepLoopsValue += _spiralScene.Cube.SetStepLoops;
            _spiralWindow.OnChangeAmountLoopsValue += _spiralScene.Cube.SetAmountLoops;
            _spiralWindow.OnChangeDirectionValue += _spiralScene.Cube.SetSpiralDirection;

            _isStarted = true;
        }

        public override void Exit()
        {
            _spiralWindow.OnChangeSpeedValue -= _spiralScene.Cube.SetSpeed;
            _spiralWindow.OnChangeStepLoopsValue -= _spiralScene.Cube.SetStepLoops;
            _spiralWindow.OnChangeAmountLoopsValue -= _spiralScene.Cube.SetAmountLoops;
            _spiralWindow.OnChangeDirectionValue -= _spiralScene.Cube.SetSpiralDirection;
        }
    }
}