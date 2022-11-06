using UnityEngine;
using UnityEngine.Events;

public class PlayerShooter : MonoBehaviour
{
    private PlayerActions _playerActions;
    private Camera _camera;
    
    public UnityAction<Vector2> PlayerShot;

    private void Awake()
    {
        _playerActions = new PlayerActions();   
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _playerActions.Enable();
        _playerActions.Weapon.Shoot.performed += (ctx) => OnShoot();
    }

    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.Weapon.Shoot.performed -= (ctx) => OnShoot();
    }

    private void OnShoot()
    {
        Vector2 screenShotPosition = _playerActions.Weapon.Position.ReadValue<Vector2>();
        Vector2 worldShotPosition = _camera.ScreenToWorldPoint(screenShotPosition);
        PlayerShot?.Invoke(worldShotPosition);
    }
}
