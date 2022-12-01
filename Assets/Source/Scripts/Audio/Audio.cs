using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _volume;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _volume = _audioSource.volume;
    }

    public void SetMode(bool isActive)
    {
        _audioSource.volume = isActive ? _volume : 0;
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
