using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class StretchController : MonoBehaviour
{
    public float changeAmount = 0.1f; // ÿ�ΰ���ʱ�ı����
    public float minStretchingScale = 0.5f; // ��С��չ����
    public float maxStretchingScale = 2f;   // �����չ����

    private ObiRope rope;

    void Start()
    {
        // ��ȡ��ǰGameObject�ϵ�ObiRope���
        rope = GetComponent<ObiRope>();

        // ȷ���ҵ�ObiRope���
        if (rope == null)
        {
            Debug.LogError("ObiRope component not found on this GameObject.");
        }
    }

    void Update()
    {
        // ����Wʱ�������ӣ�����Stretching scale��
        if (Input.GetKeyDown(KeyCode.W))
        {
            float newStretchingScale = rope.stretchingScale - changeAmount;
            rope.stretchingScale = Mathf.Max(newStretchingScale, minStretchingScale);
        }

        // ����Sʱ�ӳ����ӣ�����Stretching scale��
        if (Input.GetKeyDown(KeyCode.S))
        {
            float newStretchingScale = rope.stretchingScale + changeAmount;
            rope.stretchingScale = Mathf.Min(newStretchingScale, maxStretchingScale);
        }
    }
}
