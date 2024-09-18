using UnityEngine;

public class MouseAnimationController : MonoBehaviour
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

    // ����ƶ����
    public float moveThreshold = 0.2f; // ���������������С�ƶ�����
    private Vector3 initialMousePosition; // ��¼����ʼλ��

    // ��־����
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
        // �������������
        if (Input.GetMouseButtonDown(0))
        {
            isMouseHeld = true;
            initialMousePosition = Input.mousePosition; // ��¼����ʼλ��
        }

        // ����������ɿ�
        if (Input.GetMouseButtonUp(0))
        {
            isMouseHeld = false;
            ResetAnimationStates(); // ���ö���״̬
        }

        // ������������סʱ
        if (isMouseHeld)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float mouseMoveDistance = currentMousePosition.x - initialMousePosition.x; // �������ˮƽ�ƶ�����

            if (mouseMoveDistance < -moveThreshold && canPressA) // �����ƶ�
            {
                PlaySwingLeft();
            }
            else if (mouseMoveDistance > moveThreshold && canPressD) // �����ƶ�
            {
                PlaySwingRight();
            }
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

    void ResetAnimationStates()
    {
        animator.SetBool(isSwingLeftParam, false);
        animator.SetBool(isSwingRightParam, false);
        canPressA = true;
        canPressD = true;
    }
}
