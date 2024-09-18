using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class StretchController : MonoBehaviour
{
    public float changeAmount = 0.1f; // 每次按键时改变的量
    public float minStretchingScale = 0.5f; // 最小伸展比例
    public float maxStretchingScale = 2f;   // 最大伸展比例

    private ObiRope rope;

    void Start()
    {
        // 获取当前GameObject上的ObiRope组件
        rope = GetComponent<ObiRope>();

        // 确保找到ObiRope组件
        if (rope == null)
        {
            Debug.LogError("ObiRope component not found on this GameObject.");
        }
    }

    void Update()
    {
        // 按下W时缩短绳子（减少Stretching scale）
        if (Input.GetKeyDown(KeyCode.W))
        {
            float newStretchingScale = rope.stretchingScale - changeAmount;
            rope.stretchingScale = Mathf.Max(newStretchingScale, minStretchingScale);
        }

        // 按下S时延长绳子（增加Stretching scale）
        if (Input.GetKeyDown(KeyCode.S))
        {
            float newStretchingScale = rope.stretchingScale + changeAmount;
            rope.stretchingScale = Mathf.Min(newStretchingScale, maxStretchingScale);
        }
    }
}
