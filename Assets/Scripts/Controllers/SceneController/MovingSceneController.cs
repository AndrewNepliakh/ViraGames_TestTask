using System.Collections;
using System.Collections.Generic;
using Controllers.CubeController;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MovingSceneController : Scene
{
   [SerializeField] private CubeController _cubePrefab;
   [SerializeField] private PointerController _pointerPrefab;
   [SerializeField] private int _pointersCount = 2;

   private IPointerSetter _pointersSetter;
   private ICube _movingCube;
   private List<IPointer> _pointers = new List<IPointer>();
   private Camera _mainCamera;
   
   public override void Init(Hashtable args)
   {
      _mainCamera = Camera.main;
      InitCube();
      InitPointers();
   }

   public override void Hide()
   {
      
   }
   
   public override void SetPointer()
   {
      if(EventSystem.current.IsPointerOverGameObject()) return;
      
      if (Input.GetMouseButtonDown(0))
      {
         RaycastHit hit;
         Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
  
         if (Physics.Raycast(ray, out hit))
         {
            if (hit.transform.name == "Floor")
            {
               _pointersSetter.SetPointer(hit.point);
            }
         }
      }
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
   }
}
