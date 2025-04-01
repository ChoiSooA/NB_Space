using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using LeiaUnity.Examples;

public class ZoomClick : MonoBehaviour
{
    public GameObject[] SpaceObjects;
    public Transform centerPos;
    Vector3 originalPos;
    Vector3 originalScale;
    Vector3 originalRotation;
    public GameObject infoPannel;
    public TMP_Text Text_Info_Title;
    public TMP_Text Text_Info_Content;

    [System.Serializable]
    public struct SpaceObjectInfo
    {
        public string objectName;
        public string title;
        [TextArea(2, 5)]
        public string content;
    }

    public SpaceObjectInfo[] spaceObjectInfos;

    public void Zoom()
    {
        GameObject zoomObj = TouchObjectDetector.touchObj;
        originalPos = zoomObj.transform.position;
        originalScale = zoomObj.transform.localScale;
        originalRotation = zoomObj.transform.rotation.eulerAngles;
        SetInfoByName(zoomObj.name);
        infoPannel.SetActive(true);
        zoomObj.transform.DOMove(centerPos.position, 1f);
        zoomObj.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f);
        for (int i = 0; i < SpaceObjects.Length; i++)
        {
            SpaceObjects[i].GetComponent<Collider>().enabled = false;   //돌아갈 때 중복 선택 안 되게 콜라이더는 모두 꺼줌
            if (SpaceObjects[i] != zoomObj)
            {
                SpaceObjects[i].SetActive(false);
            }
        }
        StartCoroutine(Zoom(zoomObj));
    }
    public void ZoomOut()
    {
        GameObject zoomObj = TouchObjectDetector.touchObj;
        zoomObj.transform.DOMove(originalPos, 1f);
        zoomObj.transform.DORotate(originalRotation, 1f);
        zoomObj.transform.DOScale(originalScale, 1f);
        foreach (GameObject obj in SpaceObjects)
        {
            obj.SetActive(true);
        }
        StartCoroutine(Back(zoomObj));
    }

    void SetInfoByName(string objName)
    {
        Debug.Log(objName);
        foreach (var info in spaceObjectInfos)
        {
            if (info.objectName == objName)
            {
                Text_Info_Title.text = info.title;
                Text_Info_Content.text = info.content;
                return;
            }
        }
    }

        IEnumerator Zoom(GameObject zoomObj)
    {
        yield return new WaitForSeconds(1f);
        zoomObj.GetComponent<ModelViewerControls>().enabled = true; //바른 위치 찾아간 후에 모델뷰어컨트롤 켜주기
    }

    IEnumerator Back(GameObject zoomObj)
    {
        yield return new WaitForSeconds(1f);    //다시 원래 위치로 돌아간 후에 선택 가능하게 해주기
        foreach (GameObject obj in SpaceObjects)
        {
            obj.GetComponent<Collider>().enabled = true;   //꺼줬던 콜라이더 켜주기
        }
        zoomObj.GetComponent<Collider>().enabled = true;
        zoomObj.GetComponent<ModelViewerControls>().enabled = false;

    }
}
