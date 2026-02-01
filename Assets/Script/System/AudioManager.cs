using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField, Tooltip("SE用")]
    private AudioSource _seSource;

    public void PlayOneShotSe(AudioClip clip)
    {
        if(clip != null)
        {
            _seSource.PlayOneShot(clip);
        }
    }
}
