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
    [SerializeField] private RaycastTarget _raycastTarget;
    [SerializeField] private LeaderboardData _leaderboardData;

    private const int Percentage = 100;

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
        ApplicationPause.Pause();

        if (_pointsTracker != null)
        {
            int points = _pointsTracker.TotalPoints;
            float accuracy = _pointsTracker.Accuracy * Percentage;
            _scoreContainer.text = points.ToString();
            _accuracyContainer.text = string.Format("{0:0.0##}%", accuracy);
            _leaderboardData.SetScore(points);
        }

        _playerWeaponFollower.StopFollowing();
        StartCoroutine(WaitForParticleEffect());
    }

    private IEnumerator WaitForParticleEffect()
    {
        _particleSystem.Play();
        _raycastTarget.enabled = true;

        yield return new WaitForSeconds(_particleSystem.main.duration);

        _particleSystem.Stop();
        _raycastTarget.enabled = false;
        _levelEndMenu.Open();
    }
}
