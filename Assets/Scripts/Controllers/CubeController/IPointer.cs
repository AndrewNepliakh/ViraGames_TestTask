using UnityEngine;

namespace Controllers
{
    public interface IPointer
    {
        void SetPosition(Vector3 position, Color color);
    }
}