using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public FloatValue playerHealth;
    public Signal healthSignal;

    public void Use(int amountToIncrease)
    {
        if (playerHealth.RuntimeValue < 6f)
        {
            playerHealth.RuntimeValue += amountToIncrease;
            healthSignal.Raise();
        }
    }
}
