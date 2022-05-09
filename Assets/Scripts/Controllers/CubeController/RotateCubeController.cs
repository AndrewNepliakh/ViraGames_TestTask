using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class RotateCubeController : CubeController, IRotatable
    {
        private Direction _direction;
        private int _speed = int.Parse(Constants.DEFAULT_CUBE_SPEED_ROTATION);
        private float _radius = float.Parse(Constants.DEFAULT_CUBE_RADIUS);
        private int _amountRotations = int.Parse(Constants.DEFAULT_CUBE_AMOUNT_ROTATION);
        
        private Coroutine _rotateRoutine;
        private List<Vector3> _wayPoints;
        private bool _isInited;

        public override void Init(Hashtable args)
        {
            _isInited = true;
            
            _wayPoints = args[Constants.POINTERS_POSITION] as List<Vector3>;
        }

        public void Rotate()
        {
            if (!_isInited) return;
            if (IsInAction) return;
            
            Rotate(_wayPoints[0]);
        }
        
        private void Rotate(Vector3? Pivot)
        {
            if (_rotateRoutine == null)
                _rotateRoutine = StartCoroutine(RotateRoutine(Pivot));
            else
            {
                StopCoroutine(_rotateRoutine);
                _rotateRoutine = null;
                _rotateRoutine = StartCoroutine(RotateRoutine(Pivot));
            }
        }

        private IEnumerator RotateRoutine(Vector3? Pivot)
        {
            IsInAction = true;
            var startState = false;
            var startArgs = new Hashtable { { Constants.SWITCH_BUTTON_ACTION, startState } };
            OnStartAction?.Invoke(startArgs);
            
            var direction = _direction == Direction.Сlockwise ? 1 : - 1;
            var YrotationAngle = 0.0f;
            var startRotation = transform.rotation;

            while (YrotationAngle < 360.0f * _amountRotations)
            {
                YrotationAngle += _speed * Time.deltaTime;

                transform.position = _radius * Vector3.Normalize(transform.position - Pivot.Value) + Pivot.Value;
                transform.RotateAround(Pivot.Value, Vector3.up,_speed * Time.deltaTime * direction);
                yield return null;
            }

            transform.rotation = startRotation;
            
            IsInAction = false;
            var endState = true;
            var endArgs = new Hashtable { { Constants.SWITCH_BUTTON_ACTION, endState } };
            OnEndAction?.Invoke(endArgs);
        }

        public void SetAmountRotations(int obj) => _amountRotations = obj;
        public void SetSpeedRotation(int obj) => _speed = obj;
        public void SetRadiusRotation(float obj) => _radius = obj;
        public void SetDirectionsRotation(Direction obj) => _direction = obj;
    }
}