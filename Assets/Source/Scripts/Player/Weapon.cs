using System.Collections;
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

    private const float MaxShootAngle = 20f;
    private const float VelocityModifier = 1f;
    private const float KnockbackAcceleration = 25;
    private const float KnockbackStrength = 20;

    private PlayerShooter _playerShooter;
    private Rigidbody _rigidbody;
    private float _lastShotTime;
    private bool _shot;

    private Vector3 _bulletSpawnPointPosition => _bulletSpawnPoint.position;
    private Vector3 _shootingPointPosition => _shootingPoint.position;
    private Vector3 _shootingVector => _bulletSpawnPointPosition - _shootingPointPosition;

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

    private void FixedUpdate()
    {
        _rigidbody.velocity *= VelocityModifier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitCollider?.Invoke(collision.collider);
    }

    public void Translate(Vector3 position)
    {
        _rigidbody.position = position;
    }

    private void OnPlayerShot(Vector2 position)
    {
        float currentTime = Time.time;

        if (_lastShotTime + _stats.ShootingDelay < currentTime)
        {
            _lastShotTime = currentTime;

            float rotationAngle = Vector2.SignedAngle(position, _bulletSpawnPointPosition);

            if (Mathf.Abs(rotationAngle) > MaxShootAngle)
            {
                float koefficient = rotationAngle < 0 ? -1 : 1;
                rotationAngle = koefficient * MaxShootAngle;
            }

            Quaternion rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
            _rigidbody.rotation *= rotation;
            Shoot();
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawnPointPosition, Quaternion.identity, gameObject.transform);
        bullet.HitCollider += OnBulletHitCollider;
        bullet.Fly(_shootingVector);
        _rigidbody.AddForceAtPosition(-_shootingVector * KnockbackAcceleration, _shootingPointPosition, ForceMode.Acceleration);
        _rigidbody.velocity -= KnockbackStrength * _shootingVector;
    }

    private void OnBulletHitCollider(Bullet bullet, Collider hitCollider)
    {
        bullet.HitCollider -= OnBulletHitCollider;
        ShotCollider?.Invoke(hitCollider);
    }
}
