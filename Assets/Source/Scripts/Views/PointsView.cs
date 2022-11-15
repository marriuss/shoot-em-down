using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class PointsView : MonoBehaviour
{
    private TMP_Text _pointsContainer;

    private void Awake()
    {
        _pointsContainer = GetComponent<TMP_Text>();
    }

    public void Initialize(int points)
    {
        _pointsContainer.text = points.ToString();
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
