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
    public TMP_Text questionText;   //������ ��µ� �ؽ�Ʈ
    public List<Quiz> quizList = new List<Quiz>();  //���� ����Ʈ
    public TMP_Text changeText;  //�Ϸ�Ǿ����� ���� ��ư�� �ؽ�Ʈ�� �������� ����
    public GameObject nextButton;
    public GameObject WrightPannel;
    int currentQuiz = 0;    //���� ���� ��ȣ
    GameObject zoomObj; //���� ������ ������Ʈ(���������� ����)



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
            changeText.text= "��� ��� Ǯ�����!";
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
        if (zoomObj.name == quizList[currentQuiz].answer)   //����
        {
            zoomObj.GetComponent<Collider>().enabled = false;
            currentQuiz++;
            Audio_Manager.Instance.PlayEffect(4);   //���� �Ҹ�
            Audio_Manager.Instance.PlayEffect(3);   //�ڼ� �Ҹ�
            WrightPannel.SetActive(true);
            StartCoroutine(nextQuiz());
        }
        else                                                //����
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
