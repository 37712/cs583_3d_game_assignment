using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState {PLAYERTURN, ENEMYTURN, PLAYERMOVEMENT, ENEMYMOVEMENT, WON, LOST};

public class BattleManager : MonoBehaviour
{
    public BattleState state;
    public GameObject Player; // current player
    public GameObject Enemy; // current enemy

    // ray casting for mouse point and clic
    private RaycastHit hit;
    private Ray hit_on_click;


    public Unit PlayerUnit;
    public Unit[] EnemyUnit;

    private void Awake()
    {
        state = BattleState.PLAYERTURN;
        // initialize scenario
        //SetupBattle();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case BattleState.PLAYERTURN:
                print("PLAYERTURN");

                // check if all enemies dead

                break;

            case BattleState.ENEMYTURN:
                print("ENEMYTURN");

                // check if player is dead

                break;
        }
    }

    /*
     * @coroutine: PlayerAttack
     * notes: player inflicts damage on a unit
     */
    public void PlayerAttack( GameObject DefendingUnit)
    {
        // do attack

        // change state to enemy turn
    }

    /*
     * @coroutine: EnemyTurn
     * notes: initiated after unit makes a turn
     * update(3:53pm 9 Dec 2019): I'll have each enemy take this coroutine!
     */
    IEnumerator EnemyTurn(Unit each_enemy)
    {
        bool isDead = false;
        bool enemy_lock = false;
        float temp;

        //Can set text or something at the start to indicate enemy turn


        //Pauses so that Enemy does not move immediately
        //yield return new WaitForSeconds(2f);

        //Can change wait time to see how long each turn takes to start...etc

        /******************** ARMANDO COMMENT AND CODE *******************/

        /* seek out the player! */
        if ((temp = Vector3.Distance(each_enemy.transform.position, PlayerUnit.transform.position)) <= ((EnemyUnit)each_enemy).visibility_range)
        {
            ((EnemyUnit)each_enemy).move_enemy = true;
        }


        //Give the enemies time to travel
        //yield return new WaitForSeconds(2f);

        //check player proximity
        ((EnemyUnit)each_enemy).check_proximity();

        //finally, inflict damage if in proximity
        enemy_lock = ((EnemyUnit)each_enemy).player_on_lock;

        if (Vector3.Distance(each_enemy.transform.position, PlayerUnit.transform.position) <= ((EnemyUnit)each_enemy).attack_range && enemy_lock)
        {
            isDead = PlayerUnit.TakeDamage(each_enemy.damage);
            ((EnemyUnit)each_enemy).player_on_lock = false;

            // reset lock
            enemy_lock = false;
        }

        /******************** ARMANDO COMMENT AND CODE END ***************/

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            // state = BattleState.PLAYERTURN;

        }

        yield return new WaitForSeconds(2f);

    }


    /*
     * @method: EndBattle
     * notes: called when either the player or enemy has lost their unit(s)
     */
    void EndBattle()
    {
        if (state == BattleState.WON)
            SceneManager.LoadScene("Winner Scene");

        else if (state == BattleState.LOST)
            SceneManager.LoadScene("loser Scene");
    }





    /*
     * @method: OnAttack
     * notes: initiates the PlayerAttack coroutine
     */
    public void OnAttack()
    {
        bool not_on_exclusion_zone = move_with_cursor.Instance.exclusion_zone_method();
        hit_on_click = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Prevents the player from spamming attacks when not their turn
        if (state != BattleState.PLAYERMOVEMENT)
        {
            return;
        }


        // if the player clicks on a unit
        if (Physics.Raycast(hit_on_click, out hit) && state == BattleState.PLAYERMOVEMENT && not_on_exclusion_zone && Input.GetMouseButtonDown(1))
        {
            if (hit.transform.tag == "enemy")
            {
                // start player attack on an enemy
            }
        }
    }

    /*
     * @method: OnHeal
     * note: starts PlayerHeal coroutine; player's turn must be activated prior to call;
     */
    public void PlayerHeal()
    {
        // full Heal of player

        // change state to enemy turn
    }

    /*
     * @method: playerMovement
     * notes: toggles the PLAYERMOVEMENT state
     */
    public void playerMovement()
    {
        state = BattleState.PLAYERMOVEMENT;
    }

    public bool IsPlayerDead()
    {
        return false;
    }
    public bool all_enemies_dead()
    {
        // run through entire array of enemy units, and if
        // one of them IS NOT DEAD, then return false.
        return false;
    }

    
}

