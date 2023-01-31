using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndMenuController : MonoBehaviour
{
    [SerializeField] private MenuGroup _menuGroup;
    [SerializeField] private LevelEnder _levelEnder;
    [SerializeField] private PointsTracker _pointsTracker;
    [SerializeField] private LevelEndMenuView _view;

    private void OnEnable()
    {
        _levelEnder.LevelEnded += OnLevelEnded;
    }

    private void OnDisable()
    {
        _levelEnder.LevelEnded -= OnLevelEnded;
    }

    private void OnLevelEnded()
    {
        int points = _pointsTracker.TotalPoints;
        float accuracy = _pointsTracker.Accuracy * 100;
        _view.SetScore(points, accuracy);
        _menuGroup.Open(_view);
    }
}
