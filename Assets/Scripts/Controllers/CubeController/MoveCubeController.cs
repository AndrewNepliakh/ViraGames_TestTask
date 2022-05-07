using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class MoveCubeController : CubeController, IMovable
    {
        [SerializeField] private int _speed;
        private List<Vector3> _endPoints;
        private Vector3? _startPoint;
        private Coroutine _moveRoutine;

        private int _index = 1;

        private bool _isInited;

        public override void Init(Hashtable args)
        {
            _isInited = true;

            _startPoint = args[Constants.START_POINT_POSITION] as Vector3?;
            _endPoints = args[Constants.END_POINTS_POSITION] as List<Vector3>;
            _index = _endPoints.Count;
        }

        public void Move()
        {
            if (!_isInited) return;
            
            Move(_startPoint, _endPoints[1]);
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

            _index--;
            if (_index > 1)
            {
                Move();
            }
        }
    }
}