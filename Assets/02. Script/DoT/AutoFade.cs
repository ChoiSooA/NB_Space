using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class AutoFade : MonoBehaviour
{
    public Image fadeImage;
    public UnityEvent ChangeEvent;
    public float fadeDurationTime = 0.8f;

    int eventCount = 0;
    private void Start()
    {
        fadeImage.gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, fadeDurationTime).SetEase(Ease.Linear);
    }
    public void FadeOut()
    {
        fadeImage.DOFade(0, fadeDurationTime).SetEase(Ease.Linear);
    }
    public void FadeInOut()
    {
        fadeImage.gameObject.SetActive(true);
        FadeIn();
        StartCoroutine(FadeCoroutine());
    }
    IEnumerator FadeCoroutine()     //FadeIn -> FadeOut
    {
        yield return new WaitForSeconds(fadeDurationTime*1.4f);
        //누를때마다 다음 이벤트 실행되는 기능 구현 예정
        FadeOut();
        yield return new WaitForSeconds(fadeDurationTime);
        fadeImage.gameObject.SetActive(false);
    }
}
