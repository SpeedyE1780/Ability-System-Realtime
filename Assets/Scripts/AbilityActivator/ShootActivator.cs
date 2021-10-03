using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Activator/Shoot")]
public class ShootActivator : AbilityActivator
{
    public ShootingParams shootingParams;
    public override string Description()
    {
        StringBuilder description = new StringBuilder();
        description.Append($"Damage: {shootingParams.Damage}\n");
        return description.ToString();
    }

    public override void ActivateAbility(PlayerController player)
    {
        player.Shoot(shootingParams);
    }
}

[System.Serializable]
public class ShootingParams
{
    public int Damage;
    public GameObject Shard;
    public string AnimationBoolean;
}