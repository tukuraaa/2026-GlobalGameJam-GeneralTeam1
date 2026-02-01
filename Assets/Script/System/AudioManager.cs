using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField, Tooltip("BGM用 上")]
    private AudioSource _bgmSource;
    [SerializeField, Tooltip("SE用 下")]
    private AudioSource _seSource;

    public void PlayBgm(AudioClip clip)
    {
        if(clip!= null)
        {
            _bgmSource.clip = clip;
            _bgmSource.Play();
        }
    }

    public void StopBgm()
    {
        _bgmSource.Stop();
    }


    public void PlayOneShotSe(AudioClip clip)
    {
        if(clip != null)
        {
            _seSource.PlayOneShot(clip);
        }
    }
}
