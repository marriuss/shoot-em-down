using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private PlayerWeaponFollower _playerWeaponFollower;
    [SerializeField] private LevelLayoutGenerator _generator;
    [SerializeField] private PointsTracker _pointsTracker;
    [SerializeField] private SlowMotion _slowMotion;
    [SerializeField] private Player _player;
    [SerializeField] private Playlist _levelPlaylist;
    [SerializeField] private LevelLayout _levelLayout;
    [SerializeField] private LevelExit _levelExit;
    [SerializeField] private MenuGroup _menuGroup;
    [SerializeField] private ShopMenuView _shopMenuView;

    private void Start()
    {
        Camera.main.SetWidth(_levelLayout.Width);
        StartNewLevel();
    }

    public void StartNewLevel()
    {
        _generator.GenerateNewInnerLayout();
        _levelPlaylist.ChooseNewTrack();
        ResetObjects();
    }

    public void RestartLevel()
    {
        _generator.ResetGeneration();
        _levelPlaylist.ResetTrack();
        ResetObjects();
    }

    public void ResetObjects()
    {
        _levelExit.ResetState();
        _player.ResetState();
        _pointsTracker.ResetState();
        _slowMotion.ResetState();
        _playerWeaponFollower.ResetState();
        _menuGroup.CloseMenus();
        _menuGroup.Open(_shopMenuView);
    }
}
