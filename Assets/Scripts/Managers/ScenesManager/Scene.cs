using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Managers
{
    public abstract class Scene : MonoBehaviour, IScene
    {
        public abstract IMovable Cube { get; }
        public GameObject GameObject => gameObject;
        public abstract void Init(Hashtable args);
        public abstract void Hide();
        public abstract void SetPointer();

    }
}