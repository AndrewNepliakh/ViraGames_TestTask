using System.Collections;
using Managers;
using UnityEngine;
using Zenject;

public class GameEnterPoint : MonoBehaviour
{
    [Inject] private IStateManager _stateManager;
    [Inject] private IUserManager _userManager;
    
    private void Awake()
    {
        var args = new Hashtable {{Constants.USER_MANAGER, _userManager}};
        _stateManager.EnterState<InitialState>(args);
    }
}