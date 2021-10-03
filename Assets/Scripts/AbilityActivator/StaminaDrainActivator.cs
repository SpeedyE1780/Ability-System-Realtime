using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(menuName = "Ability/Activator/Stamina Drain")]
public class StaminaDrainActivator : AbilityActivator
{
    public int StaminaAmount;

    public override string Description()
    {
        StringBuilder description = new StringBuilder();
        description.Append($"Drain: {StaminaAmount} Stamina From Enemy\n");
        return description.ToString();
    }

    public override void ActivateAbility(PlayerController player)
    {
        player.ActivateStaminaDrain(StaminaAmount);
        player.Enemy.ActivateStaminaDrain(-StaminaAmount);
    }
}