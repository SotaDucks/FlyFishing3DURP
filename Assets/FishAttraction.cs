using UnityEngine;
using System.Collections;

public class FishAttraction : MonoBehaviour
{
    // Flyhook��Transform
    private Transform flyhookTransform;

    // �ƶ��ٶ�
    [Header("�ƶ��ٶ�")]
    public float moveSpeed = 5f;

    // ��ת�ٶ�
    [Header("��ת�ٶ�")]
    public float rotationSpeed = 360f;

    // ��������ʱ�䣨�룩
    [Header("��������ʱ�䣨�룩")]
    public float attractionDuration = 5f;

    // �Ƿ����ڱ�����
    private bool isAttracted = false;

    // Ҫ���õ�Ѳ�����
    [Header("Ѳ�����")]
    public MonoBehaviour patrolComponent;

    // Coroutine����
    private Coroutine attractionCoroutine;

    void Start()
    {
        if (patrolComponent == null)
        {
            Debug.LogError("����Inspector��ָ��һ��Ѳ�������");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flyhook"))
        {
            flyhookTransform = other.transform;
            StartAttraction();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flyhook"))
        {
            StopAttraction();
        }
    }

    void StartAttraction()
    {
        if (!isAttracted)
        {
            isAttracted = true;
            // ͣ��Ѳ�����
            if (patrolComponent != null)
            {
                patrolComponent.enabled = false;
            }

            // ��������Coroutine
            attractionCoroutine = StartCoroutine(AttractionRoutine());
        }
    }

    void StopAttraction()
    {
        if (isAttracted)
        {
            isAttracted = false;
            // ֹͣCoroutine
            if (attractionCoroutine != null)
            {
                StopCoroutine(attractionCoroutine);
            }

            // ����Ѳ�����
            if (patrolComponent != null)
            {
                patrolComponent.enabled = true;
            }
        }
    }

    IEnumerator AttractionRoutine()
    {
        float elapsed = 0f;
        while (elapsed < attractionDuration)
        {
            if (flyhookTransform == null)
            {
                break;
            }

            // ���㷽��
            Vector3 direction = (flyhookTransform.position - transform.position).normalized;

            // �ƶ��㵽flyhook��λ��
            transform.position += direction * moveSpeed * Time.deltaTime;

            // ��ת��ʹ������flyhook
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // ����ʱ��������ָ�Ѳ��
        isAttracted = false;
        if (patrolComponent != null)
        {
            patrolComponent.enabled = true;
        }
    }
}
