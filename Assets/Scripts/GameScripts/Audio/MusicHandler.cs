using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private static MusicHandler instance;

    [SerializeField] private AudioSource musicSource;
    

    // Took this from Bubble Casino :P

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void PlayMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void FadeOutMusic(float fadeDuration = 1f)
    {
        if (musicSource != null)
            StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    private System.Collections.IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
    }
    
}