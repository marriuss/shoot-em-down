using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    [SerializeField] private LevelExit _levelExit;
    [SerializeField] private Border _borderPrefab;
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private Background _backgroundPrefab;
    [SerializeField] private Transform _backgroundStartPosition;
    [SerializeField] private Transform _levelStartPoint;
    [SerializeField, Min(10)] private int _levelHeight;

    private const float LevelWidthRatio = 0.7f;
    private const float PlatformHeight = 1;
    private const float ExitLevelBottomPosition = 5;
    private const int PlatformsOffset = 5;
    private const float MinPlarformWidth = 1;
    private const float MaxPlatformWidthRatio = 0.5f;

    private Camera _camera;
    private float _backgroundWidth;
    private float _borderWidth;
    private Vector3 _startPosition;
    private float _maxPlatformWidth;

    private Bounds _cameraBounds => _camera.GetOrthographicBounds();

    private void Awake()
    {
        _camera = Camera.main;
        float cameraBoundsSize = _cameraBounds.size.x;
        _backgroundWidth = cameraBoundsSize * LevelWidthRatio;
        _borderWidth = (cameraBoundsSize * (1 - LevelWidthRatio));
        _startPosition = _levelStartPoint.position;
        _maxPlatformWidth = _backgroundWidth * MaxPlatformWidthRatio;
    }

    private void Start()
    {
        GenerateBorders();
        GenerateBackround();
        GeneratePlatforms();
    }

    private void GenerateBorders()
    {
        float xBorderOffset = _cameraBounds.size.x / 2;
        float yBorderOffset = -_levelHeight / 2;
        Vector3 leftOffset = new Vector3(-xBorderOffset, yBorderOffset, 0);
        Vector3 rightOffset = new Vector3(xBorderOffset, yBorderOffset, 0);

        Border leftBorder = Instantiate(_borderPrefab, _startPosition + leftOffset, Quaternion.identity);
        leftBorder.Scale(_levelHeight, _borderWidth);
        Instantiate(leftBorder, _startPosition + rightOffset, Quaternion.identity);
    }

    private void GenerateBackround()
    {
        Background background = Instantiate(_backgroundPrefab);
        background.Scale(_levelHeight, _backgroundWidth);
        background.transform.position = _startPosition + new Vector3(0, -_levelHeight / 2, 0.5f);
    }

    private void GeneratePlatforms()
    {
        Platform platform = Instantiate(_platformPrefab, _startPosition, Quaternion.identity);
        platform.Scale(PlatformHeight, _backgroundWidth);

        Vector3 offset = new Vector3(0, 1, 0);

        int i = PlatformsOffset;

        while (i < _levelHeight)
        {
            platform = Instantiate(platform);
            platform.Scale(PlatformHeight, _maxPlatformWidth);
            platform.transform.position = _startPosition + Vector3.down * i;
            i += PlatformsOffset;
        }
    }
}
