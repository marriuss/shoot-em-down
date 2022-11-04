using UnityEngine;

public class PlayerWeaponFollower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed;

    private Transform _targetWeapon;

    private void Update()
    {
        if (_player.CurrentWeapon == null)
            return;

        _targetWeapon = _player.CurrentWeapon.transform;

        Vector3 currentPosition = transform.position;
        Vector3 newPosition = new Vector3(currentPosition.x, _targetWeapon.position.y, currentPosition.z);
        transform.position = newPosition;
    }
}
