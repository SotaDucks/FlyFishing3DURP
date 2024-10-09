using System.Collections;
using UnityEngine;

public class FishLanding : MonoBehaviour
{
    public float activationDelay = 2f; // ����ű�����ӳ�ʱ��
    public GameObject fishStaminaCanvas; // FishStaminaCanvas ���
    public Transform escapePoint; // ������λ��
    public float moveSpeed = 5f; // ���ƶ����ٶ�

    private Rigidbody fishRigidbody;
    private FishStaminaBar staminaBar;
    private Canvas canvasComponent; // FishStaminaCanvas �ϵ� Canvas ���
    private FishDragLine fishDragLine; // FishDragLine ���

    private Collider waterSurfaceTriggerCollider; // WaterSurfaceTrigger ����ײ��
    private bool isInWater = false; // ���Ƿ���ˮ��

    private void Start()
    {
        fishRigidbody = GetComponent<Rigidbody>();
        staminaBar = FishStaminaBar.instance;

        if (staminaBar == null)
        {
            Debug.LogError("FishStaminaBar instance is not found. Please ensure FishStaminaBar script is attached to an active GameObject in the scene.");
            return;
        }

        // ��ȡ FishDragLine ���
        fishDragLine = GameObject.Find("FlyLine").GetComponent<FishDragLine>();
        if (fishDragLine == null)
        {
            Debug.LogError("FishDragLine component not found on 'FlyLine' GameObject.");
        }

        // ��ȡ WaterSurfaceTrigger ����ײ��
        GameObject waterSurfaceTrigger = GameObject.Find("WaterSurfaceTrigger");
        if (waterSurfaceTrigger != null)
        {
            waterSurfaceTriggerCollider = waterSurfaceTrigger.GetComponent<Collider>();
            if (waterSurfaceTriggerCollider == null)
            {
                Debug.LogError("Collider component not found on 'WaterSurfaceTrigger' GameObject.");
            }
        }
        else
        {
            Debug.LogError("'WaterSurfaceTrigger' GameObject not found in the scene.");
        }

        // ��ȡ FishStaminaCanvas �ϵ� Canvas ���
        if (fishStaminaCanvas != null)
        {
            canvasComponent = fishStaminaCanvas.GetComponent<Canvas>();
            if (canvasComponent == null)
            {
                Debug.LogError("Canvas component not found on FishStaminaCanvas.");
            }
            else
            {
                canvasComponent.enabled = false; // ��ʼʱ���� Canvas ���
            }
        }
        else
        {
            Debug.LogError("FishStaminaCanvas is not assigned in the inspector.");
        }

        // ��ʼ�ӳ�Э��
        StartCoroutine(ActivateStaminaBar());
    }

    private IEnumerator ActivateStaminaBar()
    {
        // �ӳ�ָ����ʱ��
        yield return new WaitForSeconds(activationDelay);

        // ���� FishStaminaCanvas �ϵ� Canvas ���
        if (canvasComponent != null)
        {
            canvasComponent.enabled = true;
        }
        else
        {
            Debug.LogError("Canvas component is null. Cannot enable.");
        }

        // ��ʼ�������ֵ��Э��
        StartCoroutine(CheckStamina());
    }

    private IEnumerator CheckStamina()
    {
        while (true)
        {
            if (staminaBar == null)
            {
                Debug.LogError("staminaBar is null.");
                yield break;
            }

            if (fishDragLine == null)
            {
                Debug.LogError("fishDragLine is null.");
                yield break;
            }

            if (staminaBar.currentStamina > 0 && isInWater)
            {
                // ����ֵ��Ϊ0 �� ����ˮ��
                fishRigidbody.isKinematic = true;

                // ���� fishDragLine.StartStruggling() �쳤����
                fishDragLine.StartStruggling();

                // ����������Գ����
                Vector3 direction = (escapePoint.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                // ��������ƶ�
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
            else
            {
                // ����ֵΪ0 �� �㲻��ˮ��
                fishRigidbody.isKinematic = false;

                // ���� fishDragLine.StopStruggling() ֹͣ�쳤����
                fishDragLine.StopStruggling();
            }

            yield return null;
        }
    }

    // �������ײ����봥����ʱ����
    private void OnTriggerEnter(Collider other)
    {
        if (other == waterSurfaceTriggerCollider)
        {
            isInWater = true;
        }
    }

    // �������ײ���˳�������ʱ����
    private void OnTriggerExit(Collider other)
    {
        if (other == waterSurfaceTriggerCollider)
        {
            isInWater = false;
        }
    }
}
