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
    public float initialLength = 5f; // ��Ϸ��ʼʱ�����ӳ���

    public float maxLength = 10f; // ��󳤶�
    public float growthAmount = 1f; // ÿ�������ĳ���
    public float growthSpeed = 1f; // �����ٶ�

    // ������صĹ�������
    public float RetrieveSpeed = 1f; // �����ٶ�
    public float RetrieveAmount = 1f; // ÿ�λ��յĳ���
    public float MinLength = 2f; // ���ߵ���С����

    // ����������صĹ�������
    public float landingSpeed = 2f; // ������ٶ�
    public float landingAmount = 2f; // ÿ��������յĳ���

    // �ڲ�״̬
    private bool isGrowing = false; // ��ǰ�Ƿ���������
    private bool isRetrieving = false; // ��ǰ�Ƿ����ڻ���
    private bool isLanding = false; // ��ǰ�Ƿ���������
    private float targetLength; // Ŀ�곤��

    // �������
    public Animator animator; // ��ɫ��Animator���

    // ���ڼ�⶯��״̬�仯
    private bool wasLiftRodPlaying = false;

    // ����FishStaminaBar���
    public FishStaminaBar fishStaminaBar;

    void Start()
    {
        // ��ʼ���������
        ropeCursor = GetComponent<ObiRopeCursor>();
        rope = GetComponent<ObiRope>();
        ropeCursor.ChangeLength(initialLength);

        // �����ǰ����
        Debug.Log($"Initial Rope Length: {rope.restLength}");

        // ���û����Inspector��ָ��Animator�������Զ���ȡ
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator���δ�ҵ�������Inspector��ָ���򽫴˽ű����ӵ���Animator�Ķ����ϡ�");
            }
        }

        // ���û����Inspector��ָ��FishStaminaBar�������Զ���ȡ
        if (fishStaminaBar == null)
        {
            fishStaminaBar = FindObjectOfType<FishStaminaBar>();
            if (fishStaminaBar == null)
            {
                Debug.LogError("FishStaminaBar���δ�ҵ�������Inspector��ָ����ȷ�������д��ڸ������");
            }
        }
    }

    void Update()
    {
        // ���� D ��������������
        if (Input.GetKeyDown(KeyCode.D) && !isGrowing && !isRetrieving && !isLanding)
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
        if (Input.GetKeyDown(KeyCode.S) && !isRetrieving && !isGrowing && !isLanding)
        {
            // �����µ�Ŀ�곤��
            targetLength = Mathf.Max(rope.restLength - RetrieveAmount, MinLength);
            // ֻ�е�Ŀ�곤��С�ڵ�ǰ����ʱ�Ž��л���
            if (targetLength < rope.restLength)
            {
                isRetrieving = true;
            }
        }

        // ��� "LiftRod" �����Ĳ���
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            bool isLiftRodPlaying = stateInfo.IsName("LiftRod");

            // �� "LiftRod" ������ʼ����ʱ�������������ֵС�ڵ���0���������㶯��
            if (isLiftRodPlaying && !wasLiftRodPlaying && !isGrowing && !isRetrieving && !isLanding)
            {
                // ���FishStaminaBar��currentStamina�Ƿ�С�ڵ���0
                if (fishStaminaBar != null && fishStaminaBar.currentStamina <= 0)
                {
                    // �����µ�Ŀ�곤��
                    targetLength = Mathf.Max(rope.restLength - landingAmount, MinLength);
                    // ֻ�е�Ŀ�곤��С�ڵ�ǰ����ʱ�Ž�������
                    if (targetLength < rope.restLength)
                    {
                        isLanding = true;
                    }
                }
            }

            wasLiftRodPlaying = isLiftRodPlaying;
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

        // �������ӵ����㶯��
        if (isLanding)
        {
            if (rope.restLength > targetLength)
            {
                float changeAmount = landingSpeed * Time.deltaTime;
                // ��ֵ��ʾ���ٳ���
                ropeCursor.ChangeLength(-Mathf.Min(changeAmount, rope.restLength - targetLength));
            }
            else
            {
                // ����Ŀ�곤�Ⱥ�ֹͣ����
                isLanding = false;
                // �����ǰ����
                Debug.Log($"Rope Length after landing: {rope.restLength}");
            }
        }
    }
}
