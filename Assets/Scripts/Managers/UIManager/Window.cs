using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public abstract class Window : MonoBehaviour, IWindow
    {
        public abstract Action OnClose { get; set; }
        public abstract void Show(Hashtable args);
        public abstract void Close();
    }
}