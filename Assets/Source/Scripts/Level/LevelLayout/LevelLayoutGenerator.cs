using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    [SerializeField] private Transform _levelStartPoint;
    [SerializeField] private Border _borderPrefab;
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private Background _backgroundPrefab;
    [SerializeField] private LevelExit _levelExitPrefab;
    [SerializeField, Min(10)] private int _levelHeight;

    private const float LevelWidthRatio = 0.7f;
    private const float PlatformHeight = 1;
    private const float LevelExitHeight = PlatformHeight * 2;
    private const float LevelExitOffset = 5;
    private const int PlatformsOffset = 5;
    private const float MinPlarformWidth = 1;
    private const float MaxPlatformWidthRatio = 0.5f;

    private Camera _camera;
    private float _backgroundWidth;
    private float _borderWidth;
    private float _maxPlatformWidth;
    private Vector3 _levelTop;
    private Vector3 _levelCenter;
    private Vector3 _levelBottom;

    private Bounds _cameraBounds => _camera.GetOrthographicBounds();

    private void Awake()
    {
        _camera = Camera.main;
        float cameraBoundsSize = _cameraBounds.size.x;
        _backgroundWidth = cameraBoundsSize * LevelWidthRatio;
        _borderWidth = cameraBoundsSize * (1 - LevelWidthRatio);
        _levelTop = _levelStartPoint.position;
        _levelCenter = _levelTop + Vector3.down * (_levelHeight * 0.5f);
        _levelBottom = _levelTop + Vector3.down * _levelHeight;
        _maxPlatformWidth = _backgroundWidth * MaxPlatformWidthRatio;
    }

    private void Start()
    {
        GenerateBorders();
        GenerateBackround();
        GeneratePlatforms();
        GenerateLevelExit();

    }

    private void GenerateBorders()
    {
        float xBorderOffset = _cameraBounds.size.x / 2;

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
        Background background = Instantiate(_backgroundPrefab, transform);
        background.Scale(_levelHeight, _backgroundWidth);
        background.transform.position = _levelCenter + new Vector3(0, 0, 0.5f);
    }

    private void GeneratePlatforms()
    {
        Platform platform = Instantiate(_platformPrefab, _levelTop, Quaternion.identity, transform);
        platform.Scale(PlatformHeight, _backgroundWidth);

        int i = PlatformsOffset;

        while (i < _levelHeight)
        {
            platform = Instantiate(platform, transform);
            platform.Scale(PlatformHeight, _maxPlatformWidth);
            platform.transform.position = _levelTop + Vector3.down * i;
            i += PlatformsOffset;
        }
    }

    private void GenerateLevelExit()
    {
        Vector3 startPosition = _levelBottom + Vector3.up * LevelExitOffset;

        LevelExit levelExit = Instantiate(_levelExitPrefab, startPosition, Quaternion.identity, transform);
        levelExit.Scale(LevelExitHeight, _backgroundWidth);
    }
}
