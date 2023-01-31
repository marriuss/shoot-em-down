using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelEndMenuView : MenuView
{
    [SerializeField] private TMP_Text _scoreContainer;
    [SerializeField] private TMP_Text _accuracyContainer;

    public void SetScore(int points, float accuracy)
    {
        _scoreContainer.text = points.ToString();
        _accuracyContainer.text = string.Format("{0:0.0##}%", accuracy);
    }

    protected override void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
