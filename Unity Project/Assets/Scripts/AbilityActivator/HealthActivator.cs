using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Activator/Health")]
public class HealthActivator : AbilityActivator
{
    public int Health;
    public override string Description()
    {
        StringBuilder description = new StringBuilder();
        description.Append($"Regenerate: {Health} HP\n");
        return description.ToString();
    }

    public override void ActivateAbility(PlayerController player)
    {
        player.AddHealth(Health);
    }
}