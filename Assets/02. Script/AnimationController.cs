using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    public GameObject Character;

    public Animator animator;

    public Vector3 resetPosition;

    bool isPlaying = false;

    private void Awake()
    {
        resetPosition = Character.transform.localPosition;
        animator = Character.GetComponent<Animator>();
    }
    private void OnDisable()
    {
        PlayAnimation("Idle");
        Character.transform.localPosition = resetPosition;
    }

    void PlayAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
    }

    public void playAnimSet(int animnum)
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        switch (animnum)
        {
            case 0:
                StartCoroutine(AnimOne());
                break;
            case 1:
                StartCoroutine(AnimTwo());
                break;
            case 2:
                StartCoroutine(AnimThree());
                break;
        }
    }

    IEnumerator AnimOne()                                   //0�� �ִϸ��̼�
    {
        Character.transform.localPosition = resetPosition;
        PlayAnimation("Jump");
        Character.transform.DOMoveY(-1.5f, 0.7f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1f);
        PlayAnimation("Hi");
        yield return new WaitForSeconds(1.7f);
        PlayAnimation("Idle");
        yield return new WaitForSeconds(23f);   //������ nice ������ ��� �ð�
        PlayAnimation("Nice");
        yield return new WaitForSeconds(3f);
        PlayAnimation("Idle");
    }
    IEnumerator AnimTwo()                                   //1�� �ִϸ��̼�
    {
        Character.transform.localPosition = resetPosition;
        PlayAnimation("Jump");
        Character.transform.DOMoveY(-1.5f, 0.7f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1f);
        PlayAnimation("Clap");
        yield return new WaitForSeconds(2f);
        PlayAnimation("Idle");
        yield return new WaitForSeconds(6f);    //������ cheer ������ ��� �ð�
        PlayAnimation("Cheer");
        yield return new WaitForSeconds(3f);
        PlayAnimation("Idle");
    }
    IEnumerator AnimThree()                                 //2�� �ִϸ��̼�
    {
        Character.transform.localPosition = new Vector3(1f,resetPosition.y,resetPosition.z);
        PlayAnimation("Jump");
        Character.transform.DOMoveY(-1.5f, 0.7f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1f);
        PlayAnimation("Clap");
        Audio_Manager.Instance.PlayEffect(3);
        yield return new WaitForSeconds(2.2f);
        PlayAnimation("Idle");
        yield return new WaitForSeconds(12f);    //������ clap ������ ��� �ð�
        PlayAnimation("Hi");
    }
}
