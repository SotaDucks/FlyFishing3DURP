using UnityEngine;

public class FishLanding : MonoBehaviour
{
    public float retrieveSpeed = 1f; // 收回绳子的速度
    private Rigidbody fishRigidbody; // 鱼的 Rigidbody
    private FishDragLine fishDragLine; // 引用 FishDragLine 脚本

    void Start()
    {
        fishRigidbody = GetComponent<Rigidbody>();
        fishDragLine = FindObjectOfType<FishDragLine>(); // 查找 FishDragLine 脚本

        if (fishRigidbody == null)
        {
            Debug.LogError("未找到鱼的 Rigidbody 组件！");
        }
        if (fishDragLine == null)
        {
            Debug.LogError("未找到 FishDragLine 脚本！");
        }
    }

    void OnEnable()
    {
        if (fishRigidbody != null)
        {
            fishRigidbody.isKinematic = false; // 设置 Rigidbody 为非运动状态
        }

        // 直接开始收回绳子
        if (fishDragLine != null)
        {
            fishDragLine.StartRetrieving(); // 开始收回绳子
        }
    }
}
