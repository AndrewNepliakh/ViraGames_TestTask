using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.CubeController
{
    public class PointerSetter : IPointerSetter
    {
        public event Action OnStartSetting;
        public event Action OnCompleteSetting;

        private Color _startColor = new Color(1.0f, 0.1839623f, 0.1839623f);
        private Color _endColor = new Color(0.759434f, 0.721118f, 1.0f);

        private List<IPointer> _pointers;
        private Vector3[] _pointersPos;

        public PointerSetter(List<IPointer> pointers)
        {
            _pointers = pointers;
            _pointersPos = new Vector3[_pointers.Count];
        }

        public void SetPointer(Vector3 position)
        {
            var isFirstPos = true;
            var IsSet = true;

            foreach (var pos in _pointersPos)
            {
                if (pos != Vector3.zero)
                {
                    isFirstPos = false;
                }
            }

            if (isFirstPos) OnStartSetting?.Invoke();

            for (var i = 0; i < _pointersPos.Length; i++)
            {
                if (_pointersPos[i] == Vector3.zero)
                {
                    _pointersPos[i] = position;
                    _pointers[i].SetPosition(position, i == 0 ? _startColor : _endColor);
                    IsSet = i == _pointersPos.Length - 1;
                    break;
                }
            }

            if (IsSet)
            {
                OnCompleteSetting?.Invoke();
                ClearPosArray();
                IsSet = false;
            }
        }

        public void ClearPosArray()
        {
            _pointersPos = new Vector3[_pointers.Count];
        }
    }
}