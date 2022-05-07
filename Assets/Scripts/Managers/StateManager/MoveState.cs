using System;
using System.Collections;
using Controllers;
using Controllers.MainWindow;
using UnityEngine.Events;

namespace Managers
{
    public class MoveState : IState
    {
        private IUIManager _uiManager;
        private IUserManager _userManager;
        private IScenesManager _scenesManager;

        private bool _isStarted;

        private IScene _movingScene;
        private ISwitchableButtonWindow _mainWindow;

        public void Enter(Hashtable args)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _userManager = args[Constants.USER_MANAGER] as UserManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;
            
            _movingScene = _scenesManager.CreateScene<MovingSceneController>(Constants.MOVING_SCENE_PATH);
            _mainWindow = _uiManager.ShowWindow<MainWindowController>(Constants.MAIN_WINDOW_PATH);

            
            Action<Hashtable> startAction = _mainWindow.SwitchMoveButton;

            var movingSceneArgs = new Hashtable
            {
                {Constants.START_POINTERS_SETTINGS_ACTION, startAction},
                {Constants.COMPLETE_POINTERS_SETTINGS_ACTION, startAction}
            };
            _movingScene.Init(movingSceneArgs);
            
            UnityAction moveAction = _movingScene.Cube.Move;

            var mainWindowArgs = new Hashtable
            {
                { Constants.MOVE_BUTTON_ACTION, moveAction}
            };
            _mainWindow.Show(mainWindowArgs);
            
            _isStarted = true;
        }

        public void Exit()
        {
        }

        public void Update()
        {
            if (!_isStarted) return;
            
            _movingScene.SetPointer();
        }
    }
}