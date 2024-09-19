using UnityEngine;
using System.Collections;

public class TroutController : MonoBehaviour
{
    // 巡逻点的引用
    public Transform patrolPoint1;
    public Transform patrolPoint2;
    public Transform patrolPoint3;

    // 移动和旋转速度
    public float moveSpeed = 2f;
    public float rotateSpeed = 2f;

    // 巡逻点的数组和当前索引
    private Transform[] patrolPoints;
    private int currentIndex = 0;

    void Start()
    {
        // 初始化巡逻点数组
        patrolPoints = new Transform[] { patrolPoint1, patrolPoint2, patrolPoint3 };

        // 初始化鱼的位置和朝向
        transform.position = patrolPoints[0].position;

        // 设置下一个目标巡逻点的索引
        currentIndex = 1;

        // 启动巡逻协程
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            // 获取当前目标巡逻点
            Transform targetPoint = patrolPoints[currentIndex];

            // 旋转朝向目标巡逻点
            yield return StartCoroutine(FaceTowards(targetPoint));

            // 移动到目标巡逻点
            yield return StartCoroutine(MoveToPoint(targetPoint));

            // 更新下一个目标巡逻点的索引
            currentIndex = (currentIndex + 1) % patrolPoints.Length;
        }
    }

    IEnumerator MoveToPoint(Transform targetPoint)
    {
        Vector3 targetPosition = targetPoint.position;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            // 平滑移动到目标点
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // 确保位置精确
        transform.position = targetPosition;
    }

    IEnumerator FaceTowards(Transform target)
    {
        Vector3 direction = target.position - transform.position;

        if (direction == Vector3.zero)
            yield break;

        // 计算目标旋转
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 持续旋转直到达到目标旋转
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            // 平滑旋转到目标方向
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * 100 * Time.deltaTime);
            yield return null;
        }

        // 确保旋转精确
        transform.rotation = targetRotation;
    }
}
