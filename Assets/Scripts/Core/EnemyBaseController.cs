using UnityEngine;
using System.Collections;

public class EnemyBaseController : MonoBehaviour, IEntity
{

    // Entity Implemenetation "properties"
    public GameObject m_Root { get; set; }
    private GameObject Player;
    private Vector2 DirectionToPlayer;
    public float EnemyAttackRange, EnemyHealth, EnemyAttackCoolDown, EnemyDetectionRange;
    private bool EnemyIsInAttackRange, EnemyIsInDetectionRange, EnemyIsDead;

    // Use this for initialization of variables.
    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        // if the things are not set in the editor window, set them default here.
        if (EnemyAttackRange == null)
        {
            EnemyAttackRange = 10f;
        }
        if (EnemyHealth == null)
        {
            EnemyHealth = 10f;
        }
        if (EnemyAttackCoolDown == null)
        {
            EnemyAttackCoolDown = 3f;
        }
        if (EnemyDetectionRange == null)
        {
            EnemyHealth = 30f;
        }
        // Set the Booleans
        if (EnemyIsInAttackRange == null)
        {
            EnemyIsInAttackRange = false;
        }
        if (EnemyIsInDetectionRange == null)
        {
            EnemyIsInDetectionRange = false;
        }
        if (EnemyIsDead == null)
        {
            EnemyIsDead = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        DoAction();
    }

    // use this to tell the monster which direction the player is.
    void FindPlayer()
    {
        // If the player doesn't exist, don't do anything else
        if (Player == null)
            Debug.Log("Player is null"); return;

        DirectionToPlayer = Player.transform.position - m_Root.transform.position;

        if (DirectionToPlayer.magnitude < EnemyDetectionRange)
        {
            EnemyIsInDetectionRange = true;
        }
        else
        {
            EnemyIsInDetectionRange = false;
        }

        // Attack Range Check
        if (DirectionToPlayer.magnitude < EnemyAttackRange)
        {
            EnemyIsInAttackRange = true;
        }
        else
        {
            EnemyIsInAttackRange = false;
        }
    }


    //Below this is IEntity related code

    public bool isCombatant()
    {
        return true;
        // the enemies are almost always combatants.
    }

    public bool isHostile(IEntity toThisEntity)
    {
        // is almost always true. 
        if (toThisEntity.m_Root.CompareTag("Player"))
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

    public void DoAction()
    {
        //for the enemy this is probably where attacking goes.
        if (EnemyIsInDetectionRange == true && EnemyIsInAttackRange == false)
        {
            // Move Code goes here
            // Should Move towards the player until it is in attack range
            Debug.Log(this + " is in Detection Range of Player");
        }
        else if (EnemyIsInAttackRange == true)
        {
            // this is where the attack code goes
            // This doesn't happen if the enemy is too far away.
            Debug.Log(this + " is in Attack Range of Player");
        }
    }

    public void Die()
    {
        //enemy does this when it perishes.
        EnemyIsDead = true;
        // Do other stuff relating to it dying.
    }
}
