using UnityEngine;

public class CamFollowFly : MonoBehaviour
{
    public Transform flyHook;               // FlyHook��Transform
    private Vector3 initialLocalOffset;     // �������FlyHook֮��ĳ�ʼ����ƫ����
    private Quaternion initialRotation;     // ������ĳ�ʼ��ת

    void Start()
    {
        if (flyHook != null)
        {
            // �����������FlyHook��������ϵ�еĳ�ʼƫ����
            initialLocalOffset = flyHook.InverseTransformPoint(transform.position);

            // ��¼������ĳ�ʼ��ת���Ա������������ת
            initialRotation = transform.rotation;
        }
        else
        {
            Debug.LogError("����Inspector��ָ��FlyHook�����Transform��");
        }
    }

    void LateUpdate()
    {
        if (flyHook != null)
        {
            // �����������FlyHook��������ϵ�е���λ��
            Vector3 newLocalPosition = initialLocalOffset;

            // �����ֻ����X��Y���ϸ��棬����ȷ��Z��ƫ�Ʊ��ֲ���
            // newLocalPosition.z = initialLocalOffset.z;

            // ������λ��ת������������ϵ
            Vector3 newWorldPosition = flyHook.TransformPoint(newLocalPosition);

            // �����������λ��
            transform.position = newWorldPosition;

            // ����������ĳ�ʼ��ת����ֹ���������FlyHook��ת
            transform.rotation = initialRotation;
        }
    }
}
