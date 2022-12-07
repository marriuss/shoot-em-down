using UnityEngine;
using Agava.YandexGames;

public class AuthorizationButton : WorkButton
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Update()
    {
        bool isEnabled = false;

#if UNITY_WEBGL && !UNITY_EDITOR
    isEnabled = YandexGamesSdk.IsInitialized ? !PlayerAccount.IsAuthorized : false;
#endif

        Show(isEnabled);
    }

    protected override void OnButtonClick()
    {
        PlayerAccount.Authorize();
    }

    private void Show(bool isShowing)
    {
        float alpha = isShowing ? 1 : 0;
        _canvasGroup.alpha = alpha;
        _canvasGroup.interactable = isShowing;
    }
}
