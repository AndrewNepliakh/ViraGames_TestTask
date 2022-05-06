using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Managers
{
    public class ScenesManager : MonoBehaviour, IScenesManager
    {
        private Dictionary<Type, IScene> _scenes = new Dictionary<Type, IScene>();
        private Vector3 _startPosition = Vector3.zero;

        public IScene CreateScene<T>(string path, Hashtable args = null) where T : Scene
        {
            if (_scenes.TryGetValue(typeof(T), out var scene))
            {
                scene.GameObject.SetActive(true);
                scene.Init(args);
                return scene as T;
            }
            
            var scenePrefab = Resources.Load<Scene>(path);
            var newScene = Instantiate(scenePrefab, _startPosition, Quaternion.identity) as T;
            newScene.Init(args);
            _scenes.Add(typeof(T), newScene);
            return newScene;
        }

        public void HideScene<T>() where T : IScene
        {
            if (!_scenes.TryGetValue(typeof(T), out var window)) return;
            window.Hide();
            window.GameObject.SetActive(false);
        }
    }
}