using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEndMenuController : MonoBehaviour
{
    [SerializeField] private MenuGroup _menuGroup;
    [SerializeField] private LevelEnder _levelEnder;
    [SerializeField] private TutorialEndMenuView _view;

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
        _menuGroup.Open(_view);
    }
}
