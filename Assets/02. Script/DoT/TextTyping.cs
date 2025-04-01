using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class TextTyping : MonoBehaviour
{
    [SerializeField] private UnityEvent event_finish;   //타이핑이 끝나고 실행하고 싶은 이벤트
    TMP_Text outMent;
    public string[] addment;
    bool coroutine_running = false;
    string originalText;

    float typingSpeed = 0.1f;

    private void Start()
    {
        outMent = this.GetComponent<TMP_Text>();
        originalText = outMent.text;
        outMent.text = "";

        if (addment.Length > 0)
        {
            StartCoroutine(NextText());
        }
        else
        {
            Debug.Log("오리지널멘트실행됨");
            outMent.DOText(originalText, originalText.Length * typingSpeed);
            if (event_finish != null)
            {
                event_finish.Invoke();
            }
        }
    }
    private void OnEnable()
    {
        transform.parent.DOScale(transform.localScale * 0.95f, 0.2f).SetLoops(4, LoopType.Yoyo); //시작할 때 커지는 효과 주려고 넣음
    }
    private void OnDisable()
    {
        if (coroutine_running == true)
        {
            StopCoroutine(NextText());
            coroutine_running = false;
        }
    }

    IEnumerator NextText()
    {
        yield return new WaitForSeconds(1f);  //너무 바로 시작하는 것 같아서 조금 시간을 줌
        coroutine_running = true;
        for (int i = 0; i < addment.Length; i++)
        {
            outMent.text = "";
            float nextSpeed = addment[i].Length * typingSpeed;
            outMent.DOText(addment[i], nextSpeed).SetEase(Ease.Linear);
            yield return new WaitForSeconds(nextSpeed+0.6f);
        }
        if (event_finish != null)
        {
            event_finish.Invoke();
        }
        coroutine_running = false;
        yield return new WaitForSeconds(0.3f);
    }
}
