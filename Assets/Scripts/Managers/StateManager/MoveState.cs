using System;
using System.Collections;
using Controllers;
using Controllers.CubeController;
using Controllers.MainWindow;
using UnityEngine;

namespace Managers
{
    public class MoveState : IState
    {
        private IUIManager _uiManager;
        private IUserManager _userManager;
        private IScenesManager _scenesManager;

        private bool _isStarted;

        private IScene _movingScene;
        private IWindow _mainWindow;

        public void Enter(Hashtable args)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _userManager = args[Constants.USER_MANAGER] as UserManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;

            _isStarted = true;
            
            _movingScene = _scenesManager.CreateScene<MovingSceneController>(Constants.MOVING_SCENE_PATH);
            _mainWindow = _uiManager.ShowWindow<MainWindowController>(Constants.MAIN_WINDOW_PATH);
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