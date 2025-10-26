using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThreeLifeSequence : MonoBehaviour
{
    public enum HeartIndex
    {
        First = 0,
        Second = 1,
        Third = 2
    }

    [Header("Heart UI")]
    [SerializeField] private Image[] hearts;            // Assign 3 heart UI images in order
    [SerializeField] private Sprite brokenHeartSprite;  // Assign broken heart sprite
    [SerializeField] private HeartIndex heartToBreak;   // Choose which heart breaks on start

    [Header("Fade Animation")]
    [SerializeField] private GameObject fadeOutObject;  // Assign fade object
    [SerializeField] private Animator fadeOutAnimator;  // Assign fade animator

    [Header("Scene Settings")]
    [SerializeField] private int sceneToLoad = 2;       // Scene index to load after fade out
    [SerializeField] private float fadeDelay = 2f;      // Delay before scene change

    private void Start()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    private void BreakHeartAndFade()
    {
        int index = (int)heartToBreak;

        if (index >= 0 && index < hearts.Length && brokenHeartSprite != null)
        {
            hearts[index].sprite = brokenHeartSprite;
            Debug.Log($"Heart {heartToBreak} broken on start.");
        }
        else
        {
            Debug.LogWarning("Invalid heart index or missing references!");
        }

        StartCoroutine(FadeOutAndLoad());
    }

    private IEnumerator FadeOutAndLoad()
    {
        yield return new WaitForSeconds(2f);

        BreakHeartAndFade();

        yield return new WaitForSeconds(1f);

        fadeOutObject.SetActive(true);
        fadeOutAnimator.Play("FadeOut_Sequence");
        yield return new WaitForSeconds(fadeDelay);

    

        SceneManager.LoadScene(sceneToLoad);
    }
}
