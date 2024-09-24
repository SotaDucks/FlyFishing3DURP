using UnityEngine;
using Obi;

public class FishDragLine : MonoBehaviour
{
    public float extendSpeed = 1f; // ÿ�����ӱ䳤���ٶ�
    private ObiRope rope; // ���� ObiRope ���
    private ObiRopeCursor ropeCursor; // ���� ObiRopeCursor ���
    private bool isExtending = false; // ��������Ƿ��ڱ䳤
    private Animator characterAnimator; // ���� Character �� Animators

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
        if (isExtending && rope != null && ropeCursor != null)
        {
            ExtendRope();
        }

        // ��顰SetTheHook�������Ƿ����ڲ���
        if (characterAnimator != null && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("SetTheHook"))
        {
            StopExtending(); // ֹͣ�ӳ�����
        }
    }

    // ��ʼ�ӳ�����
    public void StartExtending()
    {
        isExtending = true; // �������ӱ䳤
    }

    // ֹͣ�ӳ�����
    public void StopExtending()
    {
        isExtending = false; // ֹͣ���ӱ䳤
    }

    // �ӳ����ӵ��߼�
    private void ExtendRope()
    {
        // ʹ�� ObiRopeCursor ���������ӵĳ���
        ropeCursor.ChangeLength(extendSpeed * Time.deltaTime);
        Debug.Log("���������ӳ�����ǰ��������: " + extendSpeed * Time.deltaTime);
    }
}
