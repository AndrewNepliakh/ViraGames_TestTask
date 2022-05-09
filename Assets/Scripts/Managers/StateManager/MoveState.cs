using System;
using System.Collections;
using Controllers;
using Controllers.MainWindow;
using UnityEngine.Events;

namespace Managers
{
    public class MoveState : State
    {
        private IMovingScene _movingScene;
        private ISwitchableButtonWindow _moveWindow;

        public override void Enter(Hashtable args)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _stateManager = args[Constants.STATE_MANAGER] as StateManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;

            _movingScene = _scenesManager.CreateScene<MovingSceneController>(Constants.MOVING_SCENE_PATH) as IMovingScene;
            _moveWindow = _uiManager.ShowWindow<MoveWindowController>(Constants.MOVE_WINDOW_PATH);
            _scene = _movingScene;
            
            Action<Hashtable> startAction = _moveWindow.SwitchActionButton;

            var movingSceneArgs = new Hashtable
            {
                {Constants.START_POINTERS_SETTINGS_ACTION, startAction},
                {Constants.COMPLETE_POINTERS_SETTINGS_ACTION, startAction}
            };
            _movingScene.Init(movingSceneArgs);
            
            UnityAction moveAction = _movingScene.Cube.Move;

            var mainWindowArgs = new Hashtable
            {
                {Constants.MOVE_BUTTON_ACTION, moveAction},
                {Constants.STATE_MANAGER, _stateManager},
                {Constants.UI_MANAGER, _uiManager},
                {Constants.SCENES_MANAGER, _scenesManager},
            };
            _moveWindow.Show(mainWindowArgs);

            _moveWindow.OnChangeSpeedValue += _movingScene.Cube.SetSpeed;
            
            _isStarted = true;
        }

        public override void Exit()
        {
            _isStarted = false;
            
            _moveWindow.OnChangeSpeedValue -= _movingScene.Cube.SetSpeed;
            
            _scenesManager.HideScene<MovingSceneController>();
            _uiManager.CloseWindow<MoveWindowController>();
        }
    }
}