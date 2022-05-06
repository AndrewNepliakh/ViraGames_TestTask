using System.Collections;
using UnityEngine;

namespace Managers
{
    public class SliderContainer : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private float _slideSpeed = 4.0f;

        private Coroutine _slideInRoutine;
        private Coroutine _slideOutRoutine;
        
        public void SlideIn()
        {
            StartCoroutine(_slideInRoutine, SlideInRoutine());
        }

        public void SlideOut()
        {
            StartCoroutine(_slideOutRoutine, SlideOutRoutine());
        }
        
        private void StartCoroutine(Coroutine coroutine, IEnumerator enumerator)
        {
            if (coroutine == null)
                coroutine = StartCoroutine(enumerator);
            else
            {
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(enumerator);
            }
        }

        private IEnumerator SlideInRoutine()
        {
            var halfScreen = Screen.width / 2.0f;
            var endVector = new Vector3(halfScreen, _container.position.y, _container.position.z);
            var startVector = new Vector3( -halfScreen, _container.position.y, _container.position.z);
            _container.position = startVector;

            var t = 0.0f;
            
            while (t < 1.0f)
            {
                t += Time.deltaTime * _slideSpeed;
                _container.position = Vector3.Lerp(startVector, endVector, t);
                yield return null;
            }
            
            _container.position = endVector;
        }
        
        private IEnumerator SlideOutRoutine()
        {
            var halfScreen = Screen.width / 2.0f;
            var endVector = new Vector3(-halfScreen, _container.position.y, _container.position.z);
            var startVector = new Vector3( halfScreen, _container.position.y, _container.position.z);
            _container.position = startVector;

            var t = 0.0f;
            
            while (t < 1.0f)
            {
                t += Time.deltaTime * _slideSpeed;
                _container.position = Vector3.Lerp(startVector, endVector, t);
                yield return null;
            }

            _container.position = endVector;
        }
    }
}