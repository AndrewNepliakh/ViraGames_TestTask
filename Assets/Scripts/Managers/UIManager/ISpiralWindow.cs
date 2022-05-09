using System;
using Controllers;

namespace Managers
{
    public interface ISpiralWindow : IRotatingWindow
    {
        Action<float> OnChangeStepLoopsValue { get; set; }
        Action<int> OnChangeAmountLoopsValue { get; set; }
    }
}