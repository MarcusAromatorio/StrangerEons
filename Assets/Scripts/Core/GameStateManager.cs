using UnityEngine;
using System.Collections;

// Typical large-scale game states where "idling" is normal gameplay
public enum GameState
{
    idling,
    initializing,
    paused,
    playerDeath,
    victoryAchieved,
    saving,
    exiting
}
/*
    The Game State Manager class handles the large scale game transitions
    Transitions are dependent on in game events such as Initialize, Pausing, 
    Player dying, Boss encounter, level completion, and Exiting or Loading different scenes
*/
public class GameStateManager : MonoBehaviour {

    [SerializeField]
    private GameState m_State { get; set; }

	// Use this for initialization
	void Start () {
        m_State = GameState.initializing;
	}
	
	// Update is called once per frame
	void Update () {
	    switch (m_State)
        {
            case GameState.idling:
                break;

            case GameState.initializing:
                break;

            case GameState.paused:
                break;

            case GameState.playerDeath:
                break;

            case GameState.victoryAchieved:
                break;

            case GameState.saving:
                break;

            case GameState.exiting:
                break;

            default:
                break;
        }
	}
}
