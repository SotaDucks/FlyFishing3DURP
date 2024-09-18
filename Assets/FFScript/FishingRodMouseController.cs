using UnityEngine;

public class FishingRodMouseController : MonoBehaviour
{
    // ����
    public float moveSpeed = 2f;      // �ƶ��ٶ�
    public float moveDistance = 2f;   // �ƶ��ľ���
    public float tiltSpeed = 50f;     // ��б�ٶ�
    public float tiltAngle = 30f;     // �����б�Ƕ�

    private Vector3 initialPosition;  // ��ʼλ��
    private Quaternion initialRotation; // ��ʼ��ת

    private bool isMovingRight = false; // ���������ƶ�
    private bool isMovingLeft = false;  // ���������ƶ�
    private bool canMoveRight = false;   // �Ƿ���������ƶ�
    private bool canMoveLeft = true;   // �Ƿ���������ƶ�

    private Vector3 lastMousePosition;  // ��һ������λ��
    private bool isDragging = false;    // ����Ƿ���

    void Start()
    {
        // ��¼��ʼλ�ú���ת
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // �������������
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        // ����������ɿ�
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // ����ס������ʱ
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float mouseDeltaX = currentMousePosition.x - lastMousePosition.x;

            // �����������ƶ��ҿ��������ƶ���������Ʋ���б
            if (mouseDeltaX < 0 && canMoveLeft)
            {
                isMovingLeft = true;
                canMoveLeft = false;
                canMoveRight = false;  // ��ֹ���ƶ������������ƶ�
            }

            // �����������ƶ��ҿ��������ƶ���������Ʋ���б
            if (mouseDeltaX > 0 && canMoveRight)
            {
                isMovingRight = true;
                canMoveRight = false;
                canMoveLeft = false;  // ��ֹ���ƶ������������ƶ�
            }

            lastMousePosition = currentMousePosition;
        }

        // ִ�����ƶ�����б
        if (isMovingRight)
        {
            MoveAndTiltRight();
        }

        // ִ�����ƶ��ͻָ���ʼ״̬
        if (isMovingLeft)
        {
            MoveAndTiltLeft();
        }
    }

    void MoveAndTiltRight()
    {
        // �ƶ��������
        if (transform.position.x < initialPosition.x + moveDistance)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        // ��б�������
        if (transform.rotation.eulerAngles.z > 360 - tiltAngle || transform.rotation.eulerAngles.z < tiltAngle)
        {
            transform.Rotate(Vector3.back, tiltSpeed * Time.deltaTime);
        }
        else
        {
            // ����ұ߶����󱣳��ڸ�״̬�����������ƶ�
            isMovingRight = false;
            canMoveLeft = true; // ���ڿ��԰����ƶ��س�ʼ״̬
        }
    }

    void MoveAndTiltLeft()
    {
        // �ƶ��������ص���ʼλ��
        if (transform.position.x > initialPosition.x)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        // ��б��ͻص���ʼ�Ƕ�
        if (transform.rotation.eulerAngles.z != initialRotation.eulerAngles.z)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, tiltSpeed * Time.deltaTime);
        }

        // �����߶������ص���ʼ״̬
        if (transform.position.x <= initialPosition.x && Quaternion.Angle(transform.rotation, initialRotation) < 0.1f)
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            isMovingLeft = false;
            canMoveRight = true; // �ָ���ʼ״̬�����������ƶ�
        }
    }
}