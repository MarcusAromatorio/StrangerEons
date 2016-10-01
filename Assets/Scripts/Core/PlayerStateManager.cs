using UnityEngine;
using System.Collections;
// General player states that are separated into an enumeration
public enum PlayerState
{
    idling,
    walking,
    crouching,
    crawling,
    attacking,
    crouchattacking,
    jumping,
    jumpattacking
}

/*
    This class passively manages how the player changes state
    The player's state is largely dependent on the cooperation with the PlayerController class
    The state manager does not directly intervene in player action as such interventions are 
    mainly under the jurisdiction of interface implementation
    (I.e. the player dies not because its state manager reads health is zero, but how
    the player implements the IKillable interface)
*/
public class PlayerStateManager : MonoBehaviour {

    [SerializeField]
    private PlayerState m_State { get; set; }

	// Use this for initialization
	void Start () {
        m_State = PlayerState.idling;
	}
	
}
