using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private LevelExit _levelExit;
    [SerializeField] private Border _borderPrefab;
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private Background _backgroundPrefab;
    [SerializeField] private Transform _backgroundStartPosition;
    [SerializeField] private Transform _levelStartPoint;
    [SerializeField] private int _levelHeight;

    private const float LevelWidthRatio = 0.7f;

    private Camera _camera;
    private float _backgroundWidth;
    private float _borderWidth;

    private Bounds _cameraBounds => _camera.GetOrthographicBounds();

    private void Awake()
    {
        _camera = Camera.main;
        float cameraBoundsSize = _cameraBounds.size.x;
        _backgroundWidth = cameraBoundsSize * LevelWidthRatio;
        _borderWidth = (cameraBoundsSize * (1 - LevelWidthRatio));
    }

    private void Start()
    {
        GenerateBorders();
        GenerateBackround();
        GeneratePlatforms();
    }

    private void GenerateBorders()
    {
        Border leftBorder = Instantiate(_borderPrefab);
        leftBorder.Scale(_levelHeight, _borderWidth);
        float xBorderOffset = _cameraBounds.size.x / 2;
        float yBorderOffset = -_levelHeight / 2;
        Vector3 startPosition = _levelStartPoint.position;
        Vector3 leftOffset = new Vector3(-xBorderOffset, yBorderOffset, 0);
        Vector3 rightOffset = new Vector3(xBorderOffset, yBorderOffset, 0);
        leftBorder.transform.position = startPosition + leftOffset;
        Instantiate(leftBorder, startPosition + rightOffset, Quaternion.identity);
    }

    private void GenerateBackround()
    {
        Background background = Instantiate(_backgroundPrefab);
        background.transform.rotation = Quaternion.Euler(0, 90, 90);
        background.Scale(_levelHeight, _backgroundWidth);
        background.transform.position = _levelStartPoint.position + new Vector3(0, -_levelHeight / 2, 0.5f);
    }

    private void GeneratePlatforms()
    {

    }
}
