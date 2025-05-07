using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GoQuickMission : MonoBehaviour
{
    public GameObject[] trueObject;  // Ȱ��ȭ�� ������Ʈ
    public GameObject[] falseObject; // ��Ȱ��ȭ�� ������Ʈ
    public Button[] BT_Mission;      // ��ư��

    public DoTEffect doTEffect;

    private void Start()
    {
        doTEffect = GetComponent<DoTEffect>();

        for (int i = 0; i < BT_Mission.Length; i++)
        {
            int missionIndex = i;
            BT_Mission[i].onClick.AddListener(() => { GoMission(missionIndex); });
        }
    }

    public void GoMission(int missionNum)
    {
        Debug.Log("GoMission called with missionNum: " + missionNum);

        // ��� falseObject �� Ȱ��ȭ�� �� ����
        foreach (GameObject obj in falseObject)
        {
            if (obj != null && obj.activeSelf)
            {
                obj.SetActive(false);
            }
        }

        doTEffect.Close();

        StartCoroutine(ActivateTrueObject(missionNum));
    }

    IEnumerator ActivateTrueObject(int missionNum)
    {
        yield return new WaitForSeconds(0.01f);

        // trueObject[missionNum]�� �ݵ�� �ٽ� ���ش�
        if (missionNum >= 0 && missionNum < trueObject.Length && trueObject[missionNum] != null)
        {
            trueObject[missionNum].SetActive(false); // �ϴ� ���ٰ�
            yield return new WaitForSeconds(0.01f);
            trueObject[missionNum].SetActive(true);
        }
    }
}
