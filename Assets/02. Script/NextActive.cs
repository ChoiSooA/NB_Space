using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NextActive : MonoBehaviour
{
    [Header("Setting Start")]
    public GameObject[] startTrueObject; //�����Ҷ� Ȱ��ȭ�� ������Ʈ
    public GameObject[] startFalseObject; //�����Ҷ� ��Ȱ��ȭ�� ������Ʈ
    public UnityEvent startEvent;
    [Header("Setting Next")]
    public GameObject[] trueObject;
    public GameObject[] falseObject;

    private void OnEnable()
    {
        if (startFalseObject.Length == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < startFalseObject.Length; i++)
            {
                startFalseObject[i].SetActive(false);
            }
        }
        if (startTrueObject.Length == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < startTrueObject.Length; i++)
            {
                startTrueObject[i].SetActive(true);
            }
        }

        startEvent.Invoke();
    }

    public void NextStill()
    {
        StartCoroutine(Next(1.2f));
    }

    IEnumerator Next(float time)
    {
        yield return new WaitForSeconds(time);  //fadein �ð���ŭ ��ٸ�
        if (falseObject.Length == 0)
        {
            Debug.LogError("falseObject�� �������ּ���");
        }
        else
        {
            for (int i = 0; i < falseObject.Length; i++)
            {
                falseObject[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(time);  //fadeout �ð���ŭ ��ٸ�
        if (trueObject.Length == 0)
        {
            Debug.LogError("trueObject�� �������ּ���");
        }
        else
        {
            for (int i = 0; i < trueObject.Length; i++)
            {
                trueObject[i].SetActive(true);
            }
        }
    }
}
