using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public abstract class Scene : MonoBehaviour, IScene
    {
        protected IPointerSetter _pointersSetter;
        protected Camera _mainCamera;

        protected Action<Hashtable> _startPointersSetterCallback;
        protected Action<Hashtable> _completePointersSetterCallback;
       
        public GameObject GameObject => gameObject;

        public virtual void Init(Hashtable args)
        {
            _startPointersSetterCallback =
                args[Constants.START_POINTERS_SETTINGS_ACTION] as Action<Hashtable>;
            _completePointersSetterCallback =
                args[Constants.COMPLETE_POINTERS_SETTINGS_ACTION] as Action<Hashtable>;

            _mainCamera = Camera.main;
        }

        public abstract void Hide();

        public virtual void SetPointer()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == "Floor")
                    {
                        _pointersSetter.SetPointer(hit.point);
                    }
                }
            }
        }
    }
}