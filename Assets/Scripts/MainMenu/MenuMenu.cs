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

    [Header("Fade Animation")]
    [SerializeField] private GameObject fadeOutObject;
    [SerializeField] private Animator fadeOutAnimator;


    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        if (fadeOutObject != null)
        {
            fadeOutObject.SetActive(false);
        }

        StartCoroutine(FadeAndLoadScene(1));

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

}
