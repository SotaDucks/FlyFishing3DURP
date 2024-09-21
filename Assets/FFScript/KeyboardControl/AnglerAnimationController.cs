using UnityEngine;

public class AnglerAnimationController : MonoBehaviour
{
    // Animator���
    private Animator animator;

    // ������������
    private readonly string PRESSING_SPACE = "PressingSpace";
    private readonly string SWING_LEFT = "SwingLeft";
    private readonly string SWING_RIGHT = "SwingRight";
    private readonly string RETRIEVE = "Retrieve";

    void Start()
    {
        // ��ȡAnimator���
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator���δ�ҵ���");
        }
    }

    void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// �����û����벢����Animator����
    /// </summary>
    private void HandleInput()
    {
        if (animator == null)
            return;

        // ����ո�����º��ͷ�
        bool isPressingSpace = Input.GetKey(KeyCode.Space);
        animator.SetBool(PRESSING_SPACE, isPressingSpace);

        // ����A������
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A�������£����� SwingLeft ����");
            animator.SetBool(SWING_LEFT, true);
            // ����Э�����ڶ��������ò���
            StartCoroutine(ResetSwingParameter(SWING_LEFT));
        }

        // ����D������
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D�������£����� SwingRight ����");
            animator.SetBool(SWING_RIGHT, true);
            // ����Э�����ڶ��������ò���
            StartCoroutine(ResetSwingParameter(SWING_RIGHT));
        }

        // ����S������
        if (Input.GetKeyDown(KeyCode.S))
        {
            // ����PressingSpaceΪfalseʱ������Retrieve
            if (!isPressingSpace)
            {
                Debug.Log("S�������£����� Retrieve ����");
                animator.SetTrigger(RETRIEVE);
                // ����Ҫ����Э������Trigger����ΪTrigger���Զ�����
            }
            else
            {
                Debug.Log("S�������£��� PressingSpace Ϊ true���޷����� Retrieve ����");
            }
        }
    }

    /// <summary>
    /// Э�̣���ָ��ʱ�������Swing����
    /// </summary>
    /// <param name="parameter">Ҫ���õĲ�������</param>
    /// <returns></returns>
    private System.Collections.IEnumerator ResetSwingParameter(string parameter)
    {
        // �ȴ�����������ɣ����ݶ������ȵ����ȴ�ʱ�䣩
        // �������ÿ��Swing��������0.5��
        yield return new WaitForSeconds(0.5f);
        animator.SetBool(parameter, false);
    }

    /// <summary>
    /// �����¼����õķ�������������Swing����
    /// ���ʹ�ö����¼�������ͨ�����ô˷��������ò�����������ҪЭ��
    /// </summary>
    /// <param name="parameter">Ҫ���õĲ�������</param>
    public void OnAnimationEnd(string parameter)
    {
        if (animator != null)
        {
            animator.SetBool(parameter, false);
            Debug.Log($"{parameter} ����������");
        }
    }
}
