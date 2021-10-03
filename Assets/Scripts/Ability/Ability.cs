using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/New Ability")]
public class Ability : ScriptableObject
{
    public string AbilityName;
    public int StaminaCost;
    public Sprite AbilitySprite;
    public AbilityActivator Activator;

    public string Description()
    {
        StringBuilder description = new StringBuilder();
        
        description.Append($"Name: {AbilityName}\n");
        description.Append($"Stamina Cost: {StaminaCost}\n");
        description.Append(Activator.Description());

        return description.ToString();
    }

    public void TriggerAbility(PlayerController player)
    {
        Activator.ActivateAbility(player);
    }
}