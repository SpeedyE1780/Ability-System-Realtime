using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGenerator
{
    public Ability GenerateAbility(List<Ability> allAbilities , HashSet<Ability> currentAbilities)
    {
        Ability ability = allAbilities[Random.Range(0, allAbilities.Count)];

        while(currentAbilities.Contains(ability))
        {
            ability = allAbilities[Random.Range(0, allAbilities.Count)];
        }

        return ability;
    }
}