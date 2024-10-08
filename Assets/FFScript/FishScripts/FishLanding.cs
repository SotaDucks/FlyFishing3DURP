using System.Collections;
using UnityEngine;

public class FishLanding : MonoBehaviour
{
    public float activationDelay = 2f; // 激活脚本后的延迟时间
    public GameObject fishStaminaCanvas; // FishStaminaCanvas 组件
    public Transform escapePoint; // 撤离点的位置
    public float moveSpeed = 5f; // 鱼移动的速度

    private Rigidbody fishRigidbody;
    private FishStaminaBar staminaBar;
    private Canvas canvasComponent; // FishStaminaCanvas 上的 Canvas 组件
    private FishDragLine fishDragLine; // 引用 FishDragLine 脚本

    private void Start()
    {
        fishRigidbody = GetComponent<Rigidbody>();
        staminaBar = FishStaminaBar.instance;

        if (staminaBar == null)
        {
            Debug.LogError("FishStaminaBar instance is not found. Please ensure FishStaminaBar script is attached to an active GameObject in the scene.");
            return;
        }

        // 获取 FishStaminaCanvas 上的 Canvas 组件
        if (fishStaminaCanvas != null)
        {
            canvasComponent = fishStaminaCanvas.GetComponent<Canvas>();
            if (canvasComponent == null)
            {
                Debug.LogError("Canvas component not found on FishStaminaCanvas.");
            }
            else
            {
                canvasComponent.enabled = false; // 开始时禁用 Canvas 组件
            }
        }
        else
        {
            Debug.LogError("FishStaminaCanvas is not assigned in the inspector.");
        }

        // 开始延迟协程
        StartCoroutine(ActivateStaminaBar());
    }

    private IEnumerator ActivateStaminaBar()
    {
        // 延迟指定的时间
        yield return new WaitForSeconds(activationDelay);

        // 启用 FishStaminaCanvas 上的 Canvas 组件
        if (canvasComponent != null)
        {
            canvasComponent.enabled = true;
        }
        else
        {
            Debug.LogError("Canvas component is null. Cannot enable.");
        }

        // 开始检测耐力值的协程
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
                // 耐力值不为0
                fishRigidbody.isKinematic = true;

                // 调整方向面对撤离点
                Vector3 direction = (escapePoint.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                // 朝撤离点移动
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
            else
            {
                // 耐力值为0
                fishRigidbody.isKinematic = false;
            }

            yield return null;
        }
    }
}
