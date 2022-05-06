using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public abstract class Window : MonoBehaviour, IWindow
    {
        public virtual Action OnClose { get; set; }

        public virtual void Show(Hashtable args)
        {
        }
        
        public virtual void Close()
        {
            OnClose?.Invoke();
        }
    }
}