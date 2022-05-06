using System;
using System.Collections;

namespace Managers
{
    public interface IWindow
    {
        Action OnClose { get; set; }
        void Show(Hashtable args);
        void Close();
    }
}