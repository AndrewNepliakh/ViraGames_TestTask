using UnityEngine;

namespace Controllers.CubeController
{
    public interface IPointer
    {
        void SetPosition(Vector3 position, Color color);
    }
}