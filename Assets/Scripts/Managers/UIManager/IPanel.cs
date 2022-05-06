using System;
using System.Collections;

namespace Managers
{
    public interface IPanel
    {
        Action OnPanelClick { get; set; }
        void Show(Hashtable args);
    }
}