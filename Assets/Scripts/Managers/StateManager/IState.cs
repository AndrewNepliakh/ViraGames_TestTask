using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public interface IState
    {
        public void Enter(Hashtable args = null);
        public void Exit();
        public void Update();
    }
}
