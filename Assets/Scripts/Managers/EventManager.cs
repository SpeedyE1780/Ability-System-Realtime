using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void UpdateStamina(int stamina);
    public static UpdateStamina updateStamina;

    public delegate void CanShoot(bool shoot);
    public static CanShoot canShoot;

    public delegate void UpdateNextAbility(Sprite ability);
    public static UpdateNextAbility nextAbility;

    public delegate void GameOver(GameObject winner);
    public static GameOver gameOver;
}