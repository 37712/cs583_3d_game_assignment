using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState {PLAYERTURN, ENEMYTURN, PLAYERMOVEMENT, ENEMYMOVEMENT, WON, LOST};

public class BattleManager : MonoBehaviour
{
    public BattleState state;
    public GameObject PlayerUnit; // current player
    public GameObject Enemy; // current enemy
    public GameObject[] EnemyList;
    public GameObject UI_Controller;

    // ray casting for mouse point and clic
    private RaycastHit hit;
    private Ray hit_on_click;

    void Start()
    {
        //populate enemy list and enemey game object
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case BattleState.PLAYERTURN:
                print("PLAYERTURN");
                // enable player buttons
                UI_Controller.GetComponent<UI_Control>().player_turn();

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
            PlayerUnit.GetComponent<Unit>().TakeDamage(each_enemy.damage);
            ((EnemyUnit)each_enemy).player_on_lock = false;

            // reset lock
            enemy_lock = false;
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
     * notes: initiates the PlayerAttack
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
     * note: starts PlayerHeal coroutine; player's turn must be activated prior to call;
     */
    public void PlayerHeal()
    {
        // full Heal of player

        // change state to enemy turn
    }

    public void IsPlayerDead()
    {
        state = BattleState.LOST;
    }
    public void all_enemies_dead()
    {
        // run through entire array of enemy units, and if
        // one of them IS NOT DEAD, then return false.
        state = BattleState.WON;
    }
}