using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyhookMassController : MonoBehaviour
{
    public BoxCollider waterSurfaceCollider;  // 引用WaterSurfaceCollider的Box Collider
    public float defaultMass = 1.0f;          // flyhook的默认质量
    public float waterMass = 0.5f;            // flyhook在水中的质量
    private Rigidbody flyhookRigidbody;       // flyhook的刚体组件
    private bool isInWater = false;           // 标记flyhook是否在水中

    void Start()
    {
        // 获取flyhook的刚体组件
        flyhookRigidbody = GetComponent<Rigidbody>();
        if (flyhookRigidbody == null)
        {
            Debug.LogError("Flyhook上缺少Rigidbody组件！");
        }

        // 设置初始质量为默认质量
        flyhookRigidbody.mass = defaultMass;
    }

    // 触发器检测进入水中
    private void OnTriggerEnter(Collider other)
    {
        // 判断flyhook是否进入WaterSurfaceCollider
        if (other == waterSurfaceCollider)
        {
            isInWater = true;

            // 设置新的质量
            flyhookRigidbody.mass = waterMass;
            Debug.Log($"Flyhook 进入水中，质量设置为 {waterMass}");
        }
    }

    // 触发器检测离开水面
    private void OnTriggerExit(Collider other)
    {
        // 判断flyhook是否离开WaterSurfaceCollider
        if (other == waterSurfaceCollider)
        {
            isInWater = false;

            // 恢复默认质量
            flyhookRigidbody.mass = defaultMass;
            Debug.Log($"Flyhook 离开水中，质量恢复为默认值 {defaultMass}");
        }
    }
}
