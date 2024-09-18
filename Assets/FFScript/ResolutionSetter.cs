using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
    // 设置分辨率的宽度和高度
    public int width = 1920;
    public int height = 1080;
    // 是否全屏模式
    public bool fullscreen = false;

    void Start()
    {
        // 设置分辨率
        Screen.SetResolution(width, height, fullscreen);
    }
}
