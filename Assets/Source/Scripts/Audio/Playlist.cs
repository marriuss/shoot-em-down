using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Playlist : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;

    private AudioSource _audioSource;
    private AudioClip _currentAudio;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ChooseNewTrack()
    {
        int index = Random.Range(0, _clips.Count);
        _currentAudio = _clips[index];
        _audioSource.clip = _currentAudio;
        Play();
    }

    public void ResetTrack()
    {
        Stop();
        Play();
    }

    private void Play()
    {
        _audioSource.Play();
    }

    private void Stop()
    {
        _audioSource.Stop();
    }
}
