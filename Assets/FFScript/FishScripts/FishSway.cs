using UnityEngine;

public class FishSway : MonoBehaviour
{
    public float swayAmplitude = 15f; // 摆动幅度（度数）
    public float swayFrequency = 2f;  // 摆动频率

    private Quaternion initialLocalRotation;

    void Start()
    {
        // 记录初始局部旋转
        initialLocalRotation = transform.localRotation;
    }

    void Update()
    {
        // 计算当前时间的角度
        float angle = Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;

        // 创建一个绕 Y 轴的旋转
        Quaternion swayRotation = Quaternion.Euler(0f, angle, 0f);

        // 将摆动旋转应用到初始旋转上
        transform.localRotation = initialLocalRotation * swayRotation;
    }
}
