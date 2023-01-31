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

    private void Start()
    {
        _mixer.GetFloat(VolumeParameter, out _volume);
    }

    public void SetMode(bool isOn)
    {
        _mixer.SetFloat(VolumeParameter, isOn ? _volume : MinVolume);
    }
}
