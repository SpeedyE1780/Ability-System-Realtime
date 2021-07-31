using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    PlayerController player;
    int maxStamina;
    int currentStamina;
    bool AI;
    List<Ability> Abilities;
    HashSet<Ability> availableAbilities;
    Ability nextAbility;
    AbilityGenerator generator;

    public void InitializeAbilities(int MS , bool ai , List<Ability> abilities , PlayerController pc)
    {
        player = pc;
        maxStamina = MS;
        currentStamina = maxStamina;
        AI = ai;

        Abilities = abilities;
        availableAbilities = new HashSet<Ability>();

        while (availableAbilities.Count < 3)
        {
            availableAbilities.Add(Abilities[Random.Range(0, Abilities.Count)]);
        }

        generator = new AbilityGenerator();
        nextAbility = generator.GenerateAbility(Abilities, availableAbilities);

        if (!AI)
        {
            foreach(Ability ability in availableAbilities)
            {
                UIManager.Instance.UpdateAbilityTab(ability);
            }

            UIManager.Instance.UpdateNextAbilityImg(nextAbility.AbilitySprite);

            EventManager.updateStamina.Invoke(currentStamina);
        }

        else
        {
            StartCoroutine("AIBattle");
        }

        StartCoroutine("RegenerateStamina");
    }

    //Regenerates Stamina
    IEnumerator RegenerateStamina()
    {
        while (true)
        {
            yield return new WaitUntil(() => currentStamina < maxStamina);
            yield return new WaitForSeconds(0.75f);
            AddStamina();

            if (!AI)
            {
                EventManager.updateStamina.Invoke(currentStamina);
            }
        }
    }

    public void AddStamina(int stamina = 1)
    {
        currentStamina += stamina;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    IEnumerator AIBattle()
    {
        //Copy the available abilities to the array
        Ability[] currentAbilities = new Ability[availableAbilities.Count];
        availableAbilities.CopyTo(currentAbilities);
        nextAbility = generator.GenerateAbility(Abilities, availableAbilities);

        //Offset the start of the battle
        yield return new WaitForSeconds(1);

        while (true)
        {
            //Randomly choose the index of the current ability
            int index = Random.Range(0, currentAbilities.Length);
            Ability currentAbility = currentAbilities[index];

            //Wait until the player can execute the ability
            yield return new WaitUntil(() => currentAbility.StaminaCost <= currentStamina && player.CanShoot);

            //Fire the ability and update it in the list to the next ability
            ExecuteAbility(currentAbility);
            Debug.Log(currentAbility.AbilityName);
            availableAbilities.Add(nextAbility);
            currentAbilities[index] = nextAbility;
            nextAbility = generator.GenerateAbility(Abilities, availableAbilities);
        }
    }

    public void ExecuteAbility(Ability ability)
    {
        availableAbilities.Remove(ability);
        currentStamina -= ability.StaminaCost;
        ability.TriggerAbility(player);

        if (!AI)
        {
            UIManager.Instance.UpdateAbilityTab(nextAbility);
            availableAbilities.Add(nextAbility);
            nextAbility = generator.GenerateAbility(Abilities, availableAbilities);
            UIManager.Instance.UpdateNextAbilityImg(nextAbility.AbilitySprite);
            EventManager.updateStamina.Invoke(currentStamina);
        }
    }
}