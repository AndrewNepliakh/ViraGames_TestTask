using System.Collections;

namespace Managers
{
    public class GameState : IState
    {
        private IUIManager _uiManager;
        private IUserManager _userManager;

        private bool _isStarted;

        public void Enter(Hashtable args)
        {
            _uiManager = args[Constants.UI_MANAGER] as UIManager;
            _userManager = args[Constants.USER_MANAGER] as UserManager;

            _isStarted = true;
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