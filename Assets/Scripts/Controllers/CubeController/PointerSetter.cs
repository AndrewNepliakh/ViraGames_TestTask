using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class PointerSetter : IPointerSetter
    {
        public event Action<Hashtable> OnStartSetting;
        public event Action<Hashtable> OnCompleteSetting;

        private Color _startColor = new Color(1.0f, 0.0f, 0.0f);
        private Color _endColor = new Color(0.0f, 1.0f, 0.0f);

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

            if (isFirstPos)
            {
                bool? state = false;
                var args = new Hashtable
                {
                    { Constants.MOVE_BUTTON_STATE, state }
                };

                foreach (var pointer in _pointers) pointer.GameObject.SetActive(false);
                
                OnStartSetting?.Invoke(args);
            }


            for (var i = 0; i < _pointersPos.Length; i++)
            {
                if (_pointersPos[i] == Vector3.zero)
                {
                    _pointersPos[i] = position;
                    _pointers[i].SetPosition(position,
                        Color.Lerp(_startColor, _endColor, (float)i / _pointersPos.Length), i + 1);
                    IsSet = i == _pointersPos.Length - 1;
                    break;
                }
            }

            if (IsSet)
            {
                bool? state = true;
                var args = new Hashtable
                {
                    { Constants.WAY_POINTS_POSITION, _pointersPos.ToList() },
                    { Constants.MOVE_BUTTON_STATE, state }
                };

                OnCompleteSetting?.Invoke(args);
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