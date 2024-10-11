using UnityEngine;

public class TeachCamRightMove : MonoBehaviour
{
    public Animator characterAnimator; // ��ɫ��Animator
    public string animationName = "CastIntoWater"; // ��Ҫ���Ķ�������
    public float moveSpeed = 2f; // ������ƶ����ٶ�
    public float moveDuration = 3f; // ������ƶ��ĳ���ʱ�䣨�룩

    public GameObject uiElement1; // ��һ��Ҫ�����UIԪ��
    public GameObject uiElement2; // �ڶ���Ҫ�����UIԪ��

    private bool isMoving = false;
    private bool hasMoved = false; // ��־λ����ʾ������Ƿ��Ѿ�����ƶ�
    private float moveTimer = 0f;

    void Update()
    {
        // ����ɫAnimator�Ƿ��ڲ���ָ���Ķ����������������δ�ƶ���
        if (characterAnimator != null && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && !hasMoved)
        {
            if (!isMoving)
            {
                // ����������ڲ����������δ�ƶ�����ʼ�ƶ������
                isMoving = true;
                moveTimer = moveDuration;
            }
        }

        // �������������ƶ�
        if (isMoving)
        {
            // ������������ƶ�
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            // �ݼ��ƶ���ʱ��
            moveTimer -= Time.deltaTime;

            // ����ƶ���ʱ��С�ڵ���0��ֹͣ�ƶ�
            if (moveTimer <= 0)
            {
                isMoving = false;
                hasMoved = true; // ���������Ѿ�����ƶ�

                // ����ָ����UIԪ��
                if (uiElement1 != null)
                {
                    uiElement1.SetActive(true);
                }
                if (uiElement2 != null)
                {
                    uiElement2.SetActive(true);
                }
            }
        }
    }
}
