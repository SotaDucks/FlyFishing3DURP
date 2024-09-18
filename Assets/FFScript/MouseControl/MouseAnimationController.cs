using UnityEngine;

public class MouseAnimationController : MonoBehaviour
{
    private Animator animator;

    // 动画状态名称
    private string swingLeftAnimationStateName = "SwingLeft";
    private string swingRightAnimationStateName = "SwingRight";

    // 控制动画切换的参数名称
    private string isSwingLeftParam = "IsSwingLeft";
    private string isSwingRightParam = "IsSwingRight";

    // Switch Delay Time，以动画的标准化时间表示
    public float switchDelayNormalizedTime = 0.5f;

    // 鼠标移动检测
    public float moveThreshold = 0.2f; // 触发动画所需的最小移动距离
    private Vector3 initialMousePosition; // 记录鼠标初始位置

    // 标志变量
    private bool isMouseHeld = false;
    private bool canPressA = true;
    private bool canPressD = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(isSwingLeftParam, false);
        animator.SetBool(isSwingRightParam, false);
    }

    void Update()
    {
        // 检测鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            isMouseHeld = true;
            initialMousePosition = Input.mousePosition; // 记录鼠标初始位置
        }

        // 检测鼠标左键松开
        if (Input.GetMouseButtonUp(0))
        {
            isMouseHeld = false;
            ResetAnimationStates(); // 重置动画状态
        }

        // 当鼠标左键被按住时
        if (isMouseHeld)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float mouseMoveDistance = currentMousePosition.x - initialMousePosition.x; // 计算鼠标水平移动距离

            if (mouseMoveDistance < -moveThreshold && canPressA) // 向左移动
            {
                PlaySwingLeft();
            }
            else if (mouseMoveDistance > moveThreshold && canPressD) // 向右移动
            {
                PlaySwingRight();
            }
        }

        // 获取当前动画状态信息
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 检测SwingLeft动画状态
        if (stateInfo.IsName(swingLeftAnimationStateName))
        {
            if (stateInfo.normalizedTime >= switchDelayNormalizedTime)
            {
                canPressD = true;
            }
        }
        else if (stateInfo.IsName(swingRightAnimationStateName))
        {
            if (stateInfo.normalizedTime >= switchDelayNormalizedTime)
            {
                canPressA = true;
            }
        }
    }

    void PlaySwingLeft()
    {
        animator.SetBool(isSwingLeftParam, true);
        animator.SetBool(isSwingRightParam, false); // 立即重置另一参数
        canPressA = false;
        canPressD = false;
    }

    void PlaySwingRight()
    {
        animator.SetBool(isSwingRightParam, true);
        animator.SetBool(isSwingLeftParam, false); // 立即重置另一参数
        canPressA = false;
        canPressD = false;
    }

    void ResetAnimationStates()
    {
        animator.SetBool(isSwingLeftParam, false);
        animator.SetBool(isSwingRightParam, false);
        canPressA = true;
        canPressD = true;
    }
}
