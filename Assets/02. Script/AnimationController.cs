using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    public GameObject Character;

    public Animator animator;

    public Vector3 resetPosition;

    int count = 0;

    bool isPlaying = false;

    private void Awake()
    {
        resetPosition = Character.transform.localPosition;
        animator = Character.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlayAnimation("Jump");
        Character.transform.DOMoveY(-1.5f, 0.7f).SetEase(Ease.Linear);
        playAnimSet();
    }
    private void OnDisable()
    {
        Character.transform.localPosition = resetPosition;
        PlayAnimation("Idle");
    }

    public void PlayAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
    }

    void playAnimSet()
    {
        if (isPlaying == false)
        {
            switch (count)
            {
                case 0:
                    StartCoroutine(AnimOne());
                    break;
                case 1:
                    StartCoroutine(AnimTwo());
                    break;
            }
            count++;
        }
    }

    IEnumerator AnimOne()
    {
        isPlaying = true;
        yield return new WaitForSeconds(1f);
        PlayAnimation("Hi");
        yield return new WaitForSeconds(1.7f);
        PlayAnimation("Idle");
        yield return new WaitForSeconds(15f);   //������ nice ������ ��� �ð�
        PlayAnimation("Nice");
        yield return new WaitForSeconds(3f);
        PlayAnimation("Idle");
        isPlaying = false;
    }
    IEnumerator AnimTwo()
    {
        isPlaying = true;
        yield return new WaitForSeconds(1f);
        PlayAnimation("Clap");
        yield return new WaitForSeconds(2f);
        PlayAnimation("Idle");
        yield return new WaitForSeconds(4f);    //������ cheer ������ ��� �ð�
        PlayAnimation("Cheer");
        yield return new WaitForSeconds(3f);
        PlayAnimation("Idle");
        isPlaying = false;
    }
}
