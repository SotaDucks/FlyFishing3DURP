using UnityEngine;

public class FishLanding : MonoBehaviour
{
    public float retrieveSpeed = 1f; // �ջ����ӵ��ٶ�
    private Rigidbody fishRigidbody; // ��� Rigidbody
    private FishDragLine fishDragLine; // ���� FishDragLine �ű�

    void Start()
    {
        fishRigidbody = GetComponent<Rigidbody>();
        fishDragLine = FindObjectOfType<FishDragLine>(); // ���� FishDragLine �ű�

        if (fishRigidbody == null)
        {
            Debug.LogError("δ�ҵ���� Rigidbody �����");
        }
        if (fishDragLine == null)
        {
            Debug.LogError("δ�ҵ� FishDragLine �ű���");
        }
    }

    void OnEnable()
    {
        if (fishRigidbody != null)
        {
            fishRigidbody.isKinematic = false; // ���� Rigidbody Ϊ���˶�״̬
        }

        // ֱ�ӿ�ʼ�ջ�����
        if (fishDragLine != null)
        {
            fishDragLine.StartRetrieving(); // ��ʼ�ջ�����
        }
    }
}
