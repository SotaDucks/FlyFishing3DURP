using UnityEngine;

public class CameraFlyCon : MonoBehaviour
{
    public Transform target; // 跟随的目标（鱼）
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Camera targetCamera; // 摄像机组件
    private FishAttraction fishAttraction; // FishAttraction组件
    private bool isCameraActivated = false;

    private void Start()
    {
        // 获取摄像机组件
        targetCamera = GetComponent<Camera>();
        if (targetCamera == null)
        {
            Debug.LogError("未在该游戏对象上找到Camera组件。");
            return;
        }

        // 初始时禁用摄像机组件
        targetCamera.enabled = false;

        // 检查目标是否已设置
        if (target != null)
        {
            // 获取FishAttraction组件
            fishAttraction = target.GetComponent<FishAttraction>();
            if (fishAttraction == null)
            {
                Debug.LogError("未在目标上找到FishAttraction组件。");
                return;
            }
        }
        else
        {
            Debug.LogError("未设置目标。");
        }
    }

    private void LateUpdate()
    {
        if (target == null || fishAttraction == null) return;

        // 当鱼被吸引时激活摄像机
        if (fishAttraction.isAttracted && !isCameraActivated)
        {
            ActivateCamera();
        }

        // 平滑跟随目标并面向目标
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }

    private void ActivateCamera()
    {
        targetCamera.enabled = true;
        isCameraActivated = true;
        Debug.Log("摄像机已激活。");
    }
}

