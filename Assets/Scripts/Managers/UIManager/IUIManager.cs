using System.Collections;

namespace Managers
{
    public interface IUIManager
    {
        public T ShowPanel<T>(string prefabPath, Hashtable args = null) where T : Panel;
        public T ShowWindow<T>(string prefabPath, Hashtable args = null) where T : Window;
        public void CloseWindow<T>() where T : Window;
    }
}