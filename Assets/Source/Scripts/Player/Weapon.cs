using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerShooter))]
[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private string _shootingAnimation;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private WeaponStats _stats;

    private const float MaxShootAngle = 30f;
    private const float VelocityModifier = 1f;
    private const float KnockbackAcceleration = 15;
    private const float KnockbackStrength = 15;
    private const float ReloadingTimeMultiplier = 4;

    private Animator _animator;
    private PlayerShooter _playerShooter;
    private Rigidbody _rigidbody;
    private float _lastShotTime;

    public event UnityAction<Collider> HitCollider;
    public event UnityAction<Collider> ShotCollider;

    public WeaponStats WeaponStats => _stats;

    private Vector3 _bulletSpawnPointPosition => _bulletSpawnPoint.position;
    private Vector3 _shootingPointPosition => _shootingPoint.position;
    private Vector3 _shootingVector => _bulletSpawnPointPosition - _shootingPointPosition;

    public Magazine Magazine { get; private set; }
    public int Cost => _stats.Cost;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerShooter = GetComponent<PlayerShooter>();
        _rigidbody = GetComponent<Rigidbody>();
        Magazine = new Magazine(_stats.MagazineCapacity);
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

    public void SetStartState()
    {
        _rigidbody.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, -90);
        Magazine.Fill();
    }

    private void OnPlayerShot(Vector2 position)
    {
        float currentTime = Time.time;

        if (_lastShotTime + _stats.ShootingDelay < currentTime)
        {
            if (Magazine.IsReloading)
                return;

            _lastShotTime = currentTime;

            float rotationAngle = Vector2.SignedAngle(_shootingVector, position);
            float koefficient = rotationAngle < 0 ? -1 : 1;

            if (Mathf.Abs(rotationAngle) > MaxShootAngle)
                rotationAngle = koefficient * MaxShootAngle;

            _rigidbody.rotation *= Quaternion.AngleAxis(rotationAngle, Vector3.forward);
            Shoot(koefficient);
        }
    }

    private void Shoot(float koefficient)
    {
        if (Magazine.IsEmpty == false)
        {
            _animator.Play(_shootingAnimation);
            Magazine.TakeBullet(1);
            Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawnPointPosition, Quaternion.identity, gameObject.transform);
            bullet.HitCollider += OnBulletHitCollider;
            bullet.Fly(_shootingVector);
            _rigidbody.AddTorque(0, 0, koefficient, ForceMode.Acceleration);
            //_rigidbody.AddTorque(-_shootingVector * KnockbackStrength, ForceMode.VelocityChange);
            //_rigidbody.AddForceAtPosition(-_shootingVector * KnockbackStrength, _shootingPointPosition, ForceMode.VelocityChange);
            _rigidbody.velocity -= KnockbackStrength * _shootingVector;
        }

        if (Magazine.IsEmpty)
            StartCoroutine(Magazine.Reload());
    }

    private void OnBulletHitCollider(Bullet bullet, Collider hitCollider)
    {
        bullet.HitCollider -= OnBulletHitCollider;
        ShotCollider?.Invoke(hitCollider);
    }
}
