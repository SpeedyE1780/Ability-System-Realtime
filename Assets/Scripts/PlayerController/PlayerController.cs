using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Health & Shield")]
    public RectTransform HealthBar;
    public RectTransform ShieldBar;
    public ParticleSystem HealthParticles;

    [Header("Abilities")]
    public List<Ability> Abilities;
    public Transform SpawnPoint;
    public PlayerController Enemy;

    [Header("Player Characteristics")]
    public int MaxStamina;
    public int MaxHealth;
    public GameObject Character;

    [Header("Controllers")]
    public PlayerHealthController healthController;
    public PlayerAbilityController abilityController;
    public Animator animator;

    [Header("Materials")]
    public Renderer PlayerRenderer;
    public Material PlayerMaterial;
    public Material PoisonMaterial;
    public Material ArmorMaterial;

    [Header("Particle Systems")] 
    public ParticleSystem HealthRegenerationParticles;
    public ParticleSystem PoisonParticles;
    public ParticleSystem ShieldParticles;
    public ParticleSystem StaminaDrainParticles;
    public ParticleSystem LifeStealParticles;

    [HideInInspector]
    public bool CanShoot;

    bool AI;
    ShootingParams nextShot;
    bool gameOver;

    //Initialize the player
    public void Initialize()
    {
        Character.SetActive(true);
        GetComponent<Collider>().enabled = true;
        CanShoot = true;
        gameOver = false;
        AI = true;

        if (gameObject.CompareTag("MainPlayer"))
        {
            AI = false;
            UIManager.Instance.SetPlayer(this);
        }

        healthController.InitializeHealth(MaxHealth , HealthBar , ShieldBar , HealthRegenerationParticles , PoisonParticles , this);
        abilityController.InitializeAbilities(MaxStamina , AI , Abilities , this);  
    }

    public void Shoot(ShootingParams shooting)
    {
        animator.SetBool(shooting.AnimationBoolean, true);
        nextShot = shooting;
        CanShoot = false;

        if (!AI)
        {
            EventManager.canShoot.Invoke(CanShoot);
        }
    }

    //Shoot the shard called from the shooting animation
    public void ShootShard()
    {
        animator.SetBool(nextShot.AnimationBoolean, false);

        if (!gameOver)
        {
            GameObject e = Instantiate(nextShot.Shard);
            e.GetComponent<Bullet>().InitializeBullet(Enemy, nextShot.Damage, SpawnPoint.position);
            CanShoot = true;

            if (!AI)
            {
                EventManager.canShoot.Invoke(CanShoot);
            }
        }
    }

    //Activate the player's shield
    public void ActivateShield(int hits)
    {
        ShieldParticles.Play();
        healthController.ActivateShield(hits);
    }

    public void ActivateArmor(float duration, int damagaTaken)
    {
        PlayerRenderer.material = ArmorMaterial;
        healthController.ActivateArmor(duration, damagaTaken);
    }

    //Add health to the player
    public void AddHealth(int h)
    {
        HealthParticles.Play();
        healthController.AddHealth(h);
    }

    public void StealHealth(int h)
    {
        LifeStealParticles.Play();
        healthController.TakeDamage(h);
    }

    public void ActivatePoison(int damage , int hits , float rate)
    {
        healthController.ActivatePoison(damage, hits, rate);
    }

    public void ActivateStaminaDrain(int stamina)
    {
        StaminaDrainParticles.Play();
        abilityController.AddStamina(stamina);
    }

    //Start regenerating the player's health
    public void RegenerateHealth(int rDuration , int rHealth , float rRate)
    {
        healthController.RegenerateHealth(rDuration, rHealth , rRate);
    }

    //Reduce the player's health
    public void TakeDamage(int damage)
    {
        healthController.TakeDamage(damage);
    }

    public void PlayerDied()
    {
        Character.SetActive(false);
        GetComponent<Collider>().enabled = false;
        EventManager.gameOver.Invoke(Enemy.gameObject);
    }

    public void SwitchPoisonMaterial(bool poisoned)
    {
        if(poisoned)
        {
            PlayerRenderer.material = PoisonMaterial;
        }

        else
        {
            PlayerRenderer.material = PlayerMaterial;
        }
    }

    public void SwitchArmorMaterial(bool armorActive)
    {
        if (armorActive)
        {
            PlayerRenderer.material = ArmorMaterial;
        }

        else
        {
            PlayerRenderer.material = PlayerMaterial;
        }
    }

    //Stop all the coroutines of the player
    public void StopPlayer()
    {
        gameOver = true;
        healthController.StopAllCoroutines();
        abilityController.StopAllCoroutines();
        animator.Play("Idle");
    }
}