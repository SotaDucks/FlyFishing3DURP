using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishStaminaBar : MonoBehaviour
{
    public Slider fishStaminaBar;

    private int maxStamina = 100;
    public int currentStamina;

    // 正常耐力回复速度和间隔
    public float normalRegenSpeed = 2f; // 每次回复的耐力值
    public float normalRegenTick = 0.001f; // 回复间隔

    // 耐力为0时的延迟和特定回复速度
    public float zeroStaminaDelay = 2f; // 延迟时间
    public float StaminaChargingSpeed = 5f; // 特定的回复速度
    public float zeroRegenTick = 0.001f; // 特定回复间隔

    private bool isRegeneratingAfterZero = false; // 标记是否在特定回复状态

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
            // 在特定回复期间，耐力无法被减少
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

            // 启动耐力耗尽后的特定回复协程
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

