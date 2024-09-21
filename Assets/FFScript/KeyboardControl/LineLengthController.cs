using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class LineLengthController : MonoBehaviour
{
    // Obi Rope ���
    private ObiRopeCursor ropeCursor;
    private ObiRope rope;

    // �������ӵĳ��Ⱥ���������
    public float maxLength = 10f; // ��󳤶�
    public float growthAmount = 1f; // ÿ�������ĳ���
    public float growthSpeed = 1f; // �����ٶ�

    // ����������صĹ�������
    public float RetrieveSpeed = 1f; // �����ٶ�
    public float RetrieveAmount = 1f; // ÿ�λ��յĳ���
    public float MinLength = 2f; // ���ߵ���С����

    // �ڲ�״̬
    private bool isGrowing = false; // ��ǰ�Ƿ���������
    private bool isRetrieving = false; // ��ǰ�Ƿ����ڻ���
    private float targetLength; // Ŀ�곤��

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
        // ���� D ��������������
        if (Input.GetKeyDown(KeyCode.D) && !isGrowing && !isRetrieving)
        {
            // �����µ�Ŀ�곤��
            targetLength = Mathf.Min(rope.restLength + growthAmount, maxLength);
            // ֻ�е�Ŀ�곤�ȴ��ڵ�ǰ����ʱ�Ž�������
            if (targetLength > rope.restLength)
            {
                isGrowing = true;
            }
        }

        // ���� S �����ڻ�������
        if (Input.GetKeyDown(KeyCode.S) && !isRetrieving && !isGrowing)
        {
            // �����µ�Ŀ�곤��
            targetLength = Mathf.Max(rope.restLength - RetrieveAmount, MinLength);
            // ֻ�е�Ŀ�곤��С�ڵ�ǰ����ʱ�Ž��л���
            if (targetLength < rope.restLength)
            {
                isRetrieving = true;
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

        // �������ӵĻ���
        if (isRetrieving)
        {
            if (rope.restLength > targetLength)
            {
                float changeAmount = RetrieveSpeed * Time.deltaTime;
                // ��ֵ��ʾ���ٳ���
                ropeCursor.ChangeLength(-Mathf.Min(changeAmount, rope.restLength - targetLength));
            }
            else
            {
                // ����Ŀ�곤�Ⱥ�ֹͣ����
                isRetrieving = false;
                // �����ǰ����
                Debug.Log($"Rope Length after retrieval: {rope.restLength}");
            }
        }
    }
}
