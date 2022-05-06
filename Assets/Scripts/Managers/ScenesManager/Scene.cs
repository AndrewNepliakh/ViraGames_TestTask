using System.Collections;
using Controllers;
using UnityEngine;

namespace Managers
{
    public abstract class Scene : MonoBehaviour, IScene
    {
        public GameObject GameObject => gameObject;
        public abstract void Init(Hashtable args);
        public abstract void Hide();

    }
}