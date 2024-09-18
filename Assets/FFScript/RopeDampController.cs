using UnityEngine;
using Obi;

public class RopeDampingControl : MonoBehaviour
{
    // 需要获取的 ObiSolver 组件
    private ObiSolver obiSolver;

    // damping 的两个值：按住空格时和松开空格时的值
    public float dampingWhenPressed = 0f;
    public float dampingWhenReleased = 0.8f;

    void Start()
    {
        // 获取当前 GameObject 上的 ObiSolver 组件
        obiSolver = GetComponent<ObiSolver>();

        // 确保找到组件
        if (obiSolver == null)
        {
            Debug.LogError("ObiSolver component not found on this GameObject.");
        }
    }

    void Update()
    {
        // 如果按下空格键，设置damping为0
        if (Input.GetKey(KeyCode.Space))
        {
            obiSolver.parameters.damping = dampingWhenPressed;
        }
        // 松开空格键，恢复damping为9
        else
        {
            obiSolver.parameters.damping = dampingWhenReleased;
        }
    }
}