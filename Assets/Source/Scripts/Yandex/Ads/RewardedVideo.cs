using UnityEngine;
using Agava.YandexGames;

public class RewardedVideo : MonoBehaviour
{
    [SerializeField, Min(1)] private int _rewardAmount;
    [SerializeField] private Player _player;
    [SerializeField] private GameAudio _audio;
    [SerializeField] private RaycastTarget _raycastTarget;

    public void Show()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    VideoAd.Show(
        onOpenCallback: () =>
        {
            _audio.TurnOffMusic();
            _raycastTarget.enabled = true;
        },
        onRewardedCallback: () => _player.AddMoney(_rewardAmount),
        onCloseCallback: () =>
        {
            _audio.TurnOnMusic();
            _raycastTarget.enabled = false;
        }
        );
#endif
    }
}