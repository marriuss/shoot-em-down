using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;

    private const float Speed = 50;
    private const float DestructionDelay = 1f;

    private Rigidbody _rigidbody;

    public UnityAction<Bullet, Collider> HitCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Fly(Vector3 direction)
    {
        _rigidbody.velocity = direction * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Money _))
            InvokeColliderHit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.TryGetComponent(out Weapon _) == false)
            InvokeColliderHit(collider);
    }

    private void InvokeColliderHit(Collider collider)
    {
        HitCollider?.Invoke(this, collider);
        Destroy(gameObject, DestructionDelay);
    }
}
