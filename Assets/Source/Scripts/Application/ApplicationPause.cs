using UnityEngine;

public class ApplicationPause : MonoBehaviour
{
    [SerializeField] private Menu _pauseMenu;

    private static bool paused;
    private static Menu openedMenu;

    private void OnApplicationFocus(bool focus)
    {
        if ((focus || openedMenu != null || paused) == false)
                _pauseMenu.Open();
    }

    public static void Pause()
    {
        paused = true;
    }

    public static void Unpause()
    {
        paused = false;
    }

    public static void OpenMenu(Menu menu)
    {
        if (openedMenu == null)
        {
            Pause();
            openedMenu = menu;
        }
    }

    public static void CloseMenu(Menu menu)
    {
        if (openedMenu == menu)
        {
            Unpause();
            openedMenu = null;
        }
    }
}
