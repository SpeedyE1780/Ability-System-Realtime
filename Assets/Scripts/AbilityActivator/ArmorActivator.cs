using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(menuName = "Ability/Activator/Armor")]
public class ArmorActivator : AbilityActivator
{
    public float Duration;
    public int DamageTaken;
    public override string Description()
    {
        StringBuilder description = new StringBuilder();
        description.Append($"Duration: {Duration} Seconds\n");
        description.Append($"Damage Taken: {DamageTaken}% Damage\n");
        return description.ToString();
    }

    public override void ActivateAbility(PlayerController player)
    {
        player.ActivateArmor(Duration , DamageTaken);
    }
}