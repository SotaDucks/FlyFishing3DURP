using UnityEngine;
using Obi;

public class FlyhookController : MonoBehaviour
{
    public GameObject flyhook;
    public Collider WaterSurfaceCollider;
    public ObiCollider obiCollider;
    public ObiSolver obiSolver;
    public float waterGravity = -1f; // ˮ�µ��㹳gravityֵ
    public float waterSolverGravityY = -0.5f; // ˮ�µ�����gravity Yֵ

    private Rigidbody flyhookRigidbody;
    private bool isObiColliderActive = false;
    private Vector3 defaultGravity;
    private float defaultSolverGravityY;

    void Start()
    {
        // ��ȡ flyhook �� Rigidbody
        flyhookRigidbody = flyhook.GetComponent<Rigidbody>();
        // ����ϵͳĬ�ϵ� gravity ֵ
        defaultGravity = Physics.gravity;
        // ���� Obi Solver Ĭ�ϵ� gravity Y ֵ
        defaultSolverGravityY = obiSolver.parameters.gravity.y;
    }

    void Update()
    {
        // ���ո���Ƿ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isObiColliderActive = true;
            obiCollider.enabled = true; // ���� Obi Collider
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isObiColliderActive = false;
            obiCollider.enabled = false; // ���� Obi Collider
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ȷ���ո��δ������ flyhook ���� WaterSurfaceCollider
        if (!isObiColliderActive && other == WaterSurfaceCollider)
        {
            // ģ���㹳��ˮ�µĻ���Ư�����޸� gravity
            flyhookRigidbody.useGravity = false;
            flyhookRigidbody.AddForce(new Vector3(0, waterGravity, 0), ForceMode.Acceleration);

            // �޸� Obi Solver �� gravity Y ֵ��ģ�����ߵ�Ư��
            var solverParams = obiSolver.parameters;
            solverParams.gravity = new Vector3(solverParams.gravity.x, waterSolverGravityY, solverParams.gravity.z);
            obiSolver.parameters = solverParams;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ȷ�� flyhook �뿪 WaterSurfaceCollider���ָ�Ĭ�� gravity
        if (!isObiColliderActive && other == WaterSurfaceCollider)
        {
            // �ָ�ϵͳĬ�ϵ� gravity ֵ
            flyhookRigidbody.useGravity = true;

            // �ָ� Obi Solver ��Ĭ�� gravity Y ֵ
            var solverParams = obiSolver.parameters;
            solverParams.gravity = new Vector3(solverParams.gravity.x, defaultSolverGravityY, solverParams.gravity.z);
            obiSolver.parameters = solverParams;
        }
    }
}
