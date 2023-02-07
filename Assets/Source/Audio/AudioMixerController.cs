using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    private const float MinVolume = -80;
    private const float MaxVolume = 20;
    private const string VolumeParameter = "Volume";

    private float _volume;

    private void Awake()
    {
        _volume = MaxVolume;
    }

    public void SetActive(bool isOn)
    {
        if (isOn)
        {
            Unmute();
        }
        else
        {
            Mute();
        }
    }

    private void Mute()
    {
        SetMixerVolume(MinVolume);
    }

    private void Unmute()
    {
        SetMixerVolume(_volume);
    }

    private void SetMixerVolume(float volume)
    {
        _mixer.SetFloat(VolumeParameter, volume);
    }
}
