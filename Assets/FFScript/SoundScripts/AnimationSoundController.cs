using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    public Animator animator; // �������
    public AudioSource audioSource; // ���ڲ��Ŷ�����Ӧ��Ч����ƵԴ���

    [System.Serializable]
    public class AnimationSoundPair
    {
        public string animationClipName; // ������������
        public AudioClip soundEffect; // ��Ӧ����Ч
    }

    public AnimationSoundPair[] animationSoundPairs; // ��������Ч�Ķ�Ӧ����

    // ��������
    public AudioClip dragSoundEffect; // Drag��Ч
    public AudioSource dragAudioSource; // ���ڲ���Drag��Ч����ƵԴ���
    public FishDragLine fishDragLine; // ��FishDragLine�ű�������

    private string currentClipName = ""; // ��ǰ���ŵĶ�����������

    void Update()
    {
        // ��⶯�������Ŷ�Ӧ��Ч
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

        // �������֣����FishDragLine�Ĳ������������Ż�ֹͣDrag��Ч
        if (fishDragLine != null && dragSoundEffect != null && dragAudioSource != null)
        {
            if (fishDragLine.isDragging || fishDragLine.isStruggling)
            {
                if (!dragAudioSource.isPlaying)
                {
                    dragAudioSource.clip = dragSoundEffect;
                    dragAudioSource.Play();
                }
            }
            else
            {
                if (dragAudioSource.isPlaying)
                {
                    dragAudioSource.Stop();
                }
            }
        }
    }
}
