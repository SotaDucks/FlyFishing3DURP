using UnityEngine;
using UnityEngine.Splines;

public class FishAttraction : MonoBehaviour
{
    private Transform flyhook; // flyhook 的 Transform 引用
    public Transform exit1; // Exit1 的 Transform 引用
    public Transform exit2; // Exit2 的 Transform 引用
    public float attractionProbability = 0.3f; // 鱼被吸引的概率
    public float moveSpeed = 2f; // 鱼向flyhook移动的速度
    public float returnMoveSpeed = 3f; // 鱼退出吸引后的返回移动速度
    public float stopDistance = 0.5f; // 鱼停止跟随flyhook的最小距离
    public float attractionDuration = 5f; // 鱼被吸引的持续时间

    private SplineAnimate splineAnimate; // 参考SplineAnimate组件
    private bool isAttracted = false; // 标记鱼是否被吸引
    private bool isReturning = false; // 标记鱼是否在返回路径
    private float attractionTimer = 0f; // 记录吸引的时间
    private Transform currentTarget; // 当前移动的目标位置

    void Start()
    {
        // 自动查找名为 "flyhook" 的 GameObject 并获取其 Transform
        GameObject flyhookObject = GameObject.Find("flyhook");
        if (flyhookObject != null)
        {
            flyhook = flyhookObject.transform;
        }
        else
        {
            Debug.LogError("未找到名为 'flyhook' 的 GameObject！");
        }

        // 获取SplineAnimate组件
        splineAnimate = GetComponent<SplineAnimate>();
    }

    void Update()
    {
        // 如果鱼已经被吸引，跟随flyhook
        if (isAttracted && flyhook != null)
        {
            attractionTimer += Time.deltaTime;

            // 当鱼被吸引的时间到达attractionDuration时，进入返回状态
            if (attractionTimer >= attractionDuration)
            {
                ExitAttraction();
            }
            else
            {
                FollowFlyhook();
            }
        }

        // 如果鱼在返回路径上，依次移动到Exit1和Exit2
        if (isReturning)
        {
            MoveTowardsTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 判断触发的对象是否为flyhook
        if (other.transform == flyhook)
        {
            // 进行随机概率判定是否被吸引
            if (Random.value < attractionProbability)
            {
                // 停止SplineAnimate巡游
                splineAnimate.enabled = false;
                isAttracted = true;
                attractionTimer = 0f; // 重置吸引计时器
            }
        }
    }

    private void FollowFlyhook()
    {
        // 获取鱼与flyhook的距离
        float distanceToFlyhook = Vector3.Distance(transform.position, flyhook.position);

        // 如果距离大于最小停止距离，移动鱼
        if (distanceToFlyhook > stopDistance)
        {
            // 更新鱼的朝向，面向flyhook
            Vector3 direction = (flyhook.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);

            // 移动鱼向flyhook靠近
            transform.position = Vector3.MoveTowards(transform.position, flyhook.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // 停止移动但保持鱼面向flyhook
            Vector3 direction = (flyhook.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
        }
    }

    private void ExitAttraction()
    {
        isAttracted = false; // 取消吸引状态
        isReturning = true; // 开始返回状态
        currentTarget = exit1; // 首先移动到Exit1
    }

    private void MoveTowardsTarget()
    {
        if (currentTarget == null) return;

        // 获取当前目标点的距离
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

        // 如果距离目标点大于最小距离，移动鱼
        if (distanceToTarget > stopDistance)
        {
            // 更新鱼的朝向，面向当前目标
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * returnMoveSpeed);

            // 移动鱼向目标点靠近
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, returnMoveSpeed * Time.deltaTime);
        }
        else
        {
            // 如果鱼到达了当前目标点，检查下一个目标
            if (currentTarget == exit1)
            {
                currentTarget = exit2; // 移动到Exit2
            }
            else if (currentTarget == exit2)
            {
                // 到达Exit2后，销毁FishLocation
                Destroy(transform.parent.gameObject); // 销毁父物体Trout1SpawnPrefab
            }
        }
    }
}
