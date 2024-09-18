using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class LineLengthController : MonoBehaviour
{
    // Obi Rope 组件
    private ObiRopeCursor ropeCursor;
    private ObiRope rope;

    // 控制绳子的长度和增长参数
    public float maxLength = 10f; // 最大长度
    public float growthAmount = 1f; // 每次增长的长度
    public float growthSpeed = 1f; // 增长速度

    // 内部状态
    private bool isGrowing = false; // 当前是否正在增长
    private float targetLength; // 目标长度

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
        // 监听 D 键
        if (Input.GetKeyDown(KeyCode.D) && !isGrowing)
        {
            // 计算新的目标长度
            targetLength = Mathf.Min(rope.restLength + growthAmount, maxLength);
            // 只有当目标长度大于当前长度时才进行增长
            if (targetLength > rope.restLength)
            {
                isGrowing = true;
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
}
