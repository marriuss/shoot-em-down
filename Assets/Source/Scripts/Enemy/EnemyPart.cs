using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class EnemyPart : MonoBehaviour
{
    private Collider _collider;

    public UnityAction WasShot;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _collider.enabled = false;
    }

    public Enemy Enemy { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Bullet _))
            WasShot?.Invoke();
    }

    public void Initialize(Enemy enemy)
    {
        Enemy = enemy;
    }
}
