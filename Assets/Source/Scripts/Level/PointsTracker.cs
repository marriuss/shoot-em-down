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

    public event UnityAction<int, Transform> GotPoints;

    private float _accuracyMultiplier => Mathf.Clamp(Accuracy, 1f, MaxAccuracyMultiplier);

    public float Accuracy => _shotsTotal > 0 ? _enemyShots * 1f / _shotsTotal : 0;
    public int TotalPoints => (int)(_accuracyMultiplier * _points);

    private void OnEnable()
    {
        _player.ShotCollider += OnPlayerShotCollider;
    }

    private void OnDisable()
    {
        _player.ShotCollider -= OnPlayerShotCollider;
    }

    public void ResetState()
    {
        _points = 0;
        _shotsTotal = 0;
        _enemyShots = 0;
    }

    private void OnPlayerShotCollider(Collider collider)
    {
        int pointsGot = 0;

        if (collider.TryGetComponent(out EnemyPart enemyPart) && enemyPart.enabled)
        {
            Enemy enemy = enemyPart.Enemy;
            Head head = enemyPart as Head;

            if (head != null)
            {
                int remainingBodyShotPoints = enemy.Health.CurrentValue * BodyShotPoints;
                pointsGot += (int)(remainingBodyShotPoints * HeadShotMultiplier);
            }
            else
            {
                pointsGot += BodyShotPoints;
            }

            if (enemy.IsKnockedOut)
                pointsGot += KnockOutPoints;

            _enemyShots++;
        }

        _shotsTotal++;

        if (pointsGot > 0)
        {
            _points += pointsGot;
            GotPoints?.Invoke(pointsGot, enemyPart.transform);
        }
    }
}
