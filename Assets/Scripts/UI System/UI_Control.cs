using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/******** THIS SCRIPT DEPENDS ON THE BATTLE MANAGER!!!!! *********/


/********************************************************************/
/* The goal of this script is to be a UI control at the global end. */
/* In other words, where the player can make a choice in what their */
/* unit shall do prior to engagement of combat.                     */
/*                                                                  */
/* This script shall have some static methods that will allow it to */
/* have some control from the outside, or at least from the Battle  */
/* System.                                                          */
/* This script shall be a singleton to avoid duplications of it     */
/********************************************************************/


public class UI_Control : MonoBehaviour
{
    public GameObject Game_Controller;
    public Button move_button,
                   heal_button,
                   end_turn_button,
                   cancel_unit_button;

    void Awake()
    {

    }

    private void Start()
    {
        /*********************** ARMANDO COMMENT *************************/
        
        // Kent, since you are the programmer for the battle manager, I 
        // recommed you tamper the with the next 4 lines below to 
        // accommodate your programming of the manager.

        /********************* ARMANDO END COMMENT ***********************/
        //move_button.onClick.AddListener(BattleManager.Instance.playerMovement);
        //heal_button.onClick.AddListener(BattleManager.Instance.OnHeal);
        end_turn_button.onClick.AddListener(end_turn_method);
        cancel_unit_button.onClick.AddListener(cancel_action_method);

        /* Start off with all the buttons disabled until the battle manager enables player turn */
        move_button.interactable = false;
        heal_button.interactable = false;
        end_turn_button.interactable = false;
        cancel_unit_button.interactable = false;
    }

     // Update is called once per frame
    void Update()
    {
        player_turn_ready();
        enemy_turn_active();
    }

    /* Private methods that the buttons will activate */

    /*
     * @method: end_turn_method
     * notes: allows enemy to have a turn
     */
    private void end_turn_method()
    {
        Game_Controller.GetComponent<BattleManager>().state = BattleState.ENEMYTURN;
    }

    /*
     * @method: cancel_action_method
     * notes: cancels any selected action such as movement or attack PRIOR TO FINAL EXECUTION (ask Armando for information!)
     */
    private void cancel_action_method()
    {
        print("method not written yet");
    }

    /************* end of methods *********************/


   

    /* @method: player_turn_ready
     * note: makes the buttons active when it's the player's turn
     */
    private void player_turn_ready()
    {
        // enable the buttons when it's the player's turn
        if (Game_Controller.GetComponent<BattleManager>().state == BattleState.PLAYERTURN)
        {
            move_button.interactable = true;
            heal_button.interactable = true;
            cancel_unit_button.interactable = false;
            end_turn_button.interactable = true;
        }

        // if the movement button was toggled, give the player a chance to cancel
        if(Game_Controller.GetComponent<BattleManager>().state == BattleState.PLAYERMOVEMENT)
        {
            move_button.interactable = false;
            cancel_unit_button.interactable = true;
        }
    }

    /*
     * @method: enemy_turn_active
     * Notes, disable buttons while it is the enemy's turn
     */
    private void enemy_turn_active()
    {
        if(Game_Controller.GetComponent<BattleManager>().state == BattleState.ENEMYTURN)
        {
            move_button.interactable = false;
            heal_button.interactable = false;
            cancel_unit_button.interactable = false;
            end_turn_button.interactable = false;
        }
    }
}
