using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
    // ���÷ֱ��ʵĿ�Ⱥ͸߶�
    public int width = 1920;
    public int height = 1080;
    // �Ƿ�ȫ��ģʽ
    public bool fullscreen = false;

    void Start()
    {
        // ���÷ֱ���
        Screen.SetResolution(width, height, fullscreen);
    }
}
