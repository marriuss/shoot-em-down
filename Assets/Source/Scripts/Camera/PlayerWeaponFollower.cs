using UnityEngine;

public class PlayerWeaponFollower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _rotation;

    private Weapon _targetWeapon;

    private void Awake()
    {
        transform.Rotate(_rotation);
    }

    private void Update()
    {
        if (_targetWeapon == null)
            return;

        Vector3 newPosition = _targetWeapon.transform.position;
        newPosition.z = transform.position.z;

        transform.position = newPosition;
    }

    public void ResetState()
    {
        _targetWeapon = _player.CurrentWeapon;
    }

    public void StopFollowing()
    {
        _targetWeapon = null;
    }
}
