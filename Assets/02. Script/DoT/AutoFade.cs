using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class AutoFade : MonoBehaviour
{
    Image fadeImage;
    public EventSystem[] Change;    //다음 단계(미션, 씬 등 )로 넘어가기 위한 이벤트, 차례대로 실행되기 때문에 순서대로 넣어야함
    public float fadeDurationTime = 0.8f;

    int eventCount = 0; 

    private void Start()
    {
        fadeImage = GetComponent<Image>();
        fadeImage.DOFade(0, 0f).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false)); //초기화
    }
    void OnEnable()
    {
        FadeInOut();
    }

    public void FadeIn()
    {
        fadeImage.DOFade(1, fadeDurationTime).SetEase(Ease.Linear);
    }
    public void FadeOut()
    {
        fadeImage.DOFade(0, fadeDurationTime).SetEase(Ease.Linear);
    }
    public void FadeInOut()
    {
        StartCoroutine(FadeCoroutine());
    }
    IEnumerator FadeCoroutine()     //FadeIn -> FadeOut
    {
        FadeIn();
        yield return new WaitForSeconds(fadeDurationTime*1.4f);
        if(eventCount < Change.Length&& Change.Length == 0)     //순서대로 이벤트 실행, 클릭시 마다 다음 이벤트 실행
        {
            Change[eventCount].gameObject.SetActive(true);
            eventCount++;
        }
            FadeOut();
        yield return new WaitForSeconds(fadeDurationTime);
        gameObject.SetActive(false);
    }
}
