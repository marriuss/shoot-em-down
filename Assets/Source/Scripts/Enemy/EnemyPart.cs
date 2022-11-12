using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class EnemyPart : MonoBehaviour
{
    public event UnityAction<EnemyPart> WasShot;

    public Enemy Enemy { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Bullet _))
            WasShot?.Invoke(this);
    }

    public void Initialize(Enemy enemy)
    {
        Enemy = enemy;
    }
}
