using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(menuName = "Ability/Activator/Poison")]
public class PoisonActivator : AbilityActivator
{
    public int Damage;
    public int Hits;
    public float Rate;
    public override string Description()
    {
        StringBuilder description = new StringBuilder();
        description.Append($"Poison Damage: {Damage} HP/{Rate} Seconds\n");
        description.Append($"Number of Hits: {Hits} Hits\n");
        return description.ToString();
    }

    public override void ActivateAbility(PlayerController player)
    {
        player.Enemy.ActivatePoison(Damage, Hits, Rate);
    }
}