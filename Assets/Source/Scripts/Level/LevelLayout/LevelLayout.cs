using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayout : MonoBehaviour
{
    [SerializeField, Min(MinLevelHeght)] private float _levelHeight;
    [SerializeField, Min(MinLevelWidth)] private float _levelWidth;

    private const float MinLevelHeght = 20;
    private const float MinLevelWidth = 3;

    public float Height => _levelHeight;
    public float Width => _levelWidth;
}
