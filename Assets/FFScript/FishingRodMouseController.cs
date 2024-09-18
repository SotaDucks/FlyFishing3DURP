using UnityEngine;

public class FishingRodMouseController : MonoBehaviour
{
    // 参数
    public float moveSpeed = 2f;      // 移动速度
    public float moveDistance = 2f;   // 移动的距离
    public float tiltSpeed = 50f;     // 倾斜速度
    public float tiltAngle = 30f;     // 最大倾斜角度

    private Vector3 initialPosition;  // 初始位置
    private Quaternion initialRotation; // 初始旋转

    private bool isMovingRight = false; // 正在向右移动
    private bool isMovingLeft = false;  // 正在向左移动
    private bool canMoveRight = false;   // 是否可以向右移动
    private bool canMoveLeft = true;   // 是否可以向左移动

    private Vector3 lastMousePosition;  // 上一次鼠标的位置
    private bool isDragging = false;    // 鼠标是否按下

    void Start()
    {
        // 记录初始位置和旋转
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // 检测鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        // 检测鼠标左键松开
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 当按住鼠标左键时
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float mouseDeltaX = currentMousePosition.x - lastMousePosition.x;

            // 如果鼠标向左移动且可以向左移动，鱼竿左移并倾斜
            if (mouseDeltaX < 0 && canMoveLeft)
            {
                isMovingLeft = true;
                canMoveLeft = false;
                canMoveRight = false;  // 防止在移动过程中向右移动
            }

            // 如果鼠标向右移动且可以向右移动，鱼竿右移并倾斜
            if (mouseDeltaX > 0 && canMoveRight)
            {
                isMovingRight = true;
                canMoveRight = false;
                canMoveLeft = false;  // 防止在移动过程中向左移动
            }

            lastMousePosition = currentMousePosition;
        }

        // 执行右移动和倾斜
        if (isMovingRight)
        {
            MoveAndTiltRight();
        }

        // 执行左移动和恢复初始状态
        if (isMovingLeft)
        {
            MoveAndTiltLeft();
        }
    }

    void MoveAndTiltRight()
    {
        // 移动鱼竿向右
        if (transform.position.x < initialPosition.x + moveDistance)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        // 倾斜鱼竿向右
        if (transform.rotation.eulerAngles.z > 360 - tiltAngle || transform.rotation.eulerAngles.z < tiltAngle)
        {
            transform.Rotate(Vector3.back, tiltSpeed * Time.deltaTime);
        }
        else
        {
            // 完成右边动作后保持在该状态并允许向左移动
            isMovingRight = false;
            canMoveLeft = true; // 现在可以按左移动回初始状态
        }
    }

    void MoveAndTiltLeft()
    {
        // 移动鱼竿向左回到初始位置
        if (transform.position.x > initialPosition.x)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        // 倾斜鱼竿回到初始角度
        if (transform.rotation.eulerAngles.z != initialRotation.eulerAngles.z)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, tiltSpeed * Time.deltaTime);
        }

        // 完成左边动作并回到初始状态
        if (transform.position.x <= initialPosition.x && Quaternion.Angle(transform.rotation, initialRotation) < 0.1f)
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            isMovingLeft = false;
            canMoveRight = true; // 恢复初始状态，可以向右移动
        }
    }
}