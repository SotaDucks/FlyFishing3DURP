using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishStaminaBar : MonoBehaviour
{
    public Slider fishStaminaBar;

    private int maxStamina = 100;
    public int currentStamina;

    // ���������ظ��ٶȺͼ��
    public float normalRegenSpeed = 2f; // ÿ�λظ�������ֵ
    public float normalRegenTick = 0.001f; // �ظ����

    // ����Ϊ0ʱ���ӳٺ��ض��ظ��ٶ�
    public float zeroStaminaDelay = 2f; // �ӳ�ʱ��
    public float StaminaChargingSpeed = 5f; // �ض��Ļظ��ٶ�
    public float zeroRegenTick = 0.001f; // �ض��ظ����

    private bool isRegeneratingAfterZero = false; // ����Ƿ����ض��ظ�״̬

    private Coroutine regen;

    public static FishStaminaBar instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentStamina = maxStamina;
        fishStaminaBar.maxValue = maxStamina;
        fishStaminaBar.value = maxStamina;
    }

    public void UseStamina(int amount)
    {
        if (isRegeneratingAfterZero)
        {
            // ���ض��ظ��ڼ䣬�����޷�������
            return;
        }

        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            fishStaminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            currentStamina = 0;
            fishStaminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(regen);

            // ���������ľ�����ض��ظ�Э��
            regen = StartCoroutine(RegenStaminaAfterZero());
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(normalRegenTick);

        while (currentStamina < maxStamina && !isRegeneratingAfterZero)
        {
            currentStamina += Mathf.RoundToInt(normalRegenSpeed);
            fishStaminaBar.value = currentStamina;
            yield return new WaitForSeconds(normalRegenTick);
        }

        regen = null;
    }

    private IEnumerator RegenStaminaAfterZero()
    {
        isRegeneratingAfterZero = true;

        yield return new WaitForSeconds(zeroStaminaDelay);

        while (currentStamina < maxStamina)
        {
            currentStamina += Mathf.RoundToInt(StaminaChargingSpeed);
            fishStaminaBar.value = currentStamina;
            yield return new WaitForSeconds(zeroRegenTick);
        }

        isRegeneratingAfterZero = false;
        regen = null;
    }
}

