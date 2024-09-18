using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class MouseLengthController : MonoBehaviour
{
    // Obi Rope ���
    private ObiRopeCursor ropeCursor;
    private ObiRope rope;

    // �������ӵĳ��Ⱥ���������
    public float maxLength = 10f; // ��󳤶�
    public float growthAmount = 1f; // ÿ�������ĳ���
    public float growthSpeed = 1f; // �����ٶ�

    // ����ƶ����
    public float moveThreshold = 0.2f; // �����쳤�����������С�ƶ�����
    private Vector3 initialMousePosition; // ��¼����ʼλ��

    // �ڲ�״̬
    private bool isMouseHeld = false;
    private bool isGrowing = false; // ��ǰ�Ƿ���������
    private bool hasTriggeredGrowth = false; // ��ǰ�Ƿ��Ѿ������������¼�
    private float targetLength; // ����������Ŀ�곤��

    void Start()
    {
        // ��ʼ���������
        ropeCursor = GetComponent<ObiRopeCursor>();
        rope = GetComponent<ObiRope>();

        // �����ǰ����
        Debug.Log($"Initial Rope Length: {rope.restLength}");
    }

    void Update()
    {
        // �������������
        if (Input.GetMouseButtonDown(0))
        {
            isMouseHeld = true;
            initialMousePosition = Input.mousePosition; // ��¼����ʼλ��
            hasTriggeredGrowth = false; // ���������¼�����״̬
        }

        // ����������ɿ�
        if (Input.GetMouseButtonUp(0))
        {
            isMouseHeld = false;
        }

        // ������������סʱ
        if (isMouseHeld && !hasTriggeredGrowth)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float mouseMoveDistance = currentMousePosition.x - initialMousePosition.x; // �������ˮƽ�ƶ�����

            if (mouseMoveDistance > moveThreshold) // �����ƶ�������ֵ
            {
                StartGrowingRope(); // ����һ��������
                hasTriggeredGrowth = true; // ����Ѵ��������¼�
            }
        }

        // �������ӵ�����
        if (isGrowing)
        {
            if (rope.restLength < targetLength)
            {
                float changeAmount = growthSpeed * Time.deltaTime;
                ropeCursor.ChangeLength(Mathf.Min(changeAmount, targetLength - rope.restLength));
            }
            else
            {
                // ����Ŀ�곤�Ⱥ�ֹͣ����
                isGrowing = false;
                // �����ǰ����
                Debug.Log($"Rope Length after growth: {rope.restLength}");
            }
        }
    }

    void StartGrowingRope()
    {
        // �����µ�Ŀ�곤��
        targetLength = Mathf.Min(rope.restLength + growthAmount, maxLength);

        // ֻ�е�Ŀ�곤�ȴ��ڵ�ǰ����ʱ�Ž�������
        if (targetLength > rope.restLength)
        {
            isGrowing = true;
        }
    }
}
