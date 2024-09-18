using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class MouseLengthController : MonoBehaviour
{
    // Obi Rope 组件
    private ObiRopeCursor ropeCursor;
    private ObiRope rope;

    // 控制绳子的长度和增长参数
    public float maxLength = 10f; // 最大长度
    public float growthAmount = 1f; // 每次增长的长度
    public float growthSpeed = 1f; // 增长速度

    // 鼠标移动检测
    public float moveThreshold = 0.2f; // 触发伸长绳子所需的最小移动距离
    private Vector3 initialMousePosition; // 记录鼠标初始位置

    // 内部状态
    private bool isMouseHeld = false;
    private bool isGrowing = false; // 当前是否正在增长
    private bool hasTriggeredGrowth = false; // 当前是否已经触发了增长事件
    private float targetLength; // 本次增长的目标长度

    void Start()
    {
        // 初始化绳子组件
        ropeCursor = GetComponent<ObiRopeCursor>();
        rope = GetComponent<ObiRope>();

        // 输出当前长度
        Debug.Log($"Initial Rope Length: {rope.restLength}");
    }

    void Update()
    {
        // 检测鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            isMouseHeld = true;
            initialMousePosition = Input.mousePosition; // 记录鼠标初始位置
            hasTriggeredGrowth = false; // 重置增长事件触发状态
        }

        // 检测鼠标左键松开
        if (Input.GetMouseButtonUp(0))
        {
            isMouseHeld = false;
        }

        // 当鼠标左键被按住时
        if (isMouseHeld && !hasTriggeredGrowth)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float mouseMoveDistance = currentMousePosition.x - initialMousePosition.x; // 计算鼠标水平移动距离

            if (mouseMoveDistance > moveThreshold) // 向右移动超过阈值
            {
                StartGrowingRope(); // 触发一次性增长
                hasTriggeredGrowth = true; // 标记已触发增长事件
            }
        }

        // 控制绳子的增长
        if (isGrowing)
        {
            if (rope.restLength < targetLength)
            {
                float changeAmount = growthSpeed * Time.deltaTime;
                ropeCursor.ChangeLength(Mathf.Min(changeAmount, targetLength - rope.restLength));
            }
            else
            {
                // 到达目标长度后停止增长
                isGrowing = false;
                // 输出当前长度
                Debug.Log($"Rope Length after growth: {rope.restLength}");
            }
        }
    }

    void StartGrowingRope()
    {
        // 计算新的目标长度
        targetLength = Mathf.Min(rope.restLength + growthAmount, maxLength);

        // 只有当目标长度大于当前长度时才进行增长
        if (targetLength > rope.restLength)
        {
            isGrowing = true;
        }
    }
}
