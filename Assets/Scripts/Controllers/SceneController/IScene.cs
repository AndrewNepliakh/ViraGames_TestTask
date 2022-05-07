using System;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public interface IScene
    {
        IMovable Cube { get; }
        GameObject GameObject { get; }
        void Init(Hashtable args);
        void Hide();
        void SetPointer();
    }
}