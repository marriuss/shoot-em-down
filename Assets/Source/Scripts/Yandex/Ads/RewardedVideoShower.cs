using UnityEngine;
using Agava.YandexGames;

public class RewardedVideoShower : MonoBehaviour
{
    [SerializeField, Min(1)] private int _rewardAmount;
    [SerializeField] private GameAudio _audio;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private MenuGroup _menuGroup;

    public void Show()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (YandexGamesSdk.IsInitialized)
            ShowYandexVideo();
#endif
    }

    private void ShowYandexVideo()
    {
        VideoAd.Show(
            onOpenCallback: () =>
            {
                _menuGroup.OpenRaycastTarget();
                _audio.MuteMusic();
            },
            onRewardedCallback: () => _playerData.SetMoney(_playerData.Money + _rewardAmount),
            onCloseCallback: () =>
            {
                _menuGroup.CloseRaycastTarget();
                _audio.UnmuteMusic();
            }
        );
    }
}