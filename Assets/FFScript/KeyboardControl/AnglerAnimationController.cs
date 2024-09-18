using UnityEngine;

public class AnglerAnimationController : MonoBehaviour
{
    private Animator animator;

    // ����״̬����
    private string swingLeftAnimationStateName = "SwingLeft";
    private string swingRightAnimationStateName = "SwingRight";

    // ���ƶ����л��Ĳ�������
    private string isSwingLeftParam = "IsSwingLeft";
    private string isSwingRightParam = "IsSwingRight";

    // Switch Delay Time���Զ����ı�׼��ʱ���ʾ
    public float switchDelayNormalizedTime = 0.5f;

    // ��־���������ư�������
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
        // ����������
        if (Input.GetKeyDown(KeyCode.A) && canPressA)
        {
            PlaySwingLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) && canPressD)
        {
            PlaySwingRight();
        }

        // ��ȡ��ǰ����״̬��Ϣ
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // ���SwingLeft����״̬
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
        animator.SetBool(isSwingRightParam, false); // ����������һ����
        canPressA = false;
        canPressD = false;
    }

    void PlaySwingRight()
    {
        animator.SetBool(isSwingRightParam, true);
        animator.SetBool(isSwingLeftParam, false); // ����������һ����
        canPressA = false;
        canPressD = false;
    }
}
