using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextActive : MonoBehaviour
{
    public GameObject[] trueObject;
    public GameObject[] falseObject;

    public void NextStill()
    {
        StartCoroutine(Next(1f));
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
