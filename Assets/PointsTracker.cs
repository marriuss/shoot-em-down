using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PointsTracker : MonoBehaviour
{
    [SerializeField] private Player _player;

    private const int BodyShotPoints = 20;
    private const int KnockOutPoints = 100;
    private const float HeadShotMultiplier = 2f;
    private const float MaxAccuracyMultiplier = 2f;

    private int _points;
    private int _shotsTotal;
    private int _enemyShots;
    private int _enemyShotsStreak;

    public UnityAction<int> PointsGot;

    private float _accuracy => _enemyShots / _shotsTotal;

    public float AccuracyMultiplier => Mathf.Clamp(_accuracy, 1f, MaxAccuracyMultiplier);
    public int TotalPointsAmount => (int)(AccuracyMultiplier * _points);

    private void Start()
    {
        _points = 0;
        _shotsTotal = 0;
        _enemyShots = 0;
        _enemyShotsStreak = 0;
    }

    private void OnEnable()
    {
        _player.ShotCollider += OnPlayerShotCollider;
    }

    private void OnDisable()
    {
        _player.ShotCollider -= OnPlayerShotCollider;
    }

    private void OnPlayerShotCollider(Collider collider)
    {
        int pointsGot = 0;
        
        if (collider.TryGetComponent(out EnemyPart enemyPart))
        {
            Enemy enemy = enemyPart.Enemy;

            if (enemy.IsKnockedOut)
                pointsGot += KnockOutPoints;

            if (enemyPart as Head != null)
            {
                int remainingBodyShotPoints = enemy.Health * BodyShotPoints;
                pointsGot += (int)(remainingBodyShotPoints * HeadShotMultiplier);
            }
            else 
            {
                pointsGot += BodyShotPoints;
            }

            _enemyShots++;
            _enemyShotsStreak++;
        }
        else
        {
            _enemyShotsStreak = 0;
        }

        _shotsTotal++;

        if (pointsGot > 0) 
        {
            GetPoints(pointsGot);
        }
    }

    private void GetPoints(int amount)
    {
        _points += amount;
        PointsGot?.Invoke(amount);
    }
}
