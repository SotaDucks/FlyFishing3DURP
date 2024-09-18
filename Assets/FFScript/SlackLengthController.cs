using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class SlackLengthController : MonoBehaviour
{
    // Obi Rope ���
    ObiRopeCursor cursor;
    ObiRope rope;

    // �����ٶȺ����ӵĳ���״̬
    public float speed = 1f;
    public float smallSlack = 2f;
    public float largeSlack = 5f;

    // ���� Character �ϵ� Animator ���
    public GameObject character; // Character GameObject
    private Animator animator; // Character �ϵ� Animator

    // ����״̬����
    private string swingLeftAnimationStateName = "SwingLeft";
    private string swingRightAnimationStateName = "SwingRight";

    void Start()
    {
        cursor = GetComponentInChildren<ObiRopeCursor>();
        rope = cursor.GetComponent<ObiRope>();

        // ���ó�ʼ����Ϊ largeSlack
        cursor.ChangeLength(largeSlack - rope.restLength);

        // ��ȡ Character GameObject �ϵ� Animator ���
        if (character != null)
        {
            animator = character.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Character GameObject is not assigned.");
        }
    }

    void Update()
    {
        // ȷ�� Animator ����ȷ��ȡ
        if (animator == null) return;

        // ��ȡ��ǰ����״̬��Ϣ
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �������ӵ� smallSlack������ "SwingRight" ����ʱ��
        if (stateInfo.IsName(swingRightAnimationStateName))
        {
            if (rope.restLength > smallSlack)
            {
                float changeAmount = -speed * Time.deltaTime;
                // ��һ���ٶ����̵� smallSlack
                cursor.ChangeLength(Mathf.Max(changeAmount, smallSlack - rope.restLength));
            }
        }

        // �ӳ����ӵ� largeSlack������ "SwingLeft" ����ʱ��
        else if (stateInfo.IsName(swingLeftAnimationStateName))
        {
            if (rope.restLength < largeSlack)
            {
                float changeAmount = speed * Time.deltaTime;
                // ��һ���ٶ��ӳ��� largeSlack
                cursor.ChangeLength(Mathf.Min(changeAmount, largeSlack - rope.restLength));
            }
        }
    }
}
