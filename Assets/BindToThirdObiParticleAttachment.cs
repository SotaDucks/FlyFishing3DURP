using UnityEngine;
using Obi;

public class BindToThirdObiParticleAttachment : MonoBehaviour
{
    private GameObject flyLine; // FlyLine ����

    void Start()
    {
        // �Զ�������Ϊ "FlyLine(Obi Rope)" �Ķ���
        flyLine = GameObject.Find("FlyLine(Obi Rope)");

        if (flyLine != null)
        {
            // ��ȡ FlyLine �����ϵ����� ObiParticleAttachment ���
            ObiParticleAttachment[] attachments = flyLine.GetComponents<ObiParticleAttachment>();

            if (attachments.Length >= 3)
            {
                // ����ǰ����󶨵������� ObiParticleAttachment ��
                attachments[2].target = this.transform;
                Debug.Log("��ǰ�����Ѿ����󶨵� FlyLine �ĵ����� Obi Particle Attachment��");
            }
            else
            {
                Debug.LogError("FlyLine ��û���㹻�� Obi Particle Attachment �����");
            }
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'FlyLine(Obi Rope)' �Ķ���");
        }
    }
}
