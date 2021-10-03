using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform AbilityTab;
    public Text StaminaText;
    public Text AbilityDescription;
    public Image NextAbility;
    public GameObject AbilityButton;
    public GameObject EndScreen;
    public Text GameWinnerText;
    PlayerController player;
    //Singleton
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    private void OnEnable()
    {
        if(_instance == null)
        {
            _instance = this;
        }

        else
        {
            Destroy(this);
        }

        EventManager.updateStamina += UpdateStaminaTxt;
        EventManager.nextAbility += UpdateNextAbilityImg;
    }

    private void OnDisable()
    {
        EventManager.updateStamina -= UpdateStaminaTxt;
        EventManager.nextAbility -= UpdateNextAbilityImg;
    }

    public void AbilitySelected(Ability ability)
    {
        player.abilityController.ExecuteAbility(ability);
    }

    //Set the current player
    public void SetPlayer(PlayerController controller)
    {
        player = controller;
    }

    //Add the current player's ability to the tab
    public void UpdateAbilityTab(Ability ability)
    {
        GameObject abilityButton = Instantiate(AbilityButton, AbilityTab);
        abilityButton.GetComponent<AbilityButtonController>().Initialize(ability , player.CanShoot);
    }

    //Remove all abilities from the ability tab
    public void EmptyAbilityTab()
    {
        foreach(Transform child in AbilityTab)
        {
            Destroy(child.gameObject);
        }
    }

    //Update the Stamina Text
    public void UpdateStaminaTxt(int stamina)
    {
        StaminaText.text = $"Stamina: {stamina}";
    }

    //Update the next ability
    public void UpdateNextAbilityImg(Sprite ability)
    {
        NextAbility.sprite = ability;
    }

    //Show the game over screen and the winner
    public void GameOverScreen(string winner)
    {
        ToggleEndScreen();
        GameWinnerText.text = $"{winner} Wins";
    }

    public void ToggleEndScreen()
    {
        EndScreen.SetActive(!EndScreen.activeSelf);
    }

    //Show the ability description
    public void EnableDescription(string desc)
    {
        AbilityDescription.text = desc;
        AbilityDescription.gameObject.SetActive(true);
    }

    //Hide the ability description
    public void DisableDescription()
    {
        AbilityDescription.gameObject.SetActive(false);
    }
}