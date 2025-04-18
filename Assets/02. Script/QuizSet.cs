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

    public TMP_Text questionText;   // 질문이 출력될 텍스트
    public List<Quiz> quizList = new List<Quiz>();  // 퀴즈 리스트
    public TMP_Text changeText;  // 완료되었을 때 버튼의 텍스트를 변경해줄 변수
    public GameObject nextButton;
    public GameObject WrightPannel;
    int currentQuiz = 0;    // 현재 퀴즈 번호
    GameObject zoomObj;     // 현재 선택한 오브젝트 (정답 판정을 위해)


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

        if (zoomObj.name == quizList[currentQuiz].answer) // 정답
        {
            zoomObj.GetComponent<Collider>().enabled = false;
            Audio_Manager.Instance.PlayEffect(4);   // 정답 소리
            Audio_Manager.Instance.PlayEffect(3);   // 박수 소리
            Audio_Manager.Instance.Ment_audioSources.Stop();

            StartCoroutine(nextQuiz());  // 정답 패널 보여준 후 퀴즈 상태 처리
        }
        else // 오답
        {
            Audio_Manager.Instance.PlayEffect(2);
            Audio_Manager.Instance.PlayMent(quizList[currentQuiz].audioClip);
        }
    }

    IEnumerator nextQuiz()
    {
        currentQuiz++;  // 여기서 퀴즈 카운트 증가
        if (currentQuiz == quizList.Count)
        {
            // 퀴즈 모두 완료 처리
            questionText.text = "";
            changeText.text = "모든 퀴즈를 풀었어요!";
            nextButton.SetActive(true);
        }
        WrightPannel.SetActive(true);
        WrightPannel.transform.GetChild(0).DOScale(WrightPannel.transform.localScale * 1.2f, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.5f);


        if (currentQuiz != quizList.Count)
        {
            WrightPannel.transform.GetChild(0).DOScale(WrightPannel.transform.localScale * 0.8f, 0.5f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.3f);
            // 다음 퀴즈 보여주기
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
