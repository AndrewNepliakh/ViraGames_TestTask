using System.Collections;
using Controllers;

namespace Managers
{
    public abstract class State : IState
    {
        protected IUIManager _uiManager;
        protected IStateManager _stateManager;
        protected IScenesManager _scenesManager;

        protected bool _isStarted;

        protected IScene _scene;
        
        public abstract void Enter(Hashtable args = null);
        public abstract void Exit();

        public virtual void Update()
        {
            if (!_isStarted) return;
            _scene.SetPointer();
        }
    }
}