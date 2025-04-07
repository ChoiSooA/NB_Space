using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NextActive : MonoBehaviour
{
    [Header("Setting Start")]
    public GameObject[] startTrueObject; //시작할때 활성화할 오브젝트
    public GameObject[] startFalseObject; //시작할때 비활성화할 오브젝트
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
        yield return new WaitForSeconds(time);  //fadein 시간만큼 기다림
        if (falseObject.Length == 0)
        {
            Debug.LogError("falseObject를 설정해주세요");
        }
        else
        {
            for (int i = 0; i < falseObject.Length; i++)
            {
                falseObject[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(time);  //fadeout 시간만큼 기다림
        if (trueObject.Length == 0)
        {
            Debug.LogError("trueObject를 설정해주세요");
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
