using UnityEngine;
using Obi;

public class FlyhookController : MonoBehaviour
{
    public GameObject flyhook;
    public Collider WaterSurfaceCollider;
    public ObiCollider obiCollider;
    public ObiSolver obiSolver;
    public float waterGravity = -1f; // 水下的鱼钩gravity值
    public float waterSolverGravityY = -0.5f; // 水下的鱼线gravity Y值

    private Rigidbody flyhookRigidbody;
    private bool isObiColliderActive = false;
    private Vector3 defaultGravity;
    private float defaultSolverGravityY;

    void Start()
    {
        // 获取 flyhook 的 Rigidbody
        flyhookRigidbody = flyhook.GetComponent<Rigidbody>();
        // 保存系统默认的 gravity 值
        defaultGravity = Physics.gravity;
        // 保存 Obi Solver 默认的 gravity Y 值
        defaultSolverGravityY = obiSolver.parameters.gravity.y;
    }

    void Update()
    {
        // 检测空格键是否按下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isObiColliderActive = true;
            obiCollider.enabled = true; // 启用 Obi Collider
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isObiColliderActive = false;
            obiCollider.enabled = false; // 禁用 Obi Collider
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 确保空格键未按下且 flyhook 进入 WaterSurfaceCollider
        if (!isObiColliderActive && other == WaterSurfaceCollider)
        {
            // 模拟鱼钩在水下的缓慢漂浮，修改 gravity
            flyhookRigidbody.useGravity = false;
            flyhookRigidbody.AddForce(new Vector3(0, waterGravity, 0), ForceMode.Acceleration);

            // 修改 Obi Solver 的 gravity Y 值，模拟鱼线的漂浮
            var solverParams = obiSolver.parameters;
            solverParams.gravity = new Vector3(solverParams.gravity.x, waterSolverGravityY, solverParams.gravity.z);
            obiSolver.parameters = solverParams;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 确保 flyhook 离开 WaterSurfaceCollider，恢复默认 gravity
        if (!isObiColliderActive && other == WaterSurfaceCollider)
        {
            // 恢复系统默认的 gravity 值
            flyhookRigidbody.useGravity = true;

            // 恢复 Obi Solver 的默认 gravity Y 值
            var solverParams = obiSolver.parameters;
            solverParams.gravity = new Vector3(solverParams.gravity.x, defaultSolverGravityY, solverParams.gravity.z);
            obiSolver.parameters = solverParams;
        }
    }
}
