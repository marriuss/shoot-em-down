using UnityEngine;

[RequireComponent(typeof(PlayerShooter))]
[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;

    private const float KnockbackStrength = 9;

    private PlayerShooter _playerShooter;
    private Rigidbody _rigidbody;

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

    private void OnPlayerShot()
    {
        Shoot();
    }

    private void Shoot()
    {
        Vector3 bulletSpawnPointPosition = _bulletSpawnPoint.position;
        Vector3 knockbackDirection = (bulletSpawnPointPosition - _rigidbody.position).normalized;
        _rigidbody.AddForceAtPosition(-knockbackDirection * KnockbackStrength, bulletSpawnPointPosition, ForceMode.VelocityChange);
    }
}
