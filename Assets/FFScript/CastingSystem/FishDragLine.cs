using UnityEngine;
using Obi;

public class FishDragLine : MonoBehaviour
{
    public float extendSpeed = 1f; // 每秒绳子变长的速度
    private ObiRope rope; // 引用 ObiRope 组件
    private ObiRopeCursor ropeCursor; // 引用 ObiRopeCursor 组件
    private bool isExtending = false; // 标记绳子是否在变长
    private Animator characterAnimator; // 引用 Character 的 Animators

    void Start()
    {
        // 获取 ObiRope 组件
        rope = GetComponent<ObiRope>();

        // 获取 ObiRopeCursor 组件
        ropeCursor = GetComponent<ObiRopeCursor>();

        if (rope == null || ropeCursor == null)
        {
            Debug.LogError("未找到 ObiRope 或 ObiRopeCursor 组件，请确认该脚本附加在 FlyLine 上！");
        }
    }

    void Update()
    {
        if (isExtending && rope != null && ropeCursor != null)
        {
            ExtendRope();
        }

        // 检查“SetTheHook”动画是否正在播放
        if (characterAnimator != null && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("SetTheHook"))
        {
            StopExtending(); // 停止延长绳子
        }
    }

    // 开始延长绳子
    public void StartExtending()
    {
        isExtending = true; // 激活绳子变长
    }

    // 停止延长绳子
    public void StopExtending()
    {
        isExtending = false; // 停止绳子变长
    }

    // 延长绳子的逻辑
    private void ExtendRope()
    {
        // 使用 ObiRopeCursor 来增加绳子的长度
        ropeCursor.ChangeLength(extendSpeed * Time.deltaTime);
        Debug.Log("绳子正在延长，当前长度增加: " + extendSpeed * Time.deltaTime);
    }
}
