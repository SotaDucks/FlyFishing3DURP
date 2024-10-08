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
    private FishDragLine fishDragLine; // ���� FishDragLine �ű�

    private void Start()
    {
        fishRigidbody = GetComponent<Rigidbody>();
        staminaBar = FishStaminaBar.instance;

        if (staminaBar == null)
        {
            Debug.LogError("FishStaminaBar instance is not found. Please ensure FishStaminaBar script is attached to an active GameObject in the scene.");
            return;
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

            if (staminaBar.currentStamina > 0)
            {
                // ����ֵ��Ϊ0
                fishRigidbody.isKinematic = true;

                // ����������Գ����
                Vector3 direction = (escapePoint.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                // ��������ƶ�
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
            else
            {
                // ����ֵΪ0
                fishRigidbody.isKinematic = false;
            }

            yield return null;
        }
    }
}
