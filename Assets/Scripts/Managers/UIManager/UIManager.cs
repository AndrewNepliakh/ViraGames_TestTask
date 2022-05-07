using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private Transform _mainCanvas;

        private readonly Dictionary<Type,Window> _windows = new Dictionary<Type,Window>();

        public T ShowPanel<T>(string prefabPath, Hashtable args = null) where T : Panel
        {
            var panelPrefab = Resources.Load<Panel>(prefabPath);
            var panel = Instantiate(panelPrefab, _mainCanvas) as T;
            if(args != null) panel.Show(args);
            return panel;
        }

        public T ShowWindow<T>(string prefabPath, Hashtable args) where T : Window
        {
            if (_windows.TryGetValue(typeof(T), out var window))
            {
                window.gameObject.SetActive(true);
                if(args != null) window.Show(args);
                return window as T;
            }

            var windowPrefab = Resources.Load<Window>(prefabPath);
            var newWindow = Instantiate(windowPrefab, _mainCanvas) as T;
            if(args != null) newWindow.Show(args);
            _windows.Add(typeof(T), newWindow);
            return newWindow;
        }
        
        public void CloseWindow<T>() where T : Window
        {
            if (!_windows.TryGetValue(typeof(T), out var window)) return;
            window.Close();
            window.gameObject.SetActive(false);
        }
    }
}