using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Zenject;

namespace Managers
{
    public class InitialState : IState
    {
        private UserManager _userManager;

        public void Enter(Hashtable args)
        {
            _userManager = args[Constants.USER_MANAGER] as UserManager;
            LoadUserData();
            SceneManager.LoadScene(Constants.MAIN_SCENE);
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }

        private void LoadUserData()
        {
            var saveData = SaveManager.Load();
            _userManager.Init(saveData.UserData);
        }
    }
}