using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsDisplayer : MonoBehaviour
{
    [SerializeField] private PointsTracker _pointsTracker;
    [SerializeField] private PointsView _pointsViewPrefab;

    private void OnEnable()
    {
        _pointsTracker.GotPoints += OnPlayerGotPoints;
    }

    private void OnDisable()
    {
        _pointsTracker.GotPoints -= OnPlayerGotPoints;
    }

    private void OnPlayerGotPoints(int amount, Transform position)
    {
        PointsView pointsView = Instantiate(_pointsViewPrefab, position.position, Quaternion.identity, transform);
        pointsView.Initialize(amount);
    }
}
