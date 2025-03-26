/*
 * 사이즈는 작은 사이즈로 만들어둬야함, 피봇은 사이즈가 퍼지게 할 위치로 설정해야함
 * 바운드와 무브는 원래 위치가 아닌 등장할 위치로 설정해야함
 * 알파는 기본적으로 0으로 설정해두어야함(나타나는게 시작, 사라지는 게 끝)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Microsoft.Unity.VisualStudio.Editor;

public class DoTEffect : MonoBehaviour
{

    public enum State
    {
        Size,
        Bound,
        Move,
        Alpha,
    }
    public State state;
    public float durationTime = 1f;
    public Vector3 targetVector;
    Vector3 OriginalSize;
    Vector3 OriginalPosition;
    public GameObject activeOff;

    private void Start()
    {
        OriginalSize = transform.localScale;
        OriginalPosition = transform.position;
    }
    private void OnEnable()
    {
        StartDoT();
    }
    public void Close()
    {
        EndDoT();
    }

    void StartDoT()
    {
        switch (state)
        {
            case State.Size:
                SizeStart();
                break;
            case State.Bound:
                BoundStart();
                break;
            case State.Move:
                MoveStart();
                break;
            case State.Alpha:
                break;
        }
    }
    void EndDoT()
    {
        switch (state)
        {
            case State.Size:
                StartCoroutine(SizeEnd());
                break;
            case State.Alpha:
                StartCoroutine(AlphaEnd());
                break;
            default:
                StartCoroutine(OriginalEnd());
                break;
        }
    }

    void SizeStart()
    {
        transform.DOScale(targetVector, durationTime);
    }
    void BoundStart()
    {
        transform.DOLocalMove(targetVector, durationTime).SetEase(Ease.OutBounce);
    }
    void MoveStart()
    {
        transform.DOLocalMove(targetVector, durationTime);
    }
    void AlphaStart()
    {
        transform.GetComponent<UnityEngine.UI.Image>().DOFade(1, durationTime);
    }
    IEnumerator SizeEnd()
    {
        transform.DOScale(OriginalSize, durationTime);
        yield return new WaitForSeconds(durationTime * 0.8f);
        transform.localScale = OriginalSize;
        activeOff.SetActive(false);
        yield return null;
    }
    IEnumerator OriginalEnd()
    {
        transform.DOMove(OriginalPosition, durationTime);
        yield return new WaitForSeconds(durationTime * 0.8f);
        transform.position = OriginalPosition;
        activeOff.gameObject.SetActive(false);
        yield return null;
    }
    IEnumerator AlphaEnd()
    {
        transform.GetComponent<UnityEngine.UI.Image>().DOFade(0, durationTime);
        yield return new WaitForSeconds(durationTime);
        activeOff.SetActive(false);
        yield return null;
    }
}
