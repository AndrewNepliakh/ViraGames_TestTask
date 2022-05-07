using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public interface IPointerSetter
    {
        event Action<Hashtable> OnStartSetting;
        event Action<Hashtable> OnCompleteSetting;
        void SetPointer(Vector3 positions);
    }
}