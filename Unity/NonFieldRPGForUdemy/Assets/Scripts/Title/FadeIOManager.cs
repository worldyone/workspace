using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeIOManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void Start()
    {
        FadeOutToIn();
    }

    public void FadeIn()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(0, 2f)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    public void FadeOut()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 2f)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    public void FadeOutToIn()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 2f)
            .OnComplete(() => FadeIn());
    }
}
