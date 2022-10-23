using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        changeState(GameState.GenerateGrid);
    }

    public void changeState(GameState state) {
        gameState = state;

        switch (state) {
            case GameState.GenerateGrid: {
                    GridManager.instance.generateGrid();

                    break;
            }
                
            case GameState.SpawnCharacters: {
                    UnitManager.instance.spawnCharacter(Faction.Player);
                    UnitManager.instance.spawnCharacter(Faction.Enemy);
                    changeState(GameState.PlayerTurn);

                    break;
                }
                
            case GameState.PlayerTurn:
                break;

            case GameState.EnemyTurn:
                break;
        }
    }
}

public enum GameState {
    GenerateGrid = 0,
    SpawnCharacters = 1,
    PlayerTurn = 2,
    EnemyTurn = 3
}
