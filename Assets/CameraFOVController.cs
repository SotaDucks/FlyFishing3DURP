using UnityEngine;
using System.Collections.Generic;

public class CameraFOVController : MonoBehaviour
{
    [System.Serializable]
    public class AnimationFOVSettings
    {
        public string animationStateName; // ����״̬����
        public float fovIncreaseAmount = 5f; // ��Ұ������
        public float fovSmoothTime = 0.5f; // ��Ұƽ������ʱ��
        public float maxFOV = 90f; // �����Ұ�Ƕ�
    }

    public Animator characterAnimator; // ��ɫ��Animator���
    public List<AnimationFOVSettings> animationFOVSettingsList; // ������FOV���õ��б�

    private Camera targetCamera;
    private Dictionary<int, AnimationFOVSettings> animationSettingsDict;
    private float defaultFOV;
    private float targetFOV;
    private float fovVelocity = 0f;
    private int currentAnimationHash = 0;

    void Start()
    {
        // ��ȡ��������
        targetCamera = GetComponent<Camera>();
        if (targetCamera == null)
        {
            Debug.LogError("δ�ڸ���Ϸ�������ҵ�Camera�����");
            return;
        }

        // ����Ƿ���ָ����ɫ��Animator
        if (characterAnimator == null)
        {
            Debug.LogError("δָ����ɫ��Animator�����");
            return;
        }

        // �����������Ĭ��FOV
        defaultFOV = targetCamera.fieldOfView;
        targetFOV = defaultFOV;

        // ��ʼ������״̬��ϣֵ�����õ��ֵ�
        animationSettingsDict = new Dictionary<int, AnimationFOVSettings>();
        foreach (var settings in animationFOVSettingsList)
        {
            int animationHash = Animator.StringToHash(settings.animationStateName);
            if (!animationSettingsDict.ContainsKey(animationHash))
            {
                animationSettingsDict.Add(animationHash, settings);
            }
            else
            {
                Debug.LogWarning($"����״̬���� '{settings.animationStateName}' �ظ����Ѻ��ԡ�");
            }
        }
    }

    void Update()
    {
        if (characterAnimator == null || targetCamera == null) return;

        // ��ȡ��ǰ����״̬��Ϣ
        AnimatorStateInfo stateInfo = characterAnimator.GetCurrentAnimatorStateInfo(0);
        int currentStateHash = stateInfo.shortNameHash;

        // ��鵱ǰ�����Ƿ����б���
        if (animationSettingsDict.ContainsKey(currentStateHash))
        {
            // �����ǰ���������仯������Ŀ��FOV
            if (currentAnimationHash != currentStateHash)
            {
                currentAnimationHash = currentStateHash;
                AnimationFOVSettings settings = animationSettingsDict[currentStateHash];
                 
                // �����µ�Ŀ��FOV��ȷ�����������ֵ
                targetFOV = Mathf.Min(targetFOV + settings.fovIncreaseAmount, settings.maxFOV);
                // ����fovVelocity������SmoothDamp��֮ǰ���ٶ�Ӱ��
                fovVelocity = 0f;
            }
        }
        else
        {
            // �����ǰ���������б��У���֮ǰ��ƥ��Ķ�������Ҫ����FOV
            if (currentAnimationHash != 0)
            {
                currentAnimationHash = 0;
                targetFOV = defaultFOV;
                fovVelocity = 0f;
            }
        }

        // ƽ�������������FOV��Ŀ��ֵ
        float smoothTime = animationSettingsDict.ContainsKey(currentStateHash) ? animationSettingsDict[currentStateHash].fovSmoothTime : 0.5f;
        targetCamera.fieldOfView = Mathf.SmoothDamp(targetCamera.fieldOfView, targetFOV, ref fovVelocity, smoothTime);
    }

    // �����Ҫ�������ط������������FOV�����Ե��ô˷���
    public void ResetCameraFOV()
    {
        targetFOV = defaultFOV;
    }
}
