using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class SpiralCubeController : CubeController, ISpiralable
    {
        private Direction _direction;
        private int _speed = int.Parse(Constants.DEFAULT_CUBE_SPEED_ROTATION);
        private int _loopsAmount = int.Parse(Constants.DEFAULT_CUBE_LOOPS_AMOUNT);
        private float _stepLoopsAmount = float.Parse(Constants.DEFAULT_CUBE_STEP_LOOPS_AMOUNT);

        private Coroutine _spiralRoutine;
        private List<Vector3> _wayPoints;
        private bool _isInited;
        
        public override void Init(Hashtable args)
        {
            _isInited = true;
            _wayPoints = args[Constants.POINTERS_POSITION] as List<Vector3>;
        }

        public void Spiral()
        {
            if (!_isInited) return;
            if (IsInAction) return;
            
            Spiral(_wayPoints[0]);
        }
        
        private void Spiral(Vector3? Pivot)
        {
            if (_spiralRoutine == null)
                _spiralRoutine = StartCoroutine(RotateRoutine(Pivot));
            else
            {
                StopCoroutine(_spiralRoutine);
                _spiralRoutine = null;
                _spiralRoutine = StartCoroutine(RotateRoutine(Pivot));
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
            var startPosition = transform.position;
            var step = 0.0f;
            var t = 0.0f;
            var first = false;

            while (YrotationAngle < 360.0f * _loopsAmount)
            {
                YrotationAngle += _speed * Time.deltaTime;
                t = _direction == Direction.Сlockwise
                    ? YrotationAngle / (360.0f * _loopsAmount)
                    : ((360.0f * _loopsAmount) - YrotationAngle) / (360.0f * _loopsAmount);
                step = Mathf.Lerp(0.0f, _stepLoopsAmount, t);

                transform.position = step * Vector3.Normalize(transform.position - Pivot.Value) + Pivot.Value;
                
                if (!first)
                {
                    startPosition = transform.position;
                    first = true;
                }
                
                transform.RotateAround(Pivot.Value, Vector3.up,_speed * Time.deltaTime * direction);
                yield return null;
            }

            transform.rotation = startRotation;
            if(_direction == Direction.СounterСlockwise) transform.position = startPosition;
            
            IsInAction = false;
            var endState = true;
            var endArgs = new Hashtable { { Constants.SWITCH_BUTTON_ACTION, endState } };
            OnEndAction?.Invoke(endArgs);
        }
        
        public void SetStepLoops(float obj) => _stepLoopsAmount = obj;
        public void SetSpeed(int obj) => _speed = obj;
        public void SetAmountLoops(int obj) => _loopsAmount = obj;
        public void SetSpiralDirection(Direction obj) => _direction = obj;
    }
}