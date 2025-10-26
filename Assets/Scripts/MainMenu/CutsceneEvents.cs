using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneEvents : MonoBehaviour
{
    [Header("Comic Panels")]
    [SerializeField] private Image comicPanelImage;          // UI Image to display the comic panels
    [SerializeField] private List<Sprite> comicSprites;      // List of sprites to show in order
    private int currentPanelIndex = 0;

    [Header("Fade Animation")]
    [SerializeField] private GameObject fadeOutObject;
    [SerializeField] private Animator fadeOutAnimator;

    [SerializeField] private GameObject fadeInObject;
    [SerializeField] private Animator fadeInAnimator;

    [Header("Scene Settings")]
    [SerializeField] private int nextSceneIndex = 2;         // The scene to load after cutscene

    private bool isTransitioning = false;

    void Start()
    {
        if (fadeOutObject != null)
        {
            fadeOutObject.SetActive(false);
        }

        if (fadeInObject != null)
        {
            fadeInObject.SetActive(true);
        }

        if (comicSprites != null && comicSprites.Count > 0 && comicPanelImage != null)
        {
            comicPanelImage.sprite = comicSprites[currentPanelIndex];
        }

        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (isTransitioning) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextPanel();
        }
    }

    void NextPanel()
    {
        currentPanelIndex++;

        if (currentPanelIndex < comicSprites.Count)
        {
            comicPanelImage.sprite = comicSprites[currentPanelIndex];
        }
        else
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        isTransitioning = true;

        if (fadeOutObject != null && fadeOutAnimator != null)
        {
            fadeOutObject.SetActive(true);
            fadeOutAnimator.Play("FadeOut_Sequence");
            yield return new WaitForSeconds(2f);
        }

        SceneManager.LoadScene(2);
    }

    private IEnumerator FadeIn()
    {

        fadeInObject.SetActive(true);
        fadeInAnimator.Play("FadeIn_Sequence");
        yield return new WaitForSeconds(2f);

        fadeInObject.SetActive(false);

    }
}
