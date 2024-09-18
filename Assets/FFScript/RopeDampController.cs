using UnityEngine;
using Obi;

public class RopeDampingControl : MonoBehaviour
{
    // ��Ҫ��ȡ�� ObiSolver ���
    private ObiSolver obiSolver;

    // damping ������ֵ����ס�ո�ʱ���ɿ��ո�ʱ��ֵ
    public float dampingWhenPressed = 0f;
    public float dampingWhenReleased = 0.8f;

    void Start()
    {
        // ��ȡ��ǰ GameObject �ϵ� ObiSolver ���
        obiSolver = GetComponent<ObiSolver>();

        // ȷ���ҵ����
        if (obiSolver == null)
        {
            Debug.LogError("ObiSolver component not found on this GameObject.");
        }
    }

    void Update()
    {
        // ������¿ո��������dampingΪ0
        if (Input.GetKey(KeyCode.Space))
        {
            obiSolver.parameters.damping = dampingWhenPressed;
        }
        // �ɿ��ո�����ָ�dampingΪ9
        else
        {
            obiSolver.parameters.damping = dampingWhenReleased;
        }
    }
}