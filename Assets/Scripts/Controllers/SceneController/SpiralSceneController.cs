using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class SpiralSceneController : Scene, ISpiralScene
    {
        public ISpiralable Cube => _spiralCube;
        private ISpiralable _spiralCube;
        
        [SerializeField] protected SpiralCubeController _cubePrefab;
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
            _spiralCube = Instantiate(_cubePrefab, Vector3.zero, Quaternion.identity);
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
                _spiralCube.OnStartAction += _startPointersSetterCallback;
            }


            if (_completePointersSetterCallback != null)
            {
                _pointersSetter.OnCompleteSetting += _completePointersSetterCallback;
                _spiralCube.OnEndAction += _completePointersSetterCallback;
            }
            
            _pointersSetter.OnCompleteSetting += _spiralCube.Init;
        }
        
        public override void Hide()
        {
            if (_startPointersSetterCallback != null)
            {
                _pointersSetter.OnStartSetting -= _startPointersSetterCallback;
                _spiralCube.OnStartAction -= _startPointersSetterCallback;
            }


            if (_completePointersSetterCallback != null)
            {
                _pointersSetter.OnCompleteSetting -= _completePointersSetterCallback;
                _spiralCube.OnStartAction -= _completePointersSetterCallback;
            }

            _pointersSetter.OnCompleteSetting -= _spiralCube.Init;

            Destroy(_pointer.GameObject);
            Destroy(_spiralCube.GameObject);
        }
        
        public override void SetPointer()
        {
            if (_spiralCube.IsInAction) return;
            base.SetPointer();
        }
    }
}