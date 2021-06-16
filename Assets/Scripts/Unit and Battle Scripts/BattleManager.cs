using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState {PLAYERTURN, ENEMYTURN, WON, LOST, PLAYERMOVEMENT, ENEMYMOVEMENT };

public class BattleManager : MonoBehaviour
{
    [SerializeField]

    public BatteryState state;
    public GameObject Player; // current player
    public GameObject Enemy; // current enemy

    // ray casting for mouse point and clic
    private RaycastHit hit;
    private Ray hit_on_click;


    public Unit playerUnit;
    public Unit[] enemyUnit;

    private void Awake()
    {
        // initialize scenario
        SetupBattle();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        state = BattleState.PLAYERTURN;
    }

    /*
     * @coroutine: PlayerAttack
     * notes: player inflicts damage on a unit
     */
    IEnumerator PlayerAttack(float unit_distances, Transform enemy_transform)
    {
        // is the attack in range of the player?
        if (unit_distances <= attack_range_of_player)
        {
            // flag if the unit has finally died
            if (enemy_transform.GetComponent<EnemyUnit>().TakeDamage(playerUnit.damage))
            {
                enemy_transform.GetComponent<EnemyUnit>().isDead = true;

                // do a lookup of who died
                for (int i = 0; i < enemyUnit.Length; i++)
                    if (!do_not_access_this_enemy[i])
                        if (((EnemyUnit)enemyUnit[i]).isDead)
                        {
                            do_not_access_this_enemy[i] = true;
                            ((EnemyUnit)enemyUnit[i]).finish_him = true;
                        }
            }

        }

        yield return new WaitForSeconds(2f);
        //can change wait time 

        //Checks Enemy state, if dead ends battle
        //Else goes into BattleState.ENEMYTURN which sets it to enemy's turn
        if (UnitManager.Instance.all_enemies_dead())
        {
            state = BattleState.WON;
            EndBattle();
        }

        else
        {
            state = BattleState.ENEMYTURN;
            for (int i = 0; i < enemyUnit.Length; i++)
            {
                if (!do_not_access_this_enemy[i] && state != BattleState.LOST)
                    EnemyTurn(enemyUnit[i]);
            }

            if (state != BattleState.LOST)
                state = BattleState.PLAYERTURN;
        }
        //yield return new WaitForSeconds(2f);
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
        if ((temp = Vector3.Distance(each_enemy.transform.position, playerUnit.transform.position)) <= ((EnemyUnit)each_enemy).visibility_range)
        {
            ((EnemyUnit)each_enemy).move_enemy = true;
        }


        //Give the enemies time to travel
        //yield return new WaitForSeconds(2f);

        //check player proximity
        ((EnemyUnit)each_enemy).check_proximity();

        //finally, inflict damage if in proximity
        enemy_lock = ((EnemyUnit)each_enemy).player_on_lock;

        if (Vector3.Distance(each_enemy.transform.position, playerUnit.transform.position) <= ((EnemyUnit)each_enemy).attack_range && enemy_lock)
        {
            isDead = playerUnit.TakeDamage(each_enemy.damage);
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
     * @coroutine: PlayerHeal
     * notes: upon call, the player loses a turn in exchange for healing
     */
    IEnumerator PlayerHeal()
    {
        //Example of Player Skill on a button etc, heal used for example
        //Look in the Unit script to start the .commands
        playerUnit.Heal(5);

        //Can change HUD, etc here

        yield return new WaitForSeconds(2f);
        //Can change wait times here if you want 

        state = BattleState.ENEMYTURN;
        //Can change to PLAYERTURN here if you want to allow multiple moves in a turn
        for (int i = 0; i < enemyUnit.Length; i++)
        {
            if (!do_not_access_this_enemy[i] && state != BattleState.LOST)
                StartCoroutine(EnemyTurn(enemyUnit[i]));
        }

        if (state != BattleState.LOST)
            state = BattleState.PLAYERTURN;


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
                StartCoroutine(PlayerAttack(Vector3.Distance(hit.transform.position, playerUnit.transform.position), hit.transform));
            }
        }
    }

    /*
     * @method: OnHeal
     * note: starts PlayerHeal coroutine; player's turn must be activated prior to call;
     */
    public void OnHeal()
    {
        //Prevents the player from spamming heals when not their turn
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }

    /*
     * @method: playerMovement
     * notes: toggles the PLAYERMOVEMENT state
     */
    public void playerMovement()
    {
        state = BattleState.PLAYERMOVEMENT;
    }

    public void make_enemy_turn()
    {
        if (state == BattleState.ENEMYTURN)
            for (int i = 0; i < enemyUnit.Length; i++)
            {
                if (!do_not_access_this_enemy[i] && state != BattleState.LOST)
                    StartCoroutine(EnemyTurn(enemyUnit[i]));
            }

        if (state != BattleState.LOST && state == BattleState.ENEMYTURN)
            state = BattleState.PLAYERTURN;
    }

    public bool all_enemies_dead()
    {
        // run through entire array of enemy units, and if
        // one of them IS NOT DEAD, then return false.
        for (int i = 0; i < BattleManager.Instance.enemyUnit.Length; i++)
            if (!((EnemyUnit) BattleManager.Instance.enemyUnit[i]).isDead)
                return false;

        return true;
    }

    
}

