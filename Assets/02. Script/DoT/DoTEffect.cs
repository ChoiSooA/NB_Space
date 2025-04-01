/*
 * ������� ���� ������� �����־���, �Ǻ��� ����� ������ �� ��ġ�� �����ؾ���
 * �ٿ��� ����� ���� ��ġ�� �ƴ� ������ ��ġ�� �����ؾ���
 * ���Ĵ� �⺻������ 0���� �����صξ����(��Ÿ���°� ����, ������� �� ��)
 * �� ��ũ��Ʈ�� ����� ������Ʈ�� Ȱ��ȭ�� �Ǿ��־����(��Ȱ��ȭ�� Ȱ��ȭ ��� �۵��ϰ� �ϱ� ���� ActiveSet�� trueActive�� �־�־���)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class DoTEffect : MonoBehaviour
{
    public enum State
    {
        Size,
        Bound,
        Move,
        Slide,
        Blink,
    }
    public State state;                 //������, �ٿ��, ����, ���� �� ����� ȿ��
    public float durationTime = 1f;     //���ӽð�
    public Vector3 targetVector;        //Ÿ�� ����(������, ��ġ ��)
    Vector3 OriginalSize;               //���� ������
    Vector3 OriginalPosition;           //���� ��ġ
    public GameObject activeOff;        //ȿ���� ���� �� ��Ȱ��ȭ�� ������Ʈ

    bool backMove = false;

    private void Awake()
    {
        OriginalSize = transform.localScale;
        OriginalPosition = transform.localPosition;
        if(activeOff != null)
            activeOff.SetActive(true);
    }
    private void OnEnable()
    {
        StartDoT();
    }
    public void Close()
    {
        EndDoT();
    }
    public void OriginalPosClose()
    {
        StartCoroutine(OriginalEnd());
    }

    public void StartDoT()
    {
        backMove = false;
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
             case State.Slide:
                MoveStart();
                break;
            case State.Blink:
                BlinkStart();
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
            case State.Slide:
                StartCoroutine(SlideEnd());
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
    void BlinkStart()
    {
        transform.GetComponent<TextMeshProUGUI>().DOFade(0f, durationTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
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
        transform.DOLocalMove(OriginalPosition, durationTime);
        yield return new WaitForSeconds(durationTime * 0.8f);
        transform.localPosition = OriginalPosition;
        activeOff.gameObject.SetActive(false);
        yield return null;
    }
    IEnumerator SlideEnd()
    {
        transform.DOLocalMove(new Vector3(-OriginalPosition.x, OriginalPosition.y, OriginalPosition.z), durationTime);
        yield return new WaitForSeconds(durationTime);
        transform.localPosition = new Vector3(-OriginalPosition.x, OriginalPosition.y, OriginalPosition.z);
        activeOff.gameObject.SetActive(false);
        yield return null;
    }
}
