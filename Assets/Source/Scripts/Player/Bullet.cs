using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private const float DestructionDelay = 0.5f;

    private Rigidbody _rigidbody;

    public event UnityAction<Bullet, Collider> HitCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Fly(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speed;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Money _))
            InvokeColliderHit(collider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        InvokeColliderHit(collision.collider);
    }

    private void InvokeColliderHit(Collider collider)
    {
        HitCollider?.Invoke(this, collider);
        Destroy(gameObject, DestructionDelay);
    }
}
