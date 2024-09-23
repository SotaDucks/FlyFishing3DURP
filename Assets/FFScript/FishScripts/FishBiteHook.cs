using UnityEngine;
using UnityEngine.Splines;
using Obi;

public class FishBiteHook : MonoBehaviour
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
    private Animator animator; // Animator 引用
    private FishDragLine FishDragLine; // 用于存储 FlyLineExtend 组件的引用
    private Rigidbody fishRigidbody; // 用于存储自身的 Rigidbody

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

        // 获取Animator组件
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("未找到 Animator 组件！");
        }

        // 查找场景中的 FlyLine 对象并获取 FlyLineExtend 组件
        GameObject flyLineObject = GameObject.Find("FlyLine");
        if (flyLineObject != null)
        {
            FishDragLine = flyLineObject.GetComponent<FishDragLine>();
            if (FishDragLine == null)
            {
                Debug.LogError("未找到 FlyLineExtend 组件！");
            }
        }
        else
        {
            Debug.LogError("未找到名为 'FlyLine' 的对象！");
        }


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

            // 鱼已经接近flyhook，将其绑定到鱼线上
            AttachFishToFlyline();
        }
    }

    private void AttachFishToFlyline()
    {
        // 自动查找名为 "FlyLine" 的对象
        GameObject flyLine = GameObject.Find("FlyLine");

        if (flyLine != null)
        {
            // 获取 FlyLine 对象上的所有 ObiParticleAttachment 组件
            ObiParticleAttachment[] attachments = flyLine.GetComponents<ObiParticleAttachment>();

            if (attachments.Length >= 3) // 确保有至少三个 Obi Particle Attachment
            {
                // 给第三个 Obi Particle Attachment 设置目标为鱼自身
                attachments[2].target = this.transform; // 将鱼的 Transform 作为目标
                Debug.Log("鱼已经被绑定到 FlyLine 的第三个 Obi Particle Attachment！");

                // 查找 "flyhook" 对象并将其设为未激活状态
                GameObject flyhook = GameObject.Find("flyhook");
                if (flyhook != null)
                {
                    flyhook.SetActive(false); // 设置 flyhook 为未激活状态
                    Debug.Log("'flyhook' 已被设为未激活！");
                }
                else
                {
                    Debug.LogError("未找到名为 'flyhook' 的对象！");
                }
            }
            else
            {
                Debug.LogError("FlyLine 上没有足够的 Obi Particle Attachment 组件！");
            }
        }
        else
        {
            Debug.LogError("未找到名为 'FlyLine' 的对象！");
        }
    }




    private void ExitAttraction()
    {
        isAttracted = false; // 取消吸引状态
        isReturning = true; // 开始返回状态
        currentTarget = exit1; // 首先移动到Exit1

        // 激活 FlyLineExtend 脚本，开始绳子变长
        if (FishDragLine != null)
        {
            FishDragLine.StartExtending(); // 开始延长绳子
            Debug.Log("绳子开始变长！");
        }

        // 设置Animator的IsExiting为True以播放TroutFast动画
        if (animator != null)
        {
            animator.SetBool("IsExiting", true);
        }

        // 切换 Character 的动画至 "FishOn"
        GameObject character = GameObject.Find("Character");
        if (character != null)
        {
            Animator anglerAnimator = character.GetComponent<Animator>();
            if (anglerAnimator != null)
            {
                anglerAnimator.SetBool("FishOn", true); // 切换 FishOn 动画
                Debug.Log("'Character' 的 FishOn 动画已切换为 True！");
            }
            else
            {
                Debug.LogError("未找到 Character 上的 Animator 组件！");
            }
        }
        else
        {
            Debug.LogError("未找到名为 'Character' 的对象！");
        }

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