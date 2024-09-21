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

    // 新增回收相关的公共变量
    public float RetrieveSpeed = 1f; // 回收速度
    public float RetrieveAmount = 1f; // 每次回收的长度
    public float MinLength = 2f; // 鱼线的最小长度

    // 内部状态
    private bool isGrowing = false; // 当前是否正在增长
    private bool isRetrieving = false; // 当前是否正在回收
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
        // 监听 D 键用于增长鱼线
        if (Input.GetKeyDown(KeyCode.D) && !isGrowing && !isRetrieving)
        {
            // 计算新的目标长度
            targetLength = Mathf.Min(rope.restLength + growthAmount, maxLength);
            // 只有当目标长度大于当前长度时才进行增长
            if (targetLength > rope.restLength)
            {
                isGrowing = true;
            }
        }

        // 监听 S 键用于回收鱼线
        if (Input.GetKeyDown(KeyCode.S) && !isRetrieving && !isGrowing)
        {
            // 计算新的目标长度
            targetLength = Mathf.Max(rope.restLength - RetrieveAmount, MinLength);
            // 只有当目标长度小于当前长度时才进行回收
            if (targetLength < rope.restLength)
            {
                isRetrieving = true;
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

        // 控制绳子的回收
        if (isRetrieving)
        {
            if (rope.restLength > targetLength)
            {
                float changeAmount = RetrieveSpeed * Time.deltaTime;
                // 负值表示减少长度
                ropeCursor.ChangeLength(-Mathf.Min(changeAmount, rope.restLength - targetLength));
            }
            else
            {
                // 到达目标长度后停止回收
                isRetrieving = false;
                // 输出当前长度
                Debug.Log($"Rope Length after retrieval: {rope.restLength}");
            }
        }
    }
}
