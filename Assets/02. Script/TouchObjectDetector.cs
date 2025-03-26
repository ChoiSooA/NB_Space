using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchObjectDetector : MonoBehaviour
{
    public LayerMask targetLayer;
    public float rayDistance = 100f;
    public Camera mainCamera;

    public GameObject touchObj;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;

            if (mainCamera == null)
                Debug.LogError("MainCamera�� �����ϴ�! ī�޶� �Ҵ��ϼ���.");
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI Ŭ�� ���� - �н�");
                return;
            }

            Vector2 mousePosition = Input.mousePosition;
            DetectObject(mousePosition);
        }
#endif

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    Debug.Log("UI ��ġ ���� - �н�");
                    return;
                }

                Vector2 touchPosition = touch.position;
                DetectObject(touchPosition);
            }
        }
    }

    private void DetectObject(Vector2 screenPosition)
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("ī�޶� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        // ��� ���̾ �����ϵ��� ����
        if (Physics.Raycast(ray, out hit, rayDistance, targetLayer == 0 ? -1 : targetLayer))
        {
            GameObject target = hit.collider.gameObject;
            touchObj = target;
            Debug.Log($"������Ʈ ������! �̸�: {target.name}");

            if (target.GetComponent<TouchSelf>() != null)
            {
                target.GetComponent<TouchSelf>().OnClick();
                target.transform.DOScale(target.transform.localScale * 0.9f, 0.1f).SetLoops(2, LoopType.Yoyo);
            }
        }
        else
        {
            Debug.Log("��Ʈ�� ������Ʈ�� �����ϴ�.");
        }
    }
}
