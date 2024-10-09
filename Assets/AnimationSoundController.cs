using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    public Animator animator; // 动画组件
    public AudioSource audioSource; // 音频源组件

    [System.Serializable]
    public class AnimationSoundPair
    {
        public string animationClipName; // 动画剪辑名称
        public AudioClip soundEffect; // 对应的音效
    }

    public AnimationSoundPair[] animationSoundPairs; // 动画和音效的对应数组

    private string currentClipName = ""; // 当前播放的动画剪辑名称

    void Update()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        if (clipInfo.Length > 0)
        {
            string newClipName = clipInfo[0].clip.name;

            if (currentClipName != newClipName)
            {
                currentClipName = newClipName;

                // 查找匹配的动画剪辑名称
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
