
using System;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public abstract class CubeController : MonoBehaviour, ICube
    {
        public bool IsInAction { get; set; }
        public Action<Hashtable> OnStartAction { get; set; }
        public Action<Hashtable> OnEndAction { get; set; }
        public GameObject GameObject => gameObject;
        public abstract void Init(Hashtable args);
    }
}
