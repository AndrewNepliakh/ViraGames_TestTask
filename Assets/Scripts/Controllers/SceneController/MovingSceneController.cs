using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MovingSceneController : Scene
{
    public override IMovable Cube => _movingCube;

    [SerializeField] private MoveCubeController _cubePrefab;
    [SerializeField] private PointerController _pointerPrefab;
    [SerializeField] private int _pointersCount = 2;

    private IPointerSetter _pointersSetter;
    private IMovable _movingCube;
    private readonly List<IPointer> _pointers = new List<IPointer>();
    private Camera _mainCamera;

    private Action<Hashtable> _startPointersSetterCallback;
    private Action<Hashtable> _completePointersSetterCallback;

    public override void Init(Hashtable args)
    {
        _startPointersSetterCallback =
            args[Constants.START_POINTERS_SETTINGS_ACTION] as Action<Hashtable>;
        _completePointersSetterCallback =
            args[Constants.COMPLETE_POINTERS_SETTINGS_ACTION] as Action<Hashtable>;

        _mainCamera = Camera.main;

        InitCube();
        InitPointers();
    }

    public override void Hide()
    {
        if (_startPointersSetterCallback != null)
            _pointersSetter.OnStartSetting -= _startPointersSetterCallback;
        if (_completePointersSetterCallback != null)
            _pointersSetter.OnCompleteSetting -= _completePointersSetterCallback;

        _pointersSetter.OnCompleteSetting -= _movingCube.Init;

        for (var i = _pointers.Count; i > 0; i--)
        {
            Destroy(_pointers[i].GameObject);
        }
        
        _pointers.Clear();
    }

    public override void SetPointer()
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

    private void InitCube()
    {
        _movingCube = Instantiate(_cubePrefab, Vector3.zero, Quaternion.identity);
    }

    private void InitPointers()
    {
        for (var i = 0; i < _pointersCount; i++)
        {
            var pointer = Instantiate(_pointerPrefab, Vector3.zero, Quaternion.identity);
            pointer.gameObject.SetActive(false);
            _pointers.Add(pointer);
        }

        _pointersSetter = new PointerSetter(_pointers);

        if (_startPointersSetterCallback != null)
            _pointersSetter.OnStartSetting += _startPointersSetterCallback;
        if (_completePointersSetterCallback != null)
            _pointersSetter.OnCompleteSetting += _completePointersSetterCallback;

        _pointersSetter.OnCompleteSetting += _movingCube.Init;
    }
}