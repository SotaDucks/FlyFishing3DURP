using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TryAgainButtom : MonoBehaviour
{
    // ���ð�ť���
    public Button reloadButton;

    void Start()
    {
        if (reloadButton != null)
        {
            // Ϊ��ť�ĵ���¼���Ӽ�����
            reloadButton.onClick.AddListener(ReloadCurrentScene);
        }
        else
        {
            Debug.LogError("Reload Button is not assigned in the inspector.");
        }
    }

    // ���¼��ص�ǰ�����ķ���
    void ReloadCurrentScene()
    {
        // ��ȡ��ǰ�����������
        string sceneName = SceneManager.GetActiveScene().name;
        // ���¼��س���
        SceneManager.LoadScene(sceneName);
    }
}
