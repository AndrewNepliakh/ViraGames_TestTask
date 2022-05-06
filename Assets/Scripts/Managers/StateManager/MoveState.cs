using System;
using System.Collections;
using Controllers;
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

        public void Enter(Hashtable args)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _userManager = args[Constants.USER_MANAGER] as UserManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;

            _isStarted = true;

            try
            {
                if (_scenesManager != null)
                    _movingScene = _scenesManager.CreateScene<MovingSceneController>(Constants.MOVING_SCENE_PATH);
            }
            catch (NullReferenceException e)
            {
                Debug.LogError($"Enter initialise is failed, args is null : State {typeof(MoveState)}");
            }
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            if (!_isStarted) return;
        }
    }
}