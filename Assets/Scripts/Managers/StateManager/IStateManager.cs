using System.Collections;

namespace Managers
{
    public interface IStateManager
    {
        IState ActiveState { get; }
        IState EnterState<T>(Hashtable args = null) where T : IState;
    }
}