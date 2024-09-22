using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject troutLocationPrefab; // ��Ҫ���ɵ�TroutLocation1Ԥ�Ƽ�
    public GameObject troutLocation1; // �����е�TroutLocation1����
    public float checkInterval = 1f; // ���TroutLocation1�Ƿ���ڵ�ʱ����
    public Vector3 spawnPosition = new Vector3(0, 0, 0); // ������TroutLocation1��λ��

    void Start()
    {
        // ��ʱ���TroutLocation1�Ƿ�����
        InvokeRepeating("CheckTroutLocation", checkInterval, checkInterval);
    }

    void CheckTroutLocation()
    {
        // ���TroutLocation1�������٣��������ڣ��������µ�TroutLocation1
        if (troutLocation1 == null)
        {
            SpawnNewTroutLocation();
        }
    }

    void SpawnNewTroutLocation()
    {
        // ��ָ��λ�������µ�TroutLocation1
        troutLocation1 = Instantiate(troutLocationPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("��TroutLocation1�����ɣ�");
    }
}
