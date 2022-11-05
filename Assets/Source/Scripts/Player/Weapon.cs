using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerShooter))]
[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private WeaponStats _stats;

    private const float KnockbackStrength = 9;

    private PlayerShooter _playerShooter;
    private Rigidbody _rigidbody;
    private float _lastShotTime;

    public UnityAction<Collider> HitCollider;
    public UnityAction<Collider> ShotCollider; 

    private void Awake()
    {
        _playerShooter = GetComponent<PlayerShooter>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _playerShooter.PlayerShot += OnPlayerShot;
    }

    private void OnDisable()
    {
        _playerShooter.PlayerShot -= OnPlayerShot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitCollider?.Invoke(collision.collider);
    }

    public void Translate(Vector3 position)
    {
        _rigidbody.position = position;
    }

    private void OnPlayerShot()
    {
        float currentTime = Time.time;

        if (_lastShotTime + _stats.ShootingDelay < currentTime)
        {
            _lastShotTime = currentTime;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 bulletSpawnPointPosition = _bulletSpawnPoint.position;
        Vector3 bulletFlyDirection = (bulletSpawnPointPosition - _shootingPoint.position).normalized;
        Bullet bullet = Instantiate(_bulletPrefab, bulletSpawnPointPosition, Quaternion.identity, gameObject.transform);
        bullet.HitCollider += OnBulletHitCollider;
        bullet.Fly(bulletFlyDirection);
        _rigidbody.AddForceAtPosition(-bulletFlyDirection * KnockbackStrength, bulletSpawnPointPosition, ForceMode.VelocityChange);
    }

    private void OnBulletHitCollider(Bullet bullet, Collider hitCollider)
    {
        bullet.HitCollider -= OnBulletHitCollider;
        ShotCollider?.Invoke(hitCollider);
    }
}
