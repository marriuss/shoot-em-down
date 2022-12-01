using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Playlist : ResetableMonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;

    private AudioSource _audioSource;
    private AudioClip _currentAudio;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void SetStartState()
    {
        int index = Random.Range(0, _clips.Count);
        _currentAudio = _clips[index];
        _audioSource.clip = _currentAudio;
    }

    public void Play()
    {
        _audioSource.Play();
    }

    public void Pause()
    {
        _audioSource.Pause();
    }
}
