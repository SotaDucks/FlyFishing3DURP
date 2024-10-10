using UnityEngine;

public class CameraFlyCon : MonoBehaviour
{
    public Transform target; // �����Ŀ�꣨�㣩
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Camera targetCamera; // ��������
    private FishAttraction fishAttraction; // FishAttraction���
    private bool isCameraActivated = false;

    private void Start()
    {
        // ��ȡ��������
        targetCamera = GetComponent<Camera>();
        if (targetCamera == null)
        {
            Debug.LogError("δ�ڸ���Ϸ�������ҵ�Camera�����");
            return;
        }

        // ��ʼʱ������������
        targetCamera.enabled = false;

        // ���Ŀ���Ƿ�������
        if (target != null)
        {
            // ��ȡFishAttraction���
            fishAttraction = target.GetComponent<FishAttraction>();
            if (fishAttraction == null)
            {
                Debug.LogError("δ��Ŀ�����ҵ�FishAttraction�����");
                return;
            }
        }
        else
        {
            Debug.LogError("δ����Ŀ�ꡣ");
        }
    }

    private void LateUpdate()
    {
        if (target == null || fishAttraction == null) return;

        // ���㱻����ʱ���������
        if (fishAttraction.isAttracted && !isCameraActivated)
        {
            ActivateCamera();
        }

        // ƽ������Ŀ�겢����Ŀ��
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }

    private void ActivateCamera()
    {
        targetCamera.enabled = true;
        isCameraActivated = true;
        Debug.Log("������Ѽ��");
    }
}

