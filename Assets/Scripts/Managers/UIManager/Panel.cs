using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public abstract class Panel : MonoBehaviour, IPanel
    {
        public abstract void Show(Hashtable args);
    }
}