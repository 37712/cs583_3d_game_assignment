using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inherits from Unit.cs
public class EnemyUnit : Unit
{
    /* Public Field*/
    public bool isDead = false,
                player_on_lock = false,
                finish_him = false;

    /* Private Field */
    private float decide;
    private Transform player_transform;
    private RaycastHit selected_on_click;

    /* Serialized Private Field */

    /********* ARMANDO COMMENT AND CODE *************/
    [SerializeField]
    private bool enemy_has_a_path = false;

    /******* ARMANDO COMMENT AND CODE END ***********/


    public Vector3 destination;
    public bool move_enemy = false;
    public float visibility_range;

    void Start()
    {
        //player_transform = BattleManager.Instance.playerPrefab.GetComponent<Unit>().transform;
    }

    // Update is called once per frame
    void Update()
    {

        track_path_movement(enemy_has_a_path);

        /* Keep enemy updated on player's position */
        //destination = BattleManager.PlayerUnit.GetComponent<Unit>().transform.position;

        /* check if unit is still alive */
        IsDead();
    }

    /* private methods */

    /*
     * @method: track_path_movement
     * @param: hasPath
     * notes: The enemy will follow a path assigned to them, but some units may not need a path, 
     *        so set the boolean "enemy_has_a_path" prior to start of scene!!!
     */
    private void track_path_movement(bool hasPath)
    {
        // here, the enemy shall go on set path?
    }


    /*
     * @method: check_proximity
     * notes: checks if player's unit is in range of the enemy itself.
     */
    public void check_proximity()
    {
        // Does the enemy see the player unit? If so, then lock on, and attack
        if (Vector3.Distance(player_transform.position, transform.position) <= attack_range)
        {
            // lock on player!
            player_on_lock = true;
        }
    }

    /*
     * @method: enemy_death
     * notes: destroys the enemy object altogether if enemy is no longer alive
     */
    private void IsDead()
    {
        if (isDead && finish_him)
        {
            Destroy(gameObject);
        }
    }
}
