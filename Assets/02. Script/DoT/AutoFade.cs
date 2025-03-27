using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class AutoFade : MonoBehaviour
{
    Image fadeImage;
    public EventSystem[] Change;    //���� �ܰ�(�̼�, �� �� )�� �Ѿ�� ���� �̺�Ʈ, ���ʴ�� ����Ǳ� ������ ������� �־����
    public float fadeDurationTime = 0.8f;

    int eventCount = 0; 

    private void Start()
    {
        fadeImage = GetComponent<Image>();
        fadeImage.DOFade(0, 0f).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false)); //�ʱ�ȭ
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
        if(eventCount < Change.Length&& Change.Length == 0)     //������� �̺�Ʈ ����, Ŭ���� ���� ���� �̺�Ʈ ����
        {
            Change[eventCount].gameObject.SetActive(true);
            eventCount++;
        }
            FadeOut();
        yield return new WaitForSeconds(fadeDurationTime);
        gameObject.SetActive(false);
    }
}
