using System.Collections;
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MovingSceneController : Scene, IMovingScene
{
    public IMovable Cube => _movingCube;
    private IMovable _movingCube;
    
    [SerializeField] private MoveCubeController _cubePrefab;
    [SerializeField] private PointerController _pointerPrefab;
    [SerializeField] protected int _pointersCount = 2;
    
    private readonly List<IPointer> _pointers = new List<IPointer>();
    public override void Init(Hashtable args)
    {
        base.Init(args);
        InitCube();
        InitPointers();
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
    
    public override void Hide()
    {
        if (_startPointersSetterCallback != null)
            _pointersSetter.OnStartSetting -= _startPointersSetterCallback;
        if (_completePointersSetterCallback != null)
            _pointersSetter.OnCompleteSetting -= _completePointersSetterCallback;

        _pointersSetter.OnCompleteSetting -= _movingCube.Init;

        for (var i = _pointers.Count - 1; i >= 0; i--)
        {
            Destroy(_pointers[i].GameObject);
        }
        
        _pointers.Clear();
        
        Destroy(_movingCube.GameObject);
    }

    public override void SetPointer()
    {
        if(_movingCube.IsInAction) return;
        base.SetPointer();
    }
}