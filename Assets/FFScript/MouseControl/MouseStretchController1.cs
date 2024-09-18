using UnityEngine;
using Obi;

public class ObiRopeStretchController : MonoBehaviour
{
    public ObiRope rope; // �ο���ObiRope���
    public float stretchingScaleIncrement = 0.1f; // ÿ�����ӵ���չ����
    private Vector3 lastMousePosition; // �ϴ����λ��
    private bool isDragging = false; // �����ק���

    void Update()
    {
        // �������������
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        // ����������ͷ�
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // ���������ק
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float mouseMovement = currentMousePosition.x - lastMousePosition.x;

            // �����������ƶ���һ������
            if (mouseMovement > 1) // 1��һ����ֵ������Ը�����Ҫ����
            {
                // ����Obi Rope��Stretching Scale
                rope.stretchingScale += stretchingScaleIncrement;

                // ����lastMousePosition
                lastMousePosition = currentMousePosition;
            }
        }
    }
}
