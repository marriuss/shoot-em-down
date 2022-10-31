using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeometryGenerator : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private LevelExit _levelExit;
    [SerializeField] private Border _borderPrefab;
    [SerializeField] private Platform _platformPrefab;

    private Camera _camera;

    private Bounds _cameraBounds => _camera.GetOrthographicBounds();
    private Vector3 leftBorderPosition => new Vector3(_cameraBounds.min.x, _cameraBounds.center.y);
    private Vector3 rightBorderPosition => new Vector3(_cameraBounds.max.x, _cameraBounds.center.y);

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _camera.transform.position = _playerSpawnPoint.position;

        Vector3 borderLocalScale = _borderPrefab.transform.localScale;

        Border _leftBorder = Instantiate(_borderPrefab, leftBorderPosition, Quaternion.identity, _camera.transform);   
        Border _rightBorder = Instantiate(_borderPrefab, rightBorderPosition, Quaternion.identity, _camera.transform);

        float borderHeight = _leftBorder.Height;
        _leftBorder.transform.localScale = new Vector3(borderLocalScale.x, _cameraBounds.size.y / borderHeight);
        _rightBorder.transform.localScale = new Vector3(borderLocalScale.x, _cameraBounds.size.y / borderHeight);
    }
}
