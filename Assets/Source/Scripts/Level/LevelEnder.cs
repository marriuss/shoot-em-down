using System.Collections;
using TMPro;
using UnityEngine;

public class LevelEnder : MonoBehaviour
{
    [SerializeField] private LevelExit _levelExit;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private PlayerWeaponFollower _playerWeaponFollower;
    [SerializeField] private TMP_Text _scoreContainer;
    [SerializeField] private TMP_Text _accuracyContainer;
    [SerializeField] private PointsTracker _pointsTracker;
    [SerializeField] private Menu _levelEndMenu;
    [SerializeField] private Player _player;

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
        _player.SaveLevelProgress();
        _scoreContainer.text = _pointsTracker.TotalScore.ToString();
        _accuracyContainer.text = string.Format("{0:0.0##}%", _pointsTracker.Accuracy * 100);
        _playerWeaponFollower.StopFollowing();
        StartCoroutine(WaitForParticleEffect());
    }

    private IEnumerator WaitForParticleEffect()
    {
        _particleSystem.Play();

        yield return new WaitForSeconds(_particleSystem.main.duration);

        _particleSystem.Stop();
        _levelEndMenu.Open();
    }
}
