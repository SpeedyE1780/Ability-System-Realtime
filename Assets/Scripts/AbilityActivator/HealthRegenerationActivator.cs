using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Activator/Health Regeneration")]
public class HealthRegenerationActivator : AbilityActivator
{
    public int Health;
    public int Duration;
    public float Rate;

    public override string Description()
    {
        StringBuilder description = new StringBuilder();
        description.Append($"Health Regeneration: {Health} HP/{Rate} Seconds\n");
        description.Append($"Regenerate: {Duration} Times\n");
        return description.ToString();
    }

    public override void ActivateAbility(PlayerController player)
    {
        player.RegenerateHealth(Duration, Health , Rate);
    }
}
