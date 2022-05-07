using UnityEngine;

namespace Controllers
{
    public interface IPointer
    {
        GameObject GameObject { get; }
        void SetPosition(Vector3 position, Color color, int count);
    }
}