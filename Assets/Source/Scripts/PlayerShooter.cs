using UnityEngine;
using UnityEngine.Events;

public class PlayerShooter : MonoBehaviour
{
    private PlayerActions _playerActions;

    public UnityAction PlayerShot;

    private void Awake()
    {
        _playerActions = new PlayerActions();    
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
        PlayerShot?.Invoke();
    }
}
