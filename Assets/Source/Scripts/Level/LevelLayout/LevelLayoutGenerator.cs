using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    [SerializeField] private Transform _levelStartPoint;
    [SerializeField] private Border _borderPrefab;
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private Background _backgroundPrefab;
    [SerializeField] private LevelExit _levelExitPrefab;
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Money _moneyPrefab;
    [SerializeField, Min(MinLevelHeght)] private int _levelHeight;

    private const float MinLevelHeght = 10;
    private const float PlatformWidthRatio = 0.4f;
    private const float BackgroundWidthRatio = 0.7f;
    private const float PlatformHeight = 1;
    private const float LevelExitHeight = PlatformHeight * 2;
    private const int PlatformsYOffset = 5;
    private const int LevelExitYOffset = 5;
    private const float BackgroundZOffset = 0.5f;

    private Camera _camera;
    private float _cameraWidth;
    private float _backgroundWidth;
    private float _borderWidth;
    private float _platformWidth;
    private Vector3 _levelTop;
    private Vector3 _levelCenter;
    private Vector3 _levelExitPosition;

    private void Awake()
    {
        _camera = Camera.main;
        _cameraWidth = _camera.GetOrthographicBounds().size.x;
        _backgroundWidth = _cameraWidth * BackgroundWidthRatio;
        _borderWidth = _cameraWidth * (1 - BackgroundWidthRatio);
        _levelTop = _levelStartPoint.position;
        _levelCenter = _levelTop + Vector3.down * (_levelHeight * 0.5f);
        _levelExitPosition = _levelTop + Vector3.down * (_levelHeight - LevelExitYOffset);
        _platformWidth = _backgroundWidth * PlatformWidthRatio;
    }

    private void Start()
    {
        GenerateBorders();
        GenerateBackround();
        GenerateInnerLevel();
        GenerateLevelExit();
    }

    private void GenerateBorders()
    {
        float xBorderOffset = _cameraWidth / 2;

        Vector3 leftStartPosition = _levelCenter;
        leftStartPosition.x = -xBorderOffset;

        Vector3 rightStartPosition = _levelCenter;
        rightStartPosition.x = xBorderOffset;

        Border leftBorder = Instantiate(_borderPrefab, leftStartPosition, Quaternion.identity, transform);
        leftBorder.Scale(_levelHeight, _borderWidth);
        Instantiate(leftBorder, rightStartPosition, Quaternion.identity, transform);
    }

    private void GenerateBackround()
    {
        Background background = Instantiate(_backgroundPrefab, _levelCenter + Vector3.forward * BackgroundZOffset, Quaternion.identity, transform);
        background.Scale(_levelHeight, _backgroundWidth);
    }

    private void GenerateInnerLevel()
    {
        Platform startPlatform = Instantiate(_platformPrefab, _levelTop, Quaternion.identity, transform);
        startPlatform.Scale(PlatformHeight, _backgroundWidth);

        Vector3 platformXOffset = Vector3.right * (_backgroundWidth - _platformWidth) / 2;
        Vector3 currentCellPosition = _levelTop;

        int currentCellNumber = 1;
        int lastCellNumber = _levelHeight - LevelExitYOffset;

        while (currentCellNumber < lastCellNumber)
        {
            currentCellPosition += Vector3.down;

            if (currentCellNumber % PlatformsYOffset == 0)
            {
                GeneratePlatform(currentCellPosition + platformXOffset * RandomSign());
            }
            else
            {

            }

            currentCellNumber++;
        }
    }

    private void GeneratePlatform(Vector3 position)
    {
        Platform platform = Instantiate(_platformPrefab, position, Quaternion.identity, transform);
        platform.Scale(PlatformHeight, _platformWidth);
        int randomIndex = Random.Range(0, _enemyPrefabs.Length);
        Enemy enemy = Instantiate(_enemyPrefabs[randomIndex]);
        platform.SetObject(enemy.gameObject);
    }

    private void GenerateLevelExit()
    {
        LevelExit levelExit = Instantiate(_levelExitPrefab, _levelExitPosition, Quaternion.identity, transform);
        levelExit.Scale(LevelExitHeight, _backgroundWidth);
    }

    private int RandomSign() => Random.value < 0.5f ? -1 : 1;
}
