using System;
using System.Collections;
using System.Collections.Generic;

namespace Managers
{
    public class StateManager : IStateManager
    {
        public IState ActiveState => _activeState;
        
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public StateManager()
        {
            _states = new Dictionary<Type, IState>
            {
                {typeof(InitialState), new InitialState()},
                {typeof(MoveState), new MoveState()},
                {typeof(RotateState), new RotateState()},
                {typeof(SpiralState), new SpiralState()},
            };
        }

        public IState EnterState<T>(Hashtable args = null) where T : IState
        {
            _activeState?.Exit();
            var state = _states[typeof(T)];
            _activeState = state;
            state.Enter(args);
            return state;
        }
    }
}