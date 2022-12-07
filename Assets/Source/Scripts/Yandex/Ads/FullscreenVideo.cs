using UnityEngine;
using Agava.YandexGames;

public class FullscreenVideo : MonoBehaviour
{
    [SerializeField, Min(1)] private int _cooldown;
    [SerializeField] private GameAudio _audio;

    private int _menuCount;

    private void Awake()
    {
        _menuCount = 0;
    }

    public void Show()
    {
        if (_menuCount == _cooldown)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            InterstitialAd.Show(
                onOpenCallback: () => _audio.TurnOffMusic(),
                onCloseCallback: (bool _) => _audio.TurnOnMusic()
            );
            _menuCount = 0;
#endif
        }
        else
        {
            _menuCount++;
        }
    }
}
