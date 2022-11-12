using UnityEngine;

public abstract class ResetableMonoBehaviour : MonoBehaviour
{
    private void Start()
    {
        LevelStarter.AddResetableObject(this);
    }

    public abstract void SetStartState();
}