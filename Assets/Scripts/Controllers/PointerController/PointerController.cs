using Controllers;
using TMPro;
using UnityEngine;

public class PointerController : MonoBehaviour, IPointer
{
    [SerializeField] private MeshRenderer _material;
    [SerializeField] private TextMeshProUGUI _countText;

    public GameObject GameObject => gameObject;

    public void SetPosition(Vector3 position, Color color, int count)
    {
        gameObject.SetActive(true);
        transform.position = position;
        _material.material.color = color;
        _countText.text = count.ToString();
    }
}
