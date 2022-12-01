using UnityEngine;

public class InvisibleBulletDestroyer : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private void OnBecameInvisible()
    {
        Destroy(_bullet.gameObject);
    }
}
