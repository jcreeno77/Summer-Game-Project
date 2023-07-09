using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public float secondsOfGameOverScreen;
    public float fadeDuration = 1.0f;
    private CanvasGroup canvasGroup;
    public GameObject highScoreScreen;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            canvasGroup.alpha = (Time.time - startTime) / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 1;

        Invoke("StartFadeOut", secondsOfGameOverScreen);
    }

    void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        highScoreScreen.SetActive(true);
        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            canvasGroup.alpha = 1 - ((Time.time - startTime) / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;

        DisableSelf();
    }

    void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
