using UnityEngine;

public class UIFadeIn : MonoBehaviour
{
    // �������ʱ�䣨�룩
    public float duration = 2f;

    // �ڲ���ʱ��
    private float timer = 0f;

    // CanvasGroup��������ڿ���UI͸����
    private CanvasGroup canvasGroup;

    void Start()
    {
        // ��ȡ�����CanvasGroup���
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // ��ʼ��͸����Ϊ0����ȫ͸����
        canvasGroup.alpha = 0f;
    }

    void Update()
    {
        if (timer < duration)
        {
            // ���Ӽ�ʱ��
            timer += Time.deltaTime;

            // ���㵱ǰ͸����
            float alpha = Mathf.Clamp01(timer / duration);

            // ����CanvasGroup��͸����
            canvasGroup.alpha = alpha;
        }
        else
        {
            // ȷ��͸����Ϊ1����ȫ��͸����
            canvasGroup.alpha = 1f;

            // ������ɺ󣬿��Խ��øýű�����ѡ��
            // enabled = false;
        }
    }
}
