using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    PlayerController player;

    int maxHealth;
    int currentHealth;
    RectTransform healthBar;

    int currentShieldRessistance;
    int maxShieldRessistance;
    RectTransform shieldBar;

    bool armorActive;
    int armorStrength;
    WaitForSeconds armorDuration;

    int regenDuration;
    int regenHealth;
    WaitForSeconds regenRate;
    ParticleSystem regenerationParticles;

    int poisonHits;
    int poisonDamage;
    WaitForSeconds poisonRate;
    ParticleSystem poisonParticles;


    public void InitializeHealth(int MH, RectTransform HB, RectTransform SB, ParticleSystem regenParticles, ParticleSystem poiParticles , PlayerController pc)
    {
        player = pc;
        maxHealth = MH;
        healthBar = HB;
        regenerationParticles = regenParticles;
        poisonParticles = poiParticles;
        HB.LookAt(Camera.main.transform);
        currentHealth = maxHealth;

        currentShieldRessistance = 0;
        maxShieldRessistance = 0;
        shieldBar = SB;
        SB.LookAt(Camera.main.transform);

        UpdateHealthBar();
        UpdateShieldBar();
    }

    //Update the health bar
    void UpdateHealthBar()
    {
        var temp = healthBar.sizeDelta;
        temp.x = (float)currentHealth / maxHealth;
        healthBar.sizeDelta = temp;
    }

    //Update the shield bar
    void UpdateShieldBar()
    {
        var temp = shieldBar.sizeDelta;
        temp.x = (float)currentShieldRessistance / maxShieldRessistance;
        shieldBar.sizeDelta = temp;
    }

    public void TakeDamage(int damage)
    {
        if (currentShieldRessistance > 0)
        {
            currentShieldRessistance--;
            UpdateShieldBar();
        }

        else if(armorActive)
        {
            currentHealth -= armorStrength / 100 * damage;

            UpdateHealthBar();

            //Player died destroy it
            if (currentHealth <= 0)
            {
                player.PlayerDied();
            }
        }

        else
        {
            currentHealth -= damage;
            UpdateHealthBar();

            //Player died destroy it
            if (currentHealth <= 0)
            {
                player.PlayerDied();
            }
        }
    }

    public void ActivateShield(int hits)
    {
        maxShieldRessistance = hits;
        currentShieldRessistance = maxShieldRessistance;
        UpdateShieldBar();
    }

    public void ActivateArmor(float duration , int damageTaken)
    {
        armorDuration = new WaitForSeconds(duration);
        armorStrength = damageTaken;
        StartCoroutine("StartArmor");
    }

    IEnumerator StartArmor()
    {
        armorActive = true;
        player.SwitchArmorMaterial(armorActive);
        yield return armorDuration;
        armorActive = false;
        player.SwitchArmorMaterial(armorActive);
    }

    //Add health to the player
    public void AddHealth(int h)
    {
        currentHealth += h;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    //Start regenerating the player's health
    public void RegenerateHealth(int rDuration, int rHealth, float rRate)
    {
        regenDuration = rDuration;
        regenHealth = rHealth;
        regenRate = new WaitForSeconds(rRate);
        StopCoroutine("HealthRegenerator");
        StartCoroutine("HealthRegenerator");
    }

    IEnumerator HealthRegenerator()
    {
        while (regenDuration > 0)
        {
            AddHealth(regenHealth);
            regenerationParticles.Play();
            regenDuration--;
            yield return regenRate;
        }
    }

    public void ActivatePoison(int pDamage, int pHits, float pRate)
    {
        poisonDamage = pDamage;
        poisonHits = pHits;
        poisonRate = new WaitForSeconds(pRate);
        StartCoroutine("TakePoisonDamage");
    }

    IEnumerator TakePoisonDamage()
    {
        player.SwitchPoisonMaterial(true);

        while (poisonHits > 0)
        {
            TakeDamage(poisonDamage);
            poisonParticles.Play();
            poisonHits--;
            yield return poisonRate;
        }

        player.SwitchPoisonMaterial(false);
    }
}