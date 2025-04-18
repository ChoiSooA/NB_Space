using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class QuizSet : MonoBehaviour
{
    [System.Serializable]
    public struct Quiz
    {
        public string answer;
        public string question;
        public AudioClip audioClip;
    }

    public TMP_Text questionText;   // ������ ��µ� �ؽ�Ʈ
    public List<Quiz> quizList = new List<Quiz>();  // ���� ����Ʈ
    public TMP_Text changeText;  // �Ϸ�Ǿ��� �� ��ư�� �ؽ�Ʈ�� �������� ����
    public GameObject nextButton;
    public GameObject WrightPannel;
    int currentQuiz = 0;    // ���� ���� ��ȣ
    GameObject zoomObj;     // ���� ������ ������Ʈ (���� ������ ����)


    private void Start()
    {
        WrightPannel.SetActive(false);
        nextButton.SetActive(false);
        MixQuiz();
        showQuiz();
    }

    public void Reset()
    {
        currentQuiz = 0;
        WrightPannel.SetActive(false);
        nextButton.SetActive(false);
        MixQuiz();
        showQuiz();
    }

    void MixQuiz()
    {
        for (int i = quizList.Count - 1; i > 0; i--)
        {
            int rnd = UnityEngine.Random.Range(0, i);
            Quiz temp = quizList[i];
            quizList[i] = quizList[rnd];
            quizList[rnd] = temp;
        }
    }

    void showQuiz()
    {
        if (currentQuiz < quizList.Count)
        {
            questionText.text = quizList[currentQuiz].question;
            if (quizList[currentQuiz].audioClip != null)
            {
                Audio_Manager.Instance.PlayMent(quizList[currentQuiz].audioClip);
            }
        }
    }

    public void checkQuiz()
    {
        zoomObj = TouchObjectDetector.touchObj;

        if (zoomObj.name == quizList[currentQuiz].answer) // ����
        {
            zoomObj.GetComponent<Collider>().enabled = false;
            Audio_Manager.Instance.PlayEffect(4);   // ���� �Ҹ�
            Audio_Manager.Instance.PlayEffect(3);   // �ڼ� �Ҹ�
            Audio_Manager.Instance.Ment_audioSources.Stop();

            StartCoroutine(nextQuiz());  // ���� �г� ������ �� ���� ���� ó��
        }
        else // ����
        {
            Audio_Manager.Instance.PlayEffect(2);
            Audio_Manager.Instance.PlayMent(quizList[currentQuiz].audioClip);
        }
    }

    IEnumerator nextQuiz()
    {
        currentQuiz++;  // ���⼭ ���� ī��Ʈ ����
        if (currentQuiz == quizList.Count)
        {
            // ���� ��� �Ϸ� ó��
            questionText.text = "";
            changeText.text = "��� ��� Ǯ�����!";
            nextButton.SetActive(true);
        }
        WrightPannel.SetActive(true);
        WrightPannel.transform.GetChild(0).DOScale(WrightPannel.transform.localScale * 1.2f, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.5f);


        if (currentQuiz != quizList.Count)
        {
            WrightPannel.transform.GetChild(0).DOScale(WrightPannel.transform.localScale * 0.8f, 0.5f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.3f);
            // ���� ���� �����ֱ�
            showQuiz();
            if (zoomObj != null)
            {
                zoomObj.GetComponent<Collider>().enabled = true;
            }
            WrightPannel.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            WrightPannel.SetActive(false);
        }
    }
}
