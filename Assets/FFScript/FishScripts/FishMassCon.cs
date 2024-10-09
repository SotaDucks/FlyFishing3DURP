using UnityEngine;

public class FishMassCon : MonoBehaviour
{
    // ˮ������
    public float InWaterMass = 1f;
    public float InWaterDrag = 2f;
    public float InWaterAngularDrag = 1f;

    // ˮ������
    public float OutWaterMass = 1f;
    public float OutWaterDrag = 0.5f;
    public float OutWaterAngularDrag = 0.5f;

    private Rigidbody fishRigidbody;
    private Collider waterSurfaceTrigger;

    void Start()
    {
        // ��ȡ��ĸ������
        fishRigidbody = GetComponent<Rigidbody>();

        if (fishRigidbody == null)
        {
            Debug.LogError("δ�ҵ����Rigidbody�����");
            return;
        }

        // �Զ�Ѱ����Ϊ"WaterSurfaceTrigger"�Ķ���
        GameObject waterObject = GameObject.Find("WaterSurfaceTrigger");

        if (waterObject != null)
        {
            waterSurfaceTrigger = waterObject.GetComponent<Collider>();

            if (waterSurfaceTrigger == null)
            {
                Debug.LogError("WaterSurfaceTrigger������δ�ҵ�Collider�����");
            }
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ'WaterSurfaceTrigger'�Ķ���");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == waterSurfaceTrigger)
        {
            // ����ˮ�У�����ˮ������
            SetFishProperties(InWaterMass, InWaterDrag, InWaterAngularDrag);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == waterSurfaceTrigger)
        {
            // �뿪ˮ�棬����ˮ������
            SetFishProperties(OutWaterMass, OutWaterDrag, OutWaterAngularDrag);
        }
    }

    void SetFishProperties(float mass, float drag, float angularDrag)
    {
        fishRigidbody.mass = mass;
        fishRigidbody.drag = drag;
        fishRigidbody.angularDrag = angularDrag;
    }
}
