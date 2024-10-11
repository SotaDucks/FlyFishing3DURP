using UnityEngine;
using System.Collections;

public class CameraFlyCon : MonoBehaviour
{
    public Transform target; // �����Ŀ�꣨�㣩
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Viewport Rect ������ز���
    [Header("Viewport Rect Animation Settings")]
    public float initialViewportX = 0.99f; // ��ʼXֵ
    public float initialViewportY = 0.99f; // ��ʼYֵ
    public float finalViewportX = 0.6f;    // ����Xֵ
    public float finalViewportY = 0.6f;    // ����Yֵ
    public float viewportAnimationDuration = 1.0f; // ��������ʱ�䣨�룩

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

        // ����������ĳ�ʼ Viewport Rect
        targetCamera.rect = new Rect(initialViewportX, initialViewportY, targetCamera.rect.width, targetCamera.rect.height);

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

        // ���� Viewport Rect ����Э��
        StartCoroutine(AnimateViewportRect());
    }

    private IEnumerator AnimateViewportRect()
    {
        float elapsedTime = 0f;

        // ��ȡ��ǰ Viewport Rect
        Rect startRect = targetCamera.rect;

        // ����Ŀ�� Viewport Rect
        Rect endRect = new Rect(finalViewportX, finalViewportY, startRect.width, startRect.height);

        while (elapsedTime < viewportAnimationDuration)
        {
            // �����ֵ����
            float t = elapsedTime / viewportAnimationDuration;

            // ƽ����ֵ Viewport Rect �� X �� Y
            float currentX = Mathf.Lerp(initialViewportX, finalViewportX, t);
            float currentY = Mathf.Lerp(initialViewportY, finalViewportY, t);

            // �����µ� Viewport Rect
            targetCamera.rect = new Rect(currentX, currentY, startRect.width, startRect.height);

            // ��������ʱ��
            elapsedTime += Time.deltaTime;

            // �ȴ���һ֡
            yield return null;
        }

        // ȷ������ Viewport Rect �ﵽĿ��ֵ
        targetCamera.rect = new Rect(finalViewportX, finalViewportY, targetCamera.rect.width, targetCamera.rect.height);
    }
}
