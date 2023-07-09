using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public float secondsOfGameOverScreen;
    public float fadeDuration = 1.0f;
    private CanvasGroup canvasGroup;
    public CanvasGroup secondCanvasGroup; // Assign this in the Inspector

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        secondCanvasGroup.alpha = 0; // Ensure the second CanvasGroup is initially invisible

        StartCoroutine(FadeIn(canvasGroup));
    }

    IEnumerator FadeIn(CanvasGroup canvasGroupToFade)
    {
        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            canvasGroupToFade.alpha = (Time.time - startTime) / fadeDuration;
            yield return null;
        }

        canvasGroupToFade.alpha = 1;

        if (canvasGroupToFade == canvasGroup)
        {
            Invoke("StartFadeInSecondCanvas", secondsOfGameOverScreen);
        }
    }

    void StartFadeInSecondCanvas()
    {
        StartCoroutine(FadeIn(secondCanvasGroup));
    }
}
