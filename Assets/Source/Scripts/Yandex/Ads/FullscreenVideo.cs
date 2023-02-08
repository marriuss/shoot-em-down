using UnityEngine;
using Agava.YandexGames;

public class FullscreenVideo : MonoBehaviour
{
    [SerializeField, Min(1)] private int _cooldown;
    [SerializeField] private GameAudio _audio;

    private int _menuCount;

    private void Start()
    {
        _menuCount = 0;
    }

    public void Show()
    {
        if (_menuCount == _cooldown)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
    ShowYandexVideo();
#endif
            _menuCount = 0;
        }
        else
        {
            _menuCount++;
        }
    }

    public void ShowYandexVideo()
    {
        InterstitialAd.Show(
            onOpenCallback: () => 
            {
                _audio.MuteMusic();
            },
            onCloseCallback: (bool _) => 
            {
                _audio.UnmuteMusic();
            }
        );
    }
}
