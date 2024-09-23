using UnityEngine;
using Obi;

public class BindToThirdObiParticleAttachment : MonoBehaviour
{
    private GameObject flyLine; // FlyLine 对象

    void Start()
    {
        // 自动查找名为 "FlyLine(Obi Rope)" 的对象
        flyLine = GameObject.Find("FlyLine(Obi Rope)");

        if (flyLine != null)
        {
            // 获取 FlyLine 对象上的所有 ObiParticleAttachment 组件
            ObiParticleAttachment[] attachments = flyLine.GetComponents<ObiParticleAttachment>();

            if (attachments.Length >= 3)
            {
                // 将当前对象绑定到第三个 ObiParticleAttachment 上
                attachments[2].target = this.transform;
                Debug.Log("当前对象已经被绑定到 FlyLine 的第三个 Obi Particle Attachment！");
            }
            else
            {
                Debug.LogError("FlyLine 上没有足够的 Obi Particle Attachment 组件！");
            }
        }
        else
        {
            Debug.LogError("未找到名为 'FlyLine(Obi Rope)' 的对象！");
        }
    }
}
