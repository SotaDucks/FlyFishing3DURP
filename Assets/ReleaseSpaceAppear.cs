using UnityEngine;
using UnityEngine.UI;

public class ReleaseSpaceAppear : MonoBehaviour
{
    public Slider ropeProgressBar; // ����RopeProgressBar Slider���
    public float fadeDuration = 1.0f; // ƽ�����ֵĳ���ʱ�䣬��λΪ��

    private CanvasGroup canvasGroup; // CanvasGroup���ڿ���UI͸����
    private bool isFadingIn = false; // �Ƿ�����ƽ������

    void Start()
    {
        // ��ȡCanvasGroup��������û�����Զ����
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // ��ʼ����͸����Ϊ0����ȫ͸����
        canvasGroup.alpha = 0f;
    }

    void Update()
    {
        // ���RopeProgressBar��ֵ�Ƿ����0.8
        if (ropeProgressBar != null && ropeProgressBar.value > 0.8f && !isFadingIn)
        {
            // ��ʼƽ������
            StartCoroutine(FadeIn());
        }
    }

    private System.Collections.IEnumerator FadeIn()
    {
        isFadingIn = true;
        float elapsedTime = 0f;

        // ƽ�����ɣ�����fadeDurationʱ��
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ȷ������͸����Ϊ1
        canvasGroup.alpha = 1f;
        isFadingIn = false;
    }
}
