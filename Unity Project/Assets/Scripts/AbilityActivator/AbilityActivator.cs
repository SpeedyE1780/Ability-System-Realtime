using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class AbilityActivator : ScriptableObject
{
    public abstract string Description();
    public abstract void ActivateAbility(PlayerController player);
}