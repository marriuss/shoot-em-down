using UnityEngine;

public class ApplicationPause : MonoBehaviour
{
    [SerializeField] private Menu _pauseMenu;
    [SerializeField] private GameAudio _audio;

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            _audio.TurnOnMusic();
        }
        else
        {
            if (TimeChanger.IsTimeFrozen == false)
                _pauseMenu.Open();

            _audio.TurnOffMusic();
        }
    }

}
