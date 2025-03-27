using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TextTyping : MonoBehaviour
{
    List<string> ment_string = new List<string>();
    TMP_Text outMent;
    public string[] addment;

    float typingSpeed = 1.5f;

    private void Start()
    {
        //ment_string = GetComponent<TMP_Text>().text;
        outMent = this.GetComponent<TMP_Text>();
        setMent();
        StartCoroutine(NextText());

    }

    void setMent()
    {
        for (int i = 0; i < addment.Length; i++)
        {
            ment_string.Add(addment[i]);
        }
    }

    IEnumerator NextText()
    {
        for (int i = 0; i < ment_string.Count; i++)
        {
            outMent.text = "";
            outMent.DOText(ment_string[i], typingSpeed);
            float nextSpeed = ment_string[i].Length * typingSpeed;
            yield return new WaitForSeconds(nextSpeed / 2);
        }
        yield return new WaitForSeconds(1f);
    }
}
