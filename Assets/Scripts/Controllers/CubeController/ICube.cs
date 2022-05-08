using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public interface ICube
    {
        event Action OnStartAction;
        event Action OnEndAction;
        GameObject GameObject { get; }
        void Init(Hashtable args);
    }
}