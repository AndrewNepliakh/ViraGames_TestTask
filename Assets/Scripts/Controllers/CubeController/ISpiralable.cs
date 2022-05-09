using Managers;
using UnityEngine.UI;

namespace Controllers
{
    public interface ISpiralable : ICube
    {
        void Spiral();
        void SetStepLoops(float obj);
        void SetSpeed(int obj);
        void SetAmountLoops(int obj);
        void SetSpiralDirection(Direction obj);
    }
}