using UnityEngine;
using Obi;

public class FishDragLine : MonoBehaviour
{
    public float extendSpeed = 1f; // ÿ�����ӱ䳤���ٶ�
    private ObiRope rope; // ���� ObiRope ���
    private ObiRopeCursor ropeCursor; // ���� ObiRopeCursor ���
    private bool isExtending = false; // ��������Ƿ��ڱ䳤

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
        // ������Ӵ��ڱ䳤״̬������ָ���ٶ��������ӵĳ���
        if (isExtending && rope != null && ropeCursor != null)
        {
            ExtendRope();
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
