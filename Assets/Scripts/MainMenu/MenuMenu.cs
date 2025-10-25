using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button retryButton;

    [Header("Fade Animation")]
    [SerializeField] private GameObject fadeOutObject;
    [SerializeField] private Animator fadeOutAnimator;

    [Header("Objects")]
    [SerializeField] private GameObject lightning;
    [SerializeField] private AudioSource lightningAudio;

    public void StartGame()
    {
        if (fadeOutObject != null)
        {
            fadeOutObject.SetActive(false);
        }

        StartCoroutine(FadeAndLoadGame(1));
    }

    public void RetryGame()
    {
        if (fadeOutObject != null)
        {
            fadeOutObject.SetActive(false);
        }

        StartCoroutine(FadeAndLoadScene(2)); // GameStartScene
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        if (fadeOutObject != null && fadeOutAnimator != null)
        {
            fadeOutObject.SetActive(true);
            fadeOutAnimator.Play("FadeOut_Sequence");
            yield return new WaitForSeconds(2f);
        }

        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator FadeAndLoadGame(int sceneIndex)
    {
        if (fadeOutObject != null && fadeOutAnimator != null && lightning != null)
        {

            lightning.SetActive(true);

            if (lightningAudio != null)
            {
                lightningAudio.Play();
            }

            yield return new WaitForSeconds(1f);

            fadeOutObject.SetActive(true);
            fadeOutAnimator.Play("FadeOut_Sequence");

 
            yield return new WaitForSeconds(2f);
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
