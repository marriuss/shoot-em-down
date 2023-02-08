using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LevelEnder : MonoBehaviour
{
    [SerializeField] private LevelExit _levelExit;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private PlayerWeaponFollower _playerWeaponFollower;
    [SerializeField] private Player _player;
    [SerializeField] private MenuGroup _menuGroup;

    public event UnityAction LevelEnded;

    private void OnEnable()
    {
        _levelExit.PlayerExitLevel += OnPlayerExitLevel;
    }

    private void OnDisable()
    {
        _levelExit.PlayerExitLevel -= OnPlayerExitLevel;
    }

    private void OnPlayerExitLevel()
    {
        _player.EndLevel();
        _playerWeaponFollower.StopFollowing();
        StartCoroutine(WaitForParticleEffect());
    }

    private IEnumerator WaitForParticleEffect()
    {
        _menuGroup.OpenRaycastTarget();
        _particleSystem.Play();

        yield return new WaitForSeconds(_particleSystem.main.startLifetime.constant);

        _particleSystem.Stop();
        LevelEnded?.Invoke();
    }
}
