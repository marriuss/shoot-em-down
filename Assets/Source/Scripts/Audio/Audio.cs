using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetMode(bool isActive)
    {
        _audioSource.mute = !isActive;
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
