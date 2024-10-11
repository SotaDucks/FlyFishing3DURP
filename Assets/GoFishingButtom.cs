using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoFishingButtom : MonoBehaviour
{
    // ���ð�ť���
    public Button nextButton;

    void Start()
    {
        if (nextButton != null)
        {
            // Ϊ��ť�ĵ���¼���Ӽ�����
            nextButton.onClick.AddListener(LoadNextScene);
        }
        else
        {
            Debug.LogError("Next Button is not assigned in the inspector.");
        }
    }

    // ������һ�������ķ���
    void LoadNextScene()
    {
        // ��ȡ��ǰ�����������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // ������һ������������
        int nextSceneIndex = currentSceneIndex + 1;

        // �����һ�������Ƿ����
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // ������һ������
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("�Ѿ������һ���������޷�������һ��������");
        }
    }
}
