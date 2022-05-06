using System.Collections;
using Controllers;

namespace Managers
{
    public interface IScenesManager
    {
        IScene CreateScene<T>(string path, Hashtable args = null) where T : Scene;
        void HideScene<T>() where T : IScene;
    }
}