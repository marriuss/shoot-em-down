using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerShooter _playerShooter;
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private float _knockbackStrength;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private WeaponInfo _info;
    [SerializeField] private UnityEvent _shot;

    private const float GravityModifier = 0.7f;
    private const float MaxAngularVelocity = 3f;
    private const float SlowedDownModifier = 0.6f;

    private Vector3 _spawnPosition;
    private Vector3 _currentSpawnPosition;
    private Rigidbody _rigidbody;
    private float _lastShotTime;
    private float _velocityModifier;

    public event UnityAction<Collider> HitCollider;
    public event UnityAction<Collider> ShotCollider;

    private Vector3 _bulletSpawnPointPosition => _bulletSpawnPoint.position;
    private Vector3 _shootingPointPosition => _shootingPoint.position;
    private Vector3 _shootingVector => _bulletSpawnPointPosition - _shootingPointPosition;

    public WeaponInfo Info => _info;
    public Magazine Magazine { get; private set; }

    private void Awake()
    {
        _spawnPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        Magazine = new Magazine(_magazineCapacity);
        _rigidbody.maxAngularVelocity = MaxAngularVelocity;
        Despawn();
    }

    private void OnEnable()
    {
        _playerShooter.PlayerShot += OnPlayerShot;
    }

    private void OnDisable()
    {
        _playerShooter.PlayerShot -= OnPlayerShot;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Money _))
            HitCollider?.Invoke(collider);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity += GravityModifier * Time.deltaTime * Physics.gravity;
        _rigidbody.velocity *= _velocityModifier;
        _rigidbody.angularVelocity *= _velocityModifier;
    }

    public void SetSpawnPoint(Vector3 position)
    {
        _currentSpawnPosition = position;
        enabled = true;
    }

    public void Despawn()
    {
        _currentSpawnPosition = _spawnPosition;
        enabled = false;
    }

    public void SlowDown()
    {
        ModifyVelocity(SlowedDownModifier);
    }

    public void ReturnNormalSpeed()
    {
        ModifyVelocity(1f);
    }

    public void ResetState()
    {
        transform.position = _currentSpawnPosition;
        _rigidbody.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, -90);
        _velocityModifier = 1f;
        Magazine.Fill();
    }

    private void OnPlayerShot(Vector2 position)
    {
        float currentTime = Time.time;

        if (_lastShotTime + _shootingDelay < currentTime)
        {
            if (Magazine.IsReloading)
                return;

            _lastShotTime = currentTime;

            float rotationAngle = Vector2.SignedAngle(_shootingVector, position);
            float koefficient = rotationAngle < 0 ? -1 : 1;
            Shoot(koefficient);
        }
    }

    private void Shoot(float koefficient)
    {
        if (Magazine.IsEmpty == false)
        {
            _shot?.Invoke();
            Magazine.TakeBullet(1);
            Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawnPointPosition, transform.rotation, gameObject.transform);
            bullet.HitCollider += OnBulletHitCollider;
            bullet.Fly(_shootingVector);
            _rigidbody.AddForceAtPosition(-_knockbackStrength * _velocityModifier * _shootingVector, _shootingPointPosition, ForceMode.VelocityChange);
            _rigidbody.AddTorque(0, 0, koefficient * _velocityModifier, ForceMode.Acceleration);
        }

        if (Magazine.IsEmpty)
            StartCoroutine(Magazine.Reload());
    }

    private void OnBulletHitCollider(Bullet bullet, Collider hitCollider)
    {
        bullet.HitCollider -= OnBulletHitCollider;
        ShotCollider?.Invoke(hitCollider);
    }

    private void ModifyVelocity(float modifier)
    {
        _velocityModifier = modifier;
    }
}
