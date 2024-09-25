using UnityEngine;
using Obi;

public class FishDragLine : MonoBehaviour
{
    public float dragSpeed = 1f; // �϶�ʱ���������ٶ�
    public float retrieveSpeed = 1f; // �ջ�ʱ���������ٶ�
    public float struggleSpeed = 1f; // ����ʱ���������ٶ�
    public float pullSpeed = 1f; // ����ʱ���������ٶ�

    private ObiRope rope; // ���� ObiRope ���
    private ObiRopeCursor ropeCursor; // ���� ObiRopeCursor ���
    private bool isDragging = false; // ����϶�״̬
    private bool isRetrieving = false; // ����ջ�״̬
    private bool isStruggling = false; // �������״̬
    private bool isPulling = false; // �������״̬
    private Animator characterAnimator; // ���ý�ɫ����

    void Start()
    {
        // ��ȡ ObiRope ���
        rope = GetComponent<ObiRope>();
        // ��ȡ ObiRopeCursor ���
        ropeCursor = GetComponent<ObiRopeCursor>();

        if (rope == null || ropeCursor == null)
        {
            Debug.LogError("δ�ҵ� ObiRope �� ObiRopeCursor �������ȷ�ϸýű������� FlyLine �ϣ�");
        }
    }

    void Update()
    {
        // ���ݲ�ͬ״̬���ӳ�����������
        if (isDragging && rope != null && ropeCursor != null)
        {
            ExtendRope(dragSpeed); // �϶�����
        }
        if (isRetrieving && rope != null && ropeCursor != null)
        {
            ExtendRope(-retrieveSpeed); // �ջ�����
        }
        if (isStruggling && rope != null && ropeCursor != null)
        {
            ExtendRope(struggleSpeed); // ����ʱ��������
        }
        if (isPulling && rope != null && ropeCursor != null)
        {
            ExtendRope(-pullSpeed); // ��������
        }

        // ��顰SetTheHook�������Ƿ����ڲ��ţ�������ڲ�����ֹͣ���в���
        if (characterAnimator != null && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("SetTheHook"))
        {
            StopAllActions(); // ֹͣ���ж���
        }
    }

    // �϶�����
    public void StartDragging()
    {
        isDragging = true;
    }

    public void StopDragging()
    {
        isDragging = false;
    }

    // �ջ�����
    public void StartRetrieving()
    {
        isRetrieving = true;
    }

    public void StopRetrieving()
    {
        isRetrieving = false;
    }

    // ����
    public void StartStruggling()
    {
        isStruggling = true;
    }

    public void StopStruggling()
    {
        isStruggling = false;
    }

    // ��������
    public void StartPulling()
    {
        isPulling = true;
    }

    public void StopPulling()
    {
        isPulling = false;
    }

    // ֹͣ�������ӵĲ���
    public void StopAllActions()
    {
        isDragging = false;
        isRetrieving = false;
        isStruggling = false;
        isPulling = false;
    }

    // �����ӳ������̵��߼�
    private void ExtendRope(float speed)
    {
        ropeCursor.ChangeLength(speed * Time.deltaTime); // ʹ�� ObiRopeCursor ���ı����ӵĳ���
        Debug.Log("����״̬�仯����ǰ���ȱ仯: " + speed * Time.deltaTime);
    }
}
