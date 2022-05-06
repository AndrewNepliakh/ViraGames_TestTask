using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class Darker : MonoBehaviour
    {
        public Action OnComplete;
        public Action OnClick;
        public bool IsMoving => _isMoving;

        [SerializeField] private Image _darkerImage;

        private float _fadeSpeed = 2.0f;
        private float _darknessValue = 0.7f;

        private Coroutine _fadeInRoutine;
        private Coroutine _fadeOutRoutine;

        private bool _isMoving;

        public void FadeInDarker()
        {
            StartCoroutine(_fadeInRoutine, FadeInRoutine());
        }

        public void FadeOutDarker()
        {
            StartCoroutine(_fadeOutRoutine, FadeOutRoutine());
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

        private IEnumerator FadeInRoutine()
        {
            _isMoving = true;
            
            var a = 0.0f;
            var offSetColor = new Color(_darkerImage.color.r, _darkerImage.color.g, _darkerImage.color.b, a);
            _darkerImage.color = offSetColor;
            while (_darkerImage.color.a < _darknessValue - 0.1f)
            {
                offSetColor.a += Time.deltaTime * _fadeSpeed;
                _darkerImage.color = offSetColor;
                yield return null;
            }

            offSetColor.a = _darknessValue;
            _darkerImage.color = offSetColor;

            _isMoving = false;
        }

        private IEnumerator FadeOutRoutine()
        {
            _isMoving = true;
            
            var a = _darknessValue;
            var offSetColor = new Color(_darkerImage.color.r, _darkerImage.color.g, _darkerImage.color.b, a);
            _darkerImage.color = offSetColor;
            while (_darkerImage.color.a > 0.0f)
            {
                offSetColor.a -= Time.deltaTime * _fadeSpeed;
                _darkerImage.color = offSetColor;
                yield return null;
            }

            offSetColor.a = 0.0f;
            _darkerImage.color = offSetColor;
            
            _isMoving = false;
            OnComplete?.Invoke();
        }

        public void OnCloseClick()
        {
            OnClick?.Invoke();
        }
    }
}