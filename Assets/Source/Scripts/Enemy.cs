using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Min(1)] private int _health;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Bullet bullet))
            TakeDamage(bullet.Damage);
    }

    private void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health < 0)
            KnockOut();
    }

    private void KnockOut() 
    {
    }
}
