using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class MoveCubeController : CubeController, IMovable
    {
        private int _speed = 4;
        private List<Vector3> _wayPoints;
        private Coroutine _moveRoutine;

        private int _index = 1;
        private bool _isInited;

        public override void Init(Hashtable args)
        {
            _isInited = true;
            
            _wayPoints = args[Constants.WAY_POINTS_POSITION] as List<Vector3>;
        }

        public void Move()
        {
            if (!_isInited) return;
            
            Move(_wayPoints[_index - 1], _wayPoints[_index]);
        }

        public void SetSpeed(int obj)
        {
            _speed = obj;
        }

        private void Move(Vector3? start, Vector3 endPoint)
        {
            if (_moveRoutine == null)
                _moveRoutine = StartCoroutine(MoveRoutine(start, endPoint));
            else
            {
                StopCoroutine(_moveRoutine);
                _moveRoutine = null;
                _moveRoutine = StartCoroutine(MoveRoutine(start, endPoint));
            }
        }

        private IEnumerator MoveRoutine(Vector3? start, Vector3 endPoints)
        {
            var t = 0.0f;
            if (start != null)
            {
                var distance = Vector3.Distance(start.Value, endPoints);
            
                while (t < 1.0f)
                {
                    t += Time.deltaTime * _speed / distance;
                    transform.position = Vector3.Lerp(start.Value, endPoints, t);

                    yield return null;
                }
            }

            _index++;
            
            if (_index < _wayPoints.Count)
            {
                Move();
            }
            else
            {
                _index = 1;
            }
        }
    }
}