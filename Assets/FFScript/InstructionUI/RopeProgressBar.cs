using UnityEngine;
using UnityEngine.UI;
using Obi;

public class RopeProgressBar : MonoBehaviour
{
    // ���� ObiRope ���
    public ObiRope rope;
    // ���ý����������Slider��
    public Slider progressBar;

    // �������ӳ��ȵ���Сֵ�����ֵ
    public float minRopeLength = 5f;
    public float maxRopeLength = 16f;

    void Update()
    {
        // ��ȡ��ǰ���ӳ���
        float currentRopeLength = rope.restLength;

        // �����ӳ���ӳ�䵽 0 �� 1 �ķ�Χ
        float progress = (currentRopeLength - minRopeLength) / (maxRopeLength - minRopeLength);

        // ȷ������ֵ�� 0 �� 1 ֮��
        progress = Mathf.Clamp01(progress);

        // ���½�������ֵ
        progressBar.value = progress;
    }
}
