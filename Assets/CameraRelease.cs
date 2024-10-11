using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraRelease : MonoBehaviour
{
    public Transform target; // �����Ŀ�꣨�㣩
    public Vector3 offset;   // �������Ŀ���ƫ����
    public Button triggerButton; // ��������������İ�ť
    public float delayBeforeAdjustment = 2f; // �����ť��ȴ�������

    // �����������Ŀ��λ�ú���ת
    public Vector3 targetPositionOffset;
    public Vector3 targetRotationEuler;
    public float adjustmentDuration = 1.0f; // ����������Ķ�������ʱ��

    private bool shouldAdjust = false;
    private bool isAdjusting = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float elapsedTime = 0f;

    void Start()
    {
        if (triggerButton != null)
        {
            triggerButton.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        if (!isAdjusting)
        {
            StartCoroutine(AdjustCameraAfterDelay());
        }
    }

    IEnumerator AdjustCameraAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeAdjustment);
        StartCoroutine(AdjustCamera());
    }

    IEnumerator AdjustCamera()
    {
        isAdjusting = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        Vector3 finalPosition = target.position + targetPositionOffset;
        Quaternion finalRotation = Quaternion.Euler(targetRotationEuler);

        elapsedTime = 0f;

        while (elapsedTime < adjustmentDuration)
        {
            float t = elapsedTime / adjustmentDuration;
            transform.position = Vector3.Lerp(initialPosition, finalPosition, t);
            transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = finalPosition;
        transform.rotation = finalRotation;
        shouldAdjust = true;
        isAdjusting = false;
    }

    void LateUpdate()
    {
        if (target != null && shouldAdjust)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);
            transform.LookAt(target);
        }
    }
}
