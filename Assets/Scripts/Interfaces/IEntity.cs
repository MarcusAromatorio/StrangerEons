using UnityEngine;
using System.Collections;
/*
    IEntity is the interface that defines a dynamic actor in the game
    Entities are things that have their entire structure somehow controlled at runtime
    Entities must be controlled through the IAction interface, where their main choices
    for action are provided by their associated state managers
*/
public interface IEntity {
    // Properties that must have accessors and mutators
    // IAction m_CurrentAction { get; set; } COMMENTED OUT FOR REVIEW

    // boolean properties that depend on entity implementation
    bool isCombatant();
    bool isHostile(IEntity toThisEntity);
    bool isMortal();

    // Main method in which one "does" an action
    void DoAction();

    // Method in which the entity dies, or does it's own form of dying.
    void Die();

    // What gameobject owns this entity implementation
    GameObject m_Root { get; set; }
	
}
