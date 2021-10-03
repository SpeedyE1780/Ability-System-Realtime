using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(menuName = "Ability/Activator/Life Steal")]
public class LifeStealActivator : AbilityActivator
{
    public int HealthAmount;

    public override string Description()
    {
        StringBuilder description = new StringBuilder();
        description.Append($"Drain: {HealthAmount} HP From Enemy\n");
        return description.ToString();
    }

    public override void ActivateAbility(PlayerController player)
    {
        player.AddHealth(HealthAmount);
        player.Enemy.StealHealth(HealthAmount);
    }
}