using UnityEngine;
using System.Collections;

public class TroutController : MonoBehaviour
{
    // Ѳ�ߵ������
    public Transform patrolPoint1;
    public Transform patrolPoint2;
    public Transform patrolPoint3;

    // �ƶ�����ת�ٶ�
    public float moveSpeed = 2f;
    public float rotateSpeed = 2f;

    // Ѳ�ߵ������͵�ǰ����
    private Transform[] patrolPoints;
    private int currentIndex = 0;

    void Start()
    {
        // ��ʼ��Ѳ�ߵ�����
        patrolPoints = new Transform[] { patrolPoint1, patrolPoint2, patrolPoint3 };

        // ��ʼ�����λ�úͳ���
        transform.position = patrolPoints[0].position;

        // ������һ��Ŀ��Ѳ�ߵ������
        currentIndex = 1;

        // ����Ѳ��Э��
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            // ��ȡ��ǰĿ��Ѳ�ߵ�
            Transform targetPoint = patrolPoints[currentIndex];

            // ��ת����Ŀ��Ѳ�ߵ�
            yield return StartCoroutine(FaceTowards(targetPoint));

            // �ƶ���Ŀ��Ѳ�ߵ�
            yield return StartCoroutine(MoveToPoint(targetPoint));

            // ������һ��Ŀ��Ѳ�ߵ������
            currentIndex = (currentIndex + 1) % patrolPoints.Length;
        }
    }

    IEnumerator MoveToPoint(Transform targetPoint)
    {
        Vector3 targetPosition = targetPoint.position;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            // ƽ���ƶ���Ŀ���
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // ȷ��λ�þ�ȷ
        transform.position = targetPosition;
    }

    IEnumerator FaceTowards(Transform target)
    {
        Vector3 direction = target.position - transform.position;

        if (direction == Vector3.zero)
            yield break;

        // ����Ŀ����ת
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // ������תֱ���ﵽĿ����ת
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            // ƽ����ת��Ŀ�귽��
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * 100 * Time.deltaTime);
            yield return null;
        }

        // ȷ����ת��ȷ
        transform.rotation = targetRotation;
    }
}
