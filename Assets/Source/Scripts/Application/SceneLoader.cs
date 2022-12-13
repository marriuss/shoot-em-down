using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class SceneLoader : MonoBehaviour, ISceneLoadHandler<Settings>
{
    [SerializeField] private Settings _settings;

    public void LoadGameScene()
    {
        Game.Load(_settings);
    }

    public void LoadTutorialScene()
    {
        Tutorial.Load(_settings);
    }

    public void LoadMainMenu()
    {
        MainMenu.Load(_settings);
    }

    public void OnSceneLoaded(Settings settings)
    {
        _settings.SetSettings(settings);
    }
}
