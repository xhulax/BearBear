using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager game;
    [SerializeField]

    public GameState State;


    private void Awake()
    {
        game = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.DreamState);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.DreamState:
                print("dream");
                break;
            case GameState.NightmareState:
                print("nightmare");
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

        }
    }
}
public enum GameState
{
    DreamState,
    NightmareState
}
