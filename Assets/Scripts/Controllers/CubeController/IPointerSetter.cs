using System;
using UnityEngine;

namespace Controllers.CubeController
{
    public interface IPointerSetter
    {
        event Action OnStartSetting;
        event Action OnCompleteSetting;
        void SetPointer(Vector3 positions);
    }
}