using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeIOManager : MonoBehaviour
{
    // シングルトン
    public static FadeIOManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public CanvasGroup canvasGroup;
    public float fadeTime = 1f;

    public void FadeIn()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(0, fadeTime)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    public void FadeOut()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, fadeTime)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    public void FadeOutToIn(TweenCallback action)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, fadeTime)
            .OnComplete(() =>
            {
                action();
                FadeIn();
            });
    }
}
