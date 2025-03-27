using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingObj : MonoBehaviour
{
    float floatSpeed = 0.2f;
    public List<GameObject> floatingObj = new List<GameObject>();

    private void Start()
    {
        foreach (GameObject obj in floatingObj)
        {
            AnimateFloating(obj);
        }
    }

    void AnimateFloating(GameObject obj)
    {
        // ������ �̵� ���� �� �Ÿ� ���� (Y�� ���Ʒ��� + ��¦ X�൵ �����̰�)
        Vector3 randomOffset = new Vector3(
            Random.Range(-0.3f, 0.3f), // X ���� (�¿� ��鸲)
            Random.Range(0.5f, 1.0f), // Y ���� (���Ʒ� �̵�)
            0f
        );

        // DOTween�� ����Ͽ� �ݺ� �ִϸ��̼� ����
        obj.transform.DOMove(obj.transform.position + randomOffset, Random.Range(2f, 3f)) // 2~3�� ���� �̵�
            .SetEase(Ease.InOutSine) // �ε巯�� �պ� ȿ��
            .SetLoops(-1, LoopType.Yoyo); // ���� �ݺ� (��-�Ʒ�-��)
    }
}
