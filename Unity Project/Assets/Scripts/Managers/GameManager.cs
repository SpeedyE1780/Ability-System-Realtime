using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public List<PlayerController> Players;

    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        else
        {
            Destroy(this);
        }

        EventManager.gameOver += GameOver;
    }

    private void OnDisable()
    {
        EventManager.gameOver -= GameOver;    
    }

    private void Start()
    {
        Invoke("InitializePlayers", Time.deltaTime);
    }

    public void InitializePlayers()
    {
        UIManager.Instance.EmptyAbilityTab();
        foreach (PlayerController player in Players)
        {
            player.Initialize();
        }
    }

    //Stop the player and show the winner
    void GameOver(GameObject winner)
    {
        UIManager.Instance.GameOverScreen(winner.name);

        foreach (PlayerController player in Players)
        {
            player.StopPlayer();
        }
    }
}