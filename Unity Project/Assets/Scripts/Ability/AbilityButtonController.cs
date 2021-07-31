using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonController : MonoBehaviour
{
    public Image image;
    public Button button;

    Ability ability;
    string abilityDescription;
    bool enoughStamina;
    bool canShoot;

    private void OnEnable()
    {
        EventManager.updateStamina += CheckStamina;
        EventManager.canShoot += CanShoot;
    }

    private void OnDisable()
    {
        EventManager.updateStamina -= CheckStamina;
        EventManager.canShoot -= CanShoot;
    }

    public void Initialize(Ability ability, bool cs)
    {
        //Add ability to tab
        image.sprite = ability.AbilitySprite;
        abilityDescription = ability.Description();
        this.ability = ability;
        canShoot = cs;
    }

    //Check if enough ability to execute
    void CheckStamina(int stamina)
    {
        enoughStamina = stamina >= ability.StaminaCost;
        button.interactable = enoughStamina && canShoot;
    }

    void CanShoot(bool shoot)
    {
        canShoot = shoot;
        button.interactable = enoughStamina && canShoot;
    }

    public void ButtonPressed()
    {
        UIManager.Instance.AbilitySelected(ability);
        Destroy(this.gameObject);
    }

    public void PointerEnter()
    {
        UIManager.Instance.EnableDescription(abilityDescription);
    }

    public void PointerExit()
    {
        UIManager.Instance.DisableDescription();
    }
}