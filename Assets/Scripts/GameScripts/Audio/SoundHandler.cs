using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private static SoundHandler instance;

    [SerializeField] private AudioSource sfxSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }


    public void PlaySound(AudioClip clip, bool loop = false)
    {
        if (clip == null) return;

        sfxSource.clip = clip;
        sfxSource.loop = loop;
        sfxSource.Play();
    }

    public void StopSound()
    {
        if (sfxSource.isPlaying)
            sfxSource.Stop();
    }

    public bool IsPlaying()
    {
        return sfxSource.isPlaying;
    }
    
}