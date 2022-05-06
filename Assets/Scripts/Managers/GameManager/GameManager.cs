using System.Collections;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        [Inject] private IUIManager _uiManager;
        [Inject] private IUserManager _userManager;
        [Inject] private IStateManager _stateManager;
        [Inject] private IScenesManager _scenesManager;

        private void Start()
        {
            var args = new Hashtable
            {
                {Constants.UI_MANAGER, _uiManager},
                {Constants.USER_MANAGER, _userManager},
                {Constants.SCENES_MANAGER, _scenesManager}
            };
            
            _stateManager.EnterState<MoveState>(args);
        }

        private void Update()
        { 
            _stateManager.ActiveState.Update();
        }

        private void OnApplicationQuit()
        {
           SaveManager.Save(_userManager);
        }
    }
}
