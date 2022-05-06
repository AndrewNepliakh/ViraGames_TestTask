using Controllers.CubeController;
using UnityEngine;

public class PointerController : MonoBehaviour, IPointer
{
    [SerializeField] private MeshRenderer _material;
    public void SetPosition(Vector3 position, Color color)
    {
        gameObject.SetActive(true);
        transform.position = position;
        _material.material.color = color;
    }
}
