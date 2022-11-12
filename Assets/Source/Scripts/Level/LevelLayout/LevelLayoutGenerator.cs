using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutGenerator : ResetableMonoBehaviour
{
    [SerializeField] private Transform _levelStartPoint;
    [SerializeField] private LevelExit _levelExit;
    [SerializeField] private Background _background;
    [SerializeField] private Border _borderPrefab;
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Money _moneyPrefab;
    [SerializeField, Min(MinLevelHeght)] private float _levelHeight;
    [SerializeField, Min(MinLevelWidth)] private float _levelWidth;

    private const float MinLevelHeght = 20;
    private const float MinLevelWidth = 3;
    private const float PlatformWidthRatio = 0.3f;
    private const float BackgroundWidthRatio = 0.9f;
    private const float PlatformHeight = 0.2f;
    private const float LevelExitHeight = 3;
    private const int PlatformsYOffset = 5;
    private const int LevelExitYOffset = 4;
    private const float BackgroundZOffset = 0.5f;
    private const float MoneyFrequency = 0.3f;

    private float _backgroundWidth;
    private float _borderWidth;
    private float _platformWidth;
    private Vector3 _levelTop;
    private Vector3 _levelCenter;
    private Vector3 _levelExitPosition;
    private Vector3 _backgroundPosition;
    private List<Platform> _platforms;
    private List<Money> _moneySpots;

    private void Awake()
    {
        _backgroundWidth = _levelWidth * BackgroundWidthRatio;
        _borderWidth = _levelWidth * (1 - BackgroundWidthRatio);
        _levelTop = _levelStartPoint.position;
        _levelCenter = _levelTop + Vector3.down * (_levelHeight / 2);
        _levelExitPosition = _levelTop + new Vector3(0, -_levelHeight + LevelExitYOffset, BackgroundZOffset);
        _backgroundPosition = _levelCenter + Vector3.forward * BackgroundZOffset;
        _platformWidth = _backgroundWidth * PlatformWidthRatio;
        _platforms = new List<Platform>();
        _moneySpots = new List<Money>();
    }

    private void Start()
    {
        GenerateBorders();
        SetBackground();
        SetLevelExit();
        GenerateInnerLayout();
    }

    public void GenerateNewInnerLayout()
    {
        SetInnerLayout();
    }

    public override void SetStartState()
    {
        foreach (Platform platform in _platforms)
            platform.PlaceEnemy();
    }

    private void GenerateBorders()
    {
        float xBorderOffset = (_backgroundWidth + _borderWidth) / 2;

        Vector3 leftStartPosition = _levelCenter;
        leftStartPosition.x = -xBorderOffset;

        Vector3 rightStartPosition = _levelCenter;
        rightStartPosition.x = xBorderOffset;

        Border leftBorder = Instantiate(_borderPrefab, leftStartPosition, Quaternion.identity, transform);
        leftBorder.Scale(_levelHeight, _borderWidth);
        Instantiate(leftBorder, rightStartPosition, Quaternion.identity, transform);
    }

    private void GenerateInnerLayout()
    {
        Platform startPlatform = Instantiate(_platformPrefab, _levelTop, Quaternion.identity, transform);
        startPlatform.Scale(PlatformHeight, _backgroundWidth);

        Vector3 currentCellPosition = _levelTop + Vector3.down;
        int currentCellNumber = 1;

        float moneySpawnChance;
        Platform platform;
        Money moneySpot;

        float lastYPosition = _levelExitPosition.y + LevelExitYOffset;

        while (currentCellPosition.y > lastYPosition)
        {
            if (currentCellNumber % PlatformsYOffset == 0)
            {
                platform = GeneratePlatform(currentCellPosition);
                _platforms.Add(platform);
            }
            else
            {
                moneySpawnChance = Random.value;

                if (moneySpawnChance < MoneyFrequency)
                {
                    moneySpot = GenerateMoneySpot(currentCellPosition);
                    _moneySpots.Add(moneySpot);
                }
            }

            currentCellPosition += Vector3.down;
            currentCellNumber++;
        }
    }

    private Platform GeneratePlatform(Vector3 position)
    {
        Platform platform = Instantiate(_platformPrefab, position, Quaternion.identity, transform);
        platform.Scale(PlatformHeight, _platformWidth);
        int randomIndex = Random.Range(0, _enemyPrefabs.Length);
        platform.Initialize(_enemyPrefabs[randomIndex]);
        return platform;
    }

    private Money GenerateMoneySpot(Vector3 position)
    {
        return Instantiate(_moneyPrefab, position, Quaternion.identity, transform);
    }

    private void SetBackground()
    {
        _background.SetSize(_backgroundWidth, _levelHeight);
        _background.transform.position = _backgroundPosition;
    }

    private void SetLevelExit()
    {
        _levelExit.SetSize(_backgroundWidth, LevelExitHeight);
        _levelExit.transform.position = _levelExitPosition;
    }

    private void SetInnerLayout()
    {
        float xPosition = _levelTop.x;
        float yPosition;
        float zPosition = _levelTop.z;

        float platformXOffset = (_backgroundWidth - _platformWidth) / 2;
        float moneyXOffset = (_backgroundWidth - 1) / 2;

        foreach (Platform platform in _platforms)
        {
            yPosition = platform.transform.position.y;
            platform.transform.position = new Vector3(xPosition + platformXOffset * RandomSign(), yPosition, zPosition);
            platform.PlaceEnemy();
        }

        foreach (Money moneySpot in _moneySpots)
        {
            yPosition = moneySpot.transform.position.y;
            moneySpot.transform.position = new Vector3(xPosition + moneyXOffset * Random.Range(-1.0f, 1.0f), yPosition, zPosition);
        }
    }

    private int RandomSign() => Random.value < 0.5f ? -1 : 1;
}
