using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaTest : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            FishStaminaBar.instance.UseStamina(15);
    }
}
