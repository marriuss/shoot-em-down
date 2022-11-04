using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;

    private const float Speed = 10;

    private Rigidbody _rigidbody;

    public int Damage => _damage;

    public UnityAction<Bullet, Collider> HitCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Fly(Vector3 direction)
    {
        _rigidbody.velocity = direction * Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.TryGetComponent(out Weapon _))
            return;

        HitCollider?.Invoke(this, collider);
        Destroy(gameObject);
    }
}
