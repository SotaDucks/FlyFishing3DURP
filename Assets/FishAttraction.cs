using UnityEngine;
using System.Collections;

public class FishAttraction : MonoBehaviour
{
    // Flyhook的Transform
    private Transform flyhookTransform;

    // 移动速度
    [Header("移动速度")]
    public float moveSpeed = 5f;

    // 旋转速度
    [Header("旋转速度")]
    public float rotationSpeed = 360f;

    // 吸引持续时间（秒）
    [Header("吸引持续时间（秒）")]
    public float attractionDuration = 5f;

    // 是否正在被吸引
    private bool isAttracted = false;

    // 要禁用的巡游组件
    [Header("巡游组件")]
    public MonoBehaviour patrolComponent;

    // Coroutine引用
    private Coroutine attractionCoroutine;

    void Start()
    {
        if (patrolComponent == null)
        {
            Debug.LogError("请在Inspector中指定一个巡游组件！");
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
            // 停用巡游组件
            if (patrolComponent != null)
            {
                patrolComponent.enabled = false;
            }

            // 启动吸引Coroutine
            attractionCoroutine = StartCoroutine(AttractionRoutine());
        }
    }

    void StopAttraction()
    {
        if (isAttracted)
        {
            isAttracted = false;
            // 停止Coroutine
            if (attractionCoroutine != null)
            {
                StopCoroutine(attractionCoroutine);
            }

            // 启用巡游组件
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

            // 计算方向
            Vector3 direction = (flyhookTransform.position - transform.position).normalized;

            // 移动鱼到flyhook的位置
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 旋转鱼使其面向flyhook
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 吸引时间结束，恢复巡游
        isAttracted = false;
        if (patrolComponent != null)
        {
            patrolComponent.enabled = true;
        }
    }
}
