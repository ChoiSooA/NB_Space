using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class TextTyping : MonoBehaviour
{
    [SerializeField] private UnityEvent event_finish;   //Ÿ������ ������ �����ϰ� ���� �̺�Ʈ
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
            Debug.Log("�������θ�Ʈ�����");
            outMent.DOText(originalText, originalText.Length * typingSpeed);
            if (event_finish != null)
            {
                event_finish.Invoke();
            }
        }
    }
    private void OnEnable()
    {
        transform.parent.DOScale(transform.localScale * 0.95f, 0.2f).SetLoops(4, LoopType.Yoyo); //������ �� Ŀ���� ȿ�� �ַ��� ����
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
        yield return new WaitForSeconds(1f);  //�ʹ� �ٷ� �����ϴ� �� ���Ƽ� ���� �ð��� ��
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
