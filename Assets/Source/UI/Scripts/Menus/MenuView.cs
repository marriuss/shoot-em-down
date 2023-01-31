using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuView : MonoBehaviour
{
    public void Appear()
    {
        SetActive(true);
    }

    public void Disappear()
    {
        SetActive(false);
    }

    protected abstract void SetActive(bool active);
}
