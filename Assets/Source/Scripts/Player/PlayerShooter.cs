using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerShooter : MonoBehaviour
{
    private PlayerActions _playerActions;
    private Camera _camera;
    private PointerEventData _pointerEventData;

    public UnityAction<Vector2> PlayerShot;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _pointerEventData = new PointerEventData(EventSystem.current);
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

        if (CheckIsShootingAvailable(screenShotPosition))
        {
            Vector2 worldShotPosition = _camera.ScreenToWorldPoint(screenShotPosition);
            PlayerShot?.Invoke(worldShotPosition);
        }
    }

    private bool CheckIsShootingAvailable(Vector2 screenShotPosition)
    {
        _pointerEventData.position = screenShotPosition;
        return !EventSystem.current.CheckIsAnyElementPointed(_pointerEventData);
    }
}
