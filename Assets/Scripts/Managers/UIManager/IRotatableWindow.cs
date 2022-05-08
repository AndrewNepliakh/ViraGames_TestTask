using System;

namespace Managers
{
    public interface IRotatableWindow : ISwitchableButtonWindow
    {
        Action<int> OnChangeAmountRotationsValue { get; set; }
        Action<float> OnChangeRadiusValue { get; set; }
        Action<Direction> OnChangeDirectionValue { get; set; }
    }

    public enum Direction
    {
        Сlockwise,
        СounterСlockwise
    }
}