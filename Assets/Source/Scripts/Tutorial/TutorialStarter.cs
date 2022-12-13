using UnityEngine;

public class TutorialStarter : MonoBehaviour
{
    [SerializeField] private PlayerWeaponFollower _playerWeaponFollower;
    [SerializeField] private Player _player;
    [SerializeField] private SlowMotion _slowMotion;
    [SerializeField] private LevelLayout _levelLayout;

    private void Start()
    {
        Camera.main.SetWidth(_levelLayout.Width);
        _player.ResetState();
        _playerWeaponFollower.ResetState();
        _slowMotion.ResetState();
    }
}
