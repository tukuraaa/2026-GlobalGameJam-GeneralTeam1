using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField, Tooltip("BGM用 上")]
    private AudioSource _bgmSource;
    [SerializeField, Tooltip("SE用 下")]
    public AudioSource _seSource;
    [SerializeField]
    public AudioSource _SeSource;
    [SerializeField]
    private float pitchVolume = 0.8f;
    [SerializeField]
    private float defaultPitch = 1.0f;

    public void PlayBgm(AudioClip clip)
    {
        if (clip != null)
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
        if (clip != null)
        {
            _seSource.PlayOneShot(clip);
        }
    }

    public void PlayOneShotSE(AudioClip clip)
    {
        if (clip != null)
        {
            _SeSource.PlayOneShot(clip);
        }
    }

    public void StopSe()
    {
        _seSource.Stop();
    }

    public void ChangePitch()
    {
        _SeSource.pitch = pitchVolume;
    }

    public void ResetPitch()
    {
        _SeSource.pitch = defaultPitch;
    }
}