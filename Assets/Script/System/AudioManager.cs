using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField, Tooltip("SE—p")]
    private AudioSource _seSource;

    public void PlayOneShotSe(AudioClip clip)
    {
        _seSource.PlayOneShot(clip);
    }
}
