using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutGenerator : ResetableMonoBehaviour
{
    [SerializeField] private LevelLayout _levelLayout;
    [SerializeField] private List<LevelPalette> _levelPalettes;
    [SerializeField] private SpriteRenderer _spaceBackground;
    [SerializeField] private LevelExit _levelExit;
    [SerializeField] private Background _background;
    [SerializeField] private Border _borderPrefab;
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Money _moneyPrefab;
    [SerializeField] private Transform _levelStartPoint;

    private const float FreeHeight = 5;
    private const float PlatformWidthRatio = 0.3f;
    private const float BackgroundWidthRatio = 0.9f;
    private const float PlatformHeight = 0.2f;
    private const float LevelExitHeight = 2;
    private const int PlatformsYOffset = 4;
    private const int LevelExitYOffset = 4;
    private const float BackgroundZOffset = 0.5f;
    private const float MoneyFrequency = 0.2f;

    private float _levelWidth;
    private float _levelHeight;
    private float _backgroundWidth;
    private float _borderWidth;
    private float _platformWidth;
    private Vector3 _levelTop;
    private Vector3 _levelCenter;
    private Vector3 _levelExitPosition;
    private Vector3 _backgroundPosition;
    private List<Platform> _platforms;
    private List<Money> _moneySpots;
    private List<Border> _borders;

    private void Awake()
    {
        _levelWidth = _levelLayout.Width;
        _levelHeight = _levelLayout.Height;
        _backgroundWidth = _levelWidth * BackgroundWidthRatio;
        _borderWidth = _levelWidth * (1 - BackgroundWidthRatio);
        _levelTop = _levelStartPoint.position;
        _levelHeight += FreeHeight;
        _levelCenter = _levelTop + Vector3.down * (_levelHeight / 2);
        _levelExitPosition = _levelTop + new Vector3(0, -_levelHeight + LevelExitYOffset, BackgroundZOffset);
        _backgroundPosition = _levelCenter + Vector3.forward * BackgroundZOffset;
        _platformWidth = _backgroundWidth * PlatformWidthRatio;
        _platforms = new List<Platform>();
        _moneySpots = new List<Money>();
        _borders = new List<Border>();
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
        
        Border rightBorder = Instantiate(leftBorder, rightStartPosition, Quaternion.identity, transform);
        
        Border topBorder = Instantiate(_borderPrefab, _levelTop + Vector3.down * _borderWidth / 2, Quaternion.identity, transform);
        topBorder.Scale(_borderWidth, _backgroundWidth);
        
        _borders.Add(leftBorder);
        _borders.Add(rightBorder);
        _borders.Add(topBorder);
    }

    private void GenerateInnerLayout()
    {
        Vector3 currentCellPosition = _levelTop + Vector3.down * FreeHeight;
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
        int randomIndex = Random.Range(0, _levelPalettes.Count);
        LevelPalette currentLevelPalette = _levelPalettes[randomIndex];

        foreach (Border border in _borders)
            border.SetMaterial(currentLevelPalette.BorderMaterial);

        _levelExit.SetMaterial(currentLevelPalette.LevelExitMaterial);
        _spaceBackground.sprite = currentLevelPalette.SpaceBackground;

        float xPosition = _levelTop.x;
        float zPosition = _levelTop.z;
        float yPosition;

        float platformXOffset = (_backgroundWidth - _platformWidth) / 2;
        int maxMoneyXOffset = Mathf.FloorToInt(_backgroundWidth / 2);

        foreach (Platform platform in _platforms)
        {
            platform.SetMaterial(currentLevelPalette.PlatformMaterial);
            yPosition = platform.transform.position.y;
            platform.transform.position = new Vector3(xPosition + platformXOffset * RandomSign(), yPosition, zPosition);
            platform.PlaceEnemy();
        }

        float randomOffset;

        foreach (Money moneySpot in _moneySpots)
        {
            yPosition = moneySpot.transform.position.y;
            randomOffset = RandomSign() * Random.Range(-maxMoneyXOffset, 0);
            moneySpot.transform.position = new Vector3(xPosition + randomOffset, yPosition, zPosition);
        }
    }

    private int RandomSign() => Random.value < 0.5f ? -1 : 1;
}
