using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public abstract class Panel : MonoBehaviour, IPanel
    {
        public virtual Action OnPanelClick { get; set; }
        public virtual void Show(Hashtable args){}
    }
}