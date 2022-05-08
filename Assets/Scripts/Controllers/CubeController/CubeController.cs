
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public abstract class CubeController : MonoBehaviour, ICube
    {
        public event Action OnStartAction;
        public event Action OnEndAction;

        public GameObject GameObject => gameObject;
        public abstract void Init(Hashtable args);
    }
}
