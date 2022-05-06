using System.Collections;
using UnityEngine;

namespace Controllers
{
    public interface IScene
    {
        GameObject GameObject { get; }
        void Init(Hashtable args);
        void Hide();
        void SetPointer();
    }
}