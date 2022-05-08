using System;
using System.Collections;
using Controllers;
using UnityEngine.Events;

namespace Managers
{
    public class RotateState : IState
    {
        private IUIManager _uiManager;
        private IScenesManager _scenesManager;

        private bool _isStarted;

        private IRotatingScene _rotatingScene;
        private ISwitchableButtonWindow _moveWindow;
        
        public void Enter(Hashtable args = null)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _scenesManager = args[Constants.SCENES_MANAGER] as ScenesManager;
            
            _rotatingScene = _scenesManager.CreateScene<RotatingSceneController>(Constants.ROTATING_SCENE_PATH) as IRotatingScene;
            _isStarted = true;
        }

        public void Exit()
        {
           
        }

        public void Update()
        {
           
        }
    }
}