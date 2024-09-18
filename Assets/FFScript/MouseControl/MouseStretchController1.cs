using UnityEngine;
using Obi;

public class ObiRopeStretchController : MonoBehaviour
{
    public ObiRope rope; // 参考到ObiRope组件
    public float stretchingScaleIncrement = 0.1f; // 每次增加的伸展比例
    private Vector3 lastMousePosition; // 上次鼠标位置
    private bool isDragging = false; // 鼠标拖拽标记

    void Update()
    {
        // 检查鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        // 检查鼠标左键释放
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 如果正在拖拽
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float mouseMovement = currentMousePosition.x - lastMousePosition.x;

            // 如果鼠标向右移动了一定距离
            if (mouseMovement > 1) // 1是一个阈值，你可以根据需要调整
            {
                // 增加Obi Rope的Stretching Scale
                rope.stretchingScale += stretchingScaleIncrement;

                // 更新lastMousePosition
                lastMousePosition = currentMousePosition;
            }
        }
    }
}
