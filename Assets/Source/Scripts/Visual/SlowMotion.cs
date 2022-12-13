using System;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _slowMotionCooldown;
    [SerializeField, Min(1)] private int _requiredEnemyShotsStreak;

    private int _enemyShotsStreak;
    private float _lastEnemyShotTime;

    private void OnEnable()
    {
        _player.ShotCollider += OnPlayerShotCollider;
    }

    private void OnDisable()
    {
        _player.ShotCollider -= OnPlayerShotCollider;
    }

    private void Update()
    {
        if (_lastEnemyShotTime + _slowMotionCooldown < Time.time)
        {
            _lastEnemyShotTime = Time.time;
            _player.CurrentWeapon.ReturnNormalSpeed();
            _enemyShotsStreak = 0;
        }
    }

    public void ResetState()
    {
        _enemyShotsStreak = 0;
    }

    private void OnPlayerShotCollider(Collider collider)
    {
        if (collider.TryGetComponent(out EnemyPart enemyPart) && enemyPart.enabled)
        {
            _enemyShotsStreak++;
            _lastEnemyShotTime = Time.time;
        }
        else
        {
            _enemyShotsStreak = 0;
        }

        if (_enemyShotsStreak >= _requiredEnemyShotsStreak)
        {
            _player.CurrentWeapon.SlowDown();
        }
    }
}