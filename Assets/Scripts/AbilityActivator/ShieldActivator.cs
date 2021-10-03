using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Activator/Shield")]
public class ShieldActivator : AbilityActivator
{
    public int Ressistance;
    public override string Description()
    {
        StringBuilder description = new StringBuilder();
        description.Append($"Ressistance: {Ressistance} Hits\n");
        return description.ToString();
    }

    public override void ActivateAbility(PlayerController player)
    {
        player.ActivateShield(Ressistance);
    }
}