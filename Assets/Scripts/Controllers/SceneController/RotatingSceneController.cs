using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class RotatingSceneController : Scene, IRotatingScene
    {
        public IRotatable Cube => _rotatingCube;
        private IRotatable _rotatingCube;

        [SerializeField] protected RotateCubeController _cubePrefab;
        [SerializeField] protected PointerController _pointerPrefab;

        private IPointer _pointer;

        public override void Init(Hashtable args)
        {
            var switchButtonAction = args[Constants.SWITCH_BUTTON_ACTION] as Action<Hashtable>;
            
            _startPointersSetterCallback = switchButtonAction;
            _completePointersSetterCallback = switchButtonAction;
            
            _mainCamera = Camera.main;
            
            InitCube();
            InitPointer();
        }

        private void InitCube()
        {
            _rotatingCube = Instantiate(_cubePrefab, Vector3.zero, Quaternion.identity);
        }

        private void InitPointer()
        {
            var pointer = Instantiate(_pointerPrefab, Vector3.zero, Quaternion.identity);
            pointer.gameObject.SetActive(false);
            _pointer = pointer;
            
            _pointersSetter = new PointerSetter(new List<IPointer>{_pointer});

            if (_startPointersSetterCallback != null)
            {
                _pointersSetter.OnStartSetting += _startPointersSetterCallback;
                _rotatingCube.OnStartAction += _startPointersSetterCallback;
            }


            if (_completePointersSetterCallback != null)
            {
                _pointersSetter.OnCompleteSetting += _completePointersSetterCallback;
                _rotatingCube.OnEndAction += _completePointersSetterCallback;
            }
            
            _pointersSetter.OnCompleteSetting += _rotatingCube.Init;
        }

        public override void Hide()
        {
            if (_startPointersSetterCallback != null)
            {
                _pointersSetter.OnStartSetting -= _startPointersSetterCallback;
                _rotatingCube.OnStartAction -= _startPointersSetterCallback;
            }


            if (_completePointersSetterCallback != null)
            {
                _pointersSetter.OnCompleteSetting -= _completePointersSetterCallback;
                _rotatingCube.OnStartAction -= _completePointersSetterCallback;
            }

            _pointersSetter.OnCompleteSetting -= _rotatingCube.Init;

            Destroy(_pointer.GameObject);
            Destroy(_rotatingCube.GameObject);
        }

        public override void SetPointer()
        {
            if (_rotatingCube.IsInAction) return;
            base.SetPointer();
        }
    }
}