using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    public Animator animator; // �������
    public AudioSource audioSource; // ��ƵԴ���

    [System.Serializable]
    public class AnimationSoundPair
    {
        public string animationClipName; // ������������
        public AudioClip soundEffect; // ��Ӧ����Ч
    }

    public AnimationSoundPair[] animationSoundPairs; // ��������Ч�Ķ�Ӧ����

    private string currentClipName = ""; // ��ǰ���ŵĶ�����������

    void Update()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        if (clipInfo.Length > 0)
        {
            string newClipName = clipInfo[0].clip.name;

            if (currentClipName != newClipName)
            {
                currentClipName = newClipName;

                // ����ƥ��Ķ�����������
                foreach (AnimationSoundPair pair in animationSoundPairs)
                {
                    if (pair.animationClipName == newClipName)
                    {
                        audioSource.clip = pair.soundEffect;
                        audioSource.Play();
                        break;
                    }
                }
            }
        }
    }
}
