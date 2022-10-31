using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Border : MonoBehaviour
{
    private BoxCollider _boxCollider;

    public float Height => _boxCollider.size.y;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

}
