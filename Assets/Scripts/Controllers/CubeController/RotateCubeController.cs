using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class RotateCubeController : CubeController, IRotatable
    {
        private int _speed = 4;
        private Coroutine _rotateRoutine;
        private List<Vector3> _wayPoints;
        private bool _isInited;
        private bool _isMoving;
        public override void Init(Hashtable args)
        {
            _isInited = true;
            
            _wayPoints = args[Constants.POINTERS_POSITION] as List<Vector3>;
        }

        public void Rotate()
        {
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

            while (true)
            {
                transform.RotateAround(Pivot.Value, Vector3.up, _speed * Time.deltaTime); 
                Debug.Log(transform.eulerAngles.x + " : " + transform.eulerAngles.y + " : " + transform.eulerAngles.z);
                yield return null;
            }
        }

        public void SetAmountRotations(int obj)
        {
           
        }

        public void SetSpeedRotation(int obj)
        {
            _speed = obj;
            Debug.Log($"_speed : {_speed}");
        }

        public void SetRadiusRotation(float obj)
        {
            
        }

        public void SetDirectionsRotation(Direction obj)
        {
           
        }
        
    }
}