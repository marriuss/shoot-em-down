using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private LevelLayoutGenerator _levelLayoutGenerator;
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
        ResetAll();
        _gameStartMenu.Open();
    }

    public static void AddResetableObject(ResetableMonoBehaviour resetableObject)
    {
        if (instance != null)
            instance.Add(resetableObject);
    }

    public static void RestartLevel()
    {
        ResetAll();
    }

    public static void StartNewLevel()
    {
        instance._levelLayoutGenerator.GenerateNewInnerLayout();
        ResetAll();
    }

    private static void ResetAll()
    {
        foreach (ResetableMonoBehaviour resetableObject in instance._resetableObjects)
            resetableObject.SetStartState();
    }

    private void Add(ResetableMonoBehaviour restartableObject)
    {
        _resetableObjects.Add(restartableObject);
    }
}
