using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyhookMassController : MonoBehaviour
{
    public BoxCollider waterSurfaceCollider;  // ����WaterSurfaceCollider��Box Collider
    public float defaultMass = 1.0f;          // flyhook��Ĭ������
    public float waterMass = 0.5f;            // flyhook��ˮ�е�����
    private Rigidbody flyhookRigidbody;       // flyhook�ĸ������
    private bool isInWater = false;           // ���flyhook�Ƿ���ˮ��

    void Start()
    {
        // ��ȡflyhook�ĸ������
        flyhookRigidbody = GetComponent<Rigidbody>();
        if (flyhookRigidbody == null)
        {
            Debug.LogError("Flyhook��ȱ��Rigidbody�����");
        }

        // ���ó�ʼ����ΪĬ������
        flyhookRigidbody.mass = defaultMass;
    }

    // ������������ˮ��
    private void OnTriggerEnter(Collider other)
    {
        // �ж�flyhook�Ƿ����WaterSurfaceCollider
        if (other == waterSurfaceCollider)
        {
            isInWater = true;

            // �����µ�����
            flyhookRigidbody.mass = waterMass;
            Debug.Log($"Flyhook ����ˮ�У���������Ϊ {waterMass}");
        }
    }

    // ����������뿪ˮ��
    private void OnTriggerExit(Collider other)
    {
        // �ж�flyhook�Ƿ��뿪WaterSurfaceCollider
        if (other == waterSurfaceCollider)
        {
            isInWater = false;

            // �ָ�Ĭ������
            flyhookRigidbody.mass = defaultMass;
            Debug.Log($"Flyhook �뿪ˮ�У������ָ�ΪĬ��ֵ {defaultMass}");
        }
    }
}
