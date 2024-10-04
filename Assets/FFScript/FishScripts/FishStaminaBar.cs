using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishStaminaBar : MonoBehaviour
{
    public Slider fishStaminaBar;

    private int maxStamina = 100;
    private int currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.001f);
    private Coroutine regen;

    public static FishStaminaBar instance;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        fishStaminaBar.maxValue = maxStamina;
        fishStaminaBar.value = maxStamina;
    }

   public void UseStamina(int amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            fishStaminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
        }

        else
        {
            Debug.Log("not enough stamina");
        }

    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(0.001f);

        while(currentStamina < maxStamina)
        {
            currentStamina += 2; //maxStamina / 100;
            fishStaminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;

    }


}
