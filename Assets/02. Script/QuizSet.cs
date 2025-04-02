using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizSet : MonoBehaviour
{
    [System.Serializable]
    public struct Quiz
    {
        public string answer;
        public string question;
    }
    public TMP_Text questionText;   //질문이 출력될 텍스트
    public List<Quiz> quizList = new List<Quiz>();  //퀴즈 리스트
    public TMP_Text changeText;  //완료되었을때 다음 버튼의 텍스트를 변경해줄 변수
    public GameObject nextButton;
    public GameObject WrightPannel;
    int currentQuiz = 0;    //현재 퀴즈 번호
    GameObject zoomObj; //현재 선택한 오브젝트(정답판정을 위해)



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
        for (int i = quizList.Count -1; i > 0; i--)
        {
            int rnd = UnityEngine.Random.Range(0, i);
            Quiz temp = quizList[i];
            quizList[i] = quizList[rnd];
            quizList[rnd] = temp;
        }
    }

    void showQuiz()
    {
        if (currentQuiz == quizList.Count)
        {
            WrightPannel.SetActive(true);
            questionText.text = "";
            changeText.text= "모든 퀴즈를 풀었어요!";
            nextButton.SetActive(true);
        }
        else
        {
            questionText.text = quizList[currentQuiz].question;
            
        }
    }

    public void checkQuiz()
    {
        zoomObj = TouchObjectDetector.touchObj;
        if (zoomObj.name == quizList[currentQuiz].answer)   //정답
        {
            zoomObj.GetComponent<Collider>().enabled = false;
            currentQuiz++;
            Audio_Manager.Instance.PlayEffect(4);   //정답 소리
            Audio_Manager.Instance.PlayEffect(3);   //박수 소리
            WrightPannel.SetActive(true);
            StartCoroutine(nextQuiz());
        }
        else                                                //오답
        {
            Audio_Manager.Instance.PlayEffect(2);
        }
    }

    IEnumerator nextQuiz()
    {
        yield return new WaitForSeconds(1.5f);
        WrightPannel.SetActive(false);
        showQuiz(); 
        if (zoomObj != null)
        {
            zoomObj.GetComponent<Collider>().enabled = true;
        }
    }

}
