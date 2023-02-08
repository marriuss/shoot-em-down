using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStateMachine : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources;


    private void Update()
    {
        foreach(AudioSource source in audioSources)
        {
            if (source.isActiveAndEnabled)
            {

            }
        }
    }
}
