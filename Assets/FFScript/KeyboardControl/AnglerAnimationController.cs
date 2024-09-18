using UnityEngine;

public class AnglerAnimationController : MonoBehaviour
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

    // 标志变量，控制按键输入
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
        // 按键输入检测
        if (Input.GetKeyDown(KeyCode.A) && canPressA)
        {
            PlaySwingLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) && canPressD)
        {
            PlaySwingRight();
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
}
