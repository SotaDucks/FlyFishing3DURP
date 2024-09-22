using UnityEngine;
using UnityEngine.Splines;

public class FishAttraction : MonoBehaviour
{
    private Transform flyhook; // flyhook �� Transform ����
    public Transform exit1; // Exit1 �� Transform ����
    public Transform exit2; // Exit2 �� Transform ����
    public float attractionProbability = 0.3f; // �㱻�����ĸ���
    public float moveSpeed = 2f; // ����flyhook�ƶ����ٶ�
    public float returnMoveSpeed = 3f; // ���˳�������ķ����ƶ��ٶ�
    public float stopDistance = 0.5f; // ��ֹͣ����flyhook����С����
    public float attractionDuration = 5f; // �㱻�����ĳ���ʱ��

    private SplineAnimate splineAnimate; // �ο�SplineAnimate���
    private bool isAttracted = false; // ������Ƿ�����
    private bool isReturning = false; // ������Ƿ��ڷ���·��
    private float attractionTimer = 0f; // ��¼������ʱ��
    private Transform currentTarget; // ��ǰ�ƶ���Ŀ��λ��

    void Start()
    {
        // �Զ�������Ϊ "flyhook" �� GameObject ����ȡ�� Transform
        GameObject flyhookObject = GameObject.Find("flyhook");
        if (flyhookObject != null)
        {
            flyhook = flyhookObject.transform;
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'flyhook' �� GameObject��");
        }

        // ��ȡSplineAnimate���
        splineAnimate = GetComponent<SplineAnimate>();
    }

    void Update()
    {
        // ������Ѿ�������������flyhook
        if (isAttracted && flyhook != null)
        {
            attractionTimer += Time.deltaTime;

            // ���㱻������ʱ�䵽��attractionDurationʱ�����뷵��״̬
            if (attractionTimer >= attractionDuration)
            {
                ExitAttraction();
            }
            else
            {
                FollowFlyhook();
            }
        }

        // ������ڷ���·���ϣ������ƶ���Exit1��Exit2
        if (isReturning)
        {
            MoveTowardsTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �жϴ����Ķ����Ƿ�Ϊflyhook
        if (other.transform == flyhook)
        {
            // ������������ж��Ƿ�����
            if (Random.value < attractionProbability)
            {
                // ֹͣSplineAnimateѲ��
                splineAnimate.enabled = false;
                isAttracted = true;
                attractionTimer = 0f; // ����������ʱ��
            }
        }
    }

    private void FollowFlyhook()
    {
        // ��ȡ����flyhook�ľ���
        float distanceToFlyhook = Vector3.Distance(transform.position, flyhook.position);

        // ������������Сֹͣ���룬�ƶ���
        if (distanceToFlyhook > stopDistance)
        {
            // ������ĳ�������flyhook
            Vector3 direction = (flyhook.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);

            // �ƶ�����flyhook����
            transform.position = Vector3.MoveTowards(transform.position, flyhook.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // ֹͣ�ƶ�������������flyhook
            Vector3 direction = (flyhook.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
        }
    }

    private void ExitAttraction()
    {
        isAttracted = false; // ȡ������״̬
        isReturning = true; // ��ʼ����״̬
        currentTarget = exit1; // �����ƶ���Exit1
    }

    private void MoveTowardsTarget()
    {
        if (currentTarget == null) return;

        // ��ȡ��ǰĿ���ľ���
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

        // �������Ŀ��������С���룬�ƶ���
        if (distanceToTarget > stopDistance)
        {
            // ������ĳ�������ǰĿ��
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * returnMoveSpeed);

            // �ƶ�����Ŀ��㿿��
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, returnMoveSpeed * Time.deltaTime);
        }
        else
        {
            // ����㵽���˵�ǰĿ��㣬�����һ��Ŀ��
            if (currentTarget == exit1)
            {
                currentTarget = exit2; // �ƶ���Exit2
            }
            else if (currentTarget == exit2)
            {
                // ����Exit2������FishLocation
                Destroy(transform.parent.gameObject); // ���ٸ�����Trout1SpawnPrefab
            }
        }
    }
}
