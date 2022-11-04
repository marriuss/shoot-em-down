using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Money : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private float _rotationAnglePerSecond;

    private const float DestructionDelay = 0.5f;

    private BoxCollider _boxCollider;

    public int Value => _value;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        transform.Rotate(_rotationAnglePerSecond * Time.deltaTime * Vector3.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.TryGetComponent(out Weapon _) || collider.TryGetComponent(out Bullet _))
        {
            _boxCollider.enabled = false;
            Destroy(gameObject, DestructionDelay);
        }
    }
}
