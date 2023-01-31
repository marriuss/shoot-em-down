using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardScoreSaver : MonoBehaviour
{
    [SerializeField] private PointsTracker _pointsTracker;
    [SerializeField] private LevelEnder _levelEnder;

    private void OnEnable()
    {
        _levelEnder.LevelEnded += OnLevelEnded;
    }
    
    private void OnDisable()
    {
        _levelEnder.LevelEnded += OnLevelEnded;
    }

    private void OnLevelEnded()
    {
        int points = _pointsTracker.TotalPoints;
        LeaderboardData.SetScore(points);
    }
}
