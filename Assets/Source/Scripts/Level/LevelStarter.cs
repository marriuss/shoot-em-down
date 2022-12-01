using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private LevelLayout _levelLayout;
    [SerializeField] private Menu _gameStartMenu;

    private static LevelStarter instance;

    private List<ResetableMonoBehaviour> _resetableObjects;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _resetableObjects = new List<ResetableMonoBehaviour>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadMainMenu();
        Camera.main.SetWidth(_levelLayout.Width);
    }

    public static void AddResetableObject(ResetableMonoBehaviour resetableObject)
    {
        if (instance != null)
            instance.Add(resetableObject);
    }

    public void LoadMainMenu()
    {
        ResetLevel();
        _gameStartMenu.Open();
    }

    public void ResetLevel()
    {
        foreach (ResetableMonoBehaviour resetableObject in _resetableObjects)
            resetableObject.SetStartState();
    }

    private void Add(ResetableMonoBehaviour restartableObject)
    {
        _resetableObjects.Add(restartableObject);
    }
}
