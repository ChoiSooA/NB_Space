/*
 * ������� ���� ������� �����־���, �Ǻ��� ����� ������ �� ��ġ�� �����ؾ���
 * �ٿ��� ����� ���� ��ġ�� �ƴ� ������ ��ġ�� �����ؾ���
 * ���Ĵ� �⺻������ 0���� �����صξ����(��Ÿ���°� ����, ������� �� ��)
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
