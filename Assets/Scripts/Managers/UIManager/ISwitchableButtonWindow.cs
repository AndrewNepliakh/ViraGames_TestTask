using System;
using System.Collections;

namespace Managers
{
    public interface ISwitchableButtonWindow : IWindow
    {
        Action<int> OnChangeSpeedValue { get; set; }
        void SwitchActionButton(Hashtable args);
    }
}