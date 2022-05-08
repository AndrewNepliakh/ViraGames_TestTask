using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public interface ICube
    {
        bool IsInAction { get; set; }
        Action<Hashtable> OnStartAction { get; set; }
        Action<Hashtable> OnEndAction { get; set; }
        GameObject GameObject { get; }
        void Init(Hashtable args);
    }
}