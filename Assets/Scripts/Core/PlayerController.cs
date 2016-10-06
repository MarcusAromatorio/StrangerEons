using UnityEngine;
using System.Collections;
/*
    The Player Controller script defines how the user is able to influence player actions
    Such actions are only available to the user when the player is controllable
*/
public class PlayerController : MonoBehaviour, IEntity {

    public bool m_IsControllable { get; set; }
    public PlayerStateManager m_StateManager;

    // Entity Implemenetation "properties"
    public GameObject m_Root { get; set; }
    // public IAction m_CurrentAction { get; set; } 

	// Use this for initialization
	void Start () {
        m_Root = gameObject;
        m_IsControllable = true;
	}
	
	// Update is called once per frame
    // Each frame, check if player state allow control
    // If control is allowed, determine which action will be taken
    // Afterward, execute the determined action
	void Update () {
        m_IsControllable = m_StateManager.CanBeControlled();
        if (m_IsControllable)
        {
            //m_CurrentAction = 
            DetermineActionFromInput();
        }
	}

    // Method to read input from player and translate to an action to take
    public void DetermineActionFromInput()
    {
        
    }

    // IEntity implementation for the player
    public void DoAction()
    {

    }

    // IEntity implementations of isCombatatant(), isHostile (IEntity), and isMortal()
    // The player is considered to always be capable of attacking and therefore of combat
    public bool isCombatant()
    {
        return true;
    }

    public bool isHostile(IEntity toThisEntity)
    {
        if (toThisEntity.m_Root.CompareTag("Enemy"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isMortal()
    {
        return true;
    }
}
