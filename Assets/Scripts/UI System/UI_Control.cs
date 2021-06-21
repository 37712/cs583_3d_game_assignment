using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Control : MonoBehaviour
{
    public GameObject Game_Controller;
    public GameObject PlayerUnit;
    public Button move_button,
                   heal_button,
                   cancel_button,
                   end_turn_button;
    
    // Start is called before the first frame update
    private void Start()
    {

    }

     // Update is called once per frame
    void Update()
    {

    }

    public void move_action()
    {
        PlayerUnit.GetComponent<BasicMovement>().onMouseMove();
    }

    public void heal_action()
    {
        print("method not written yet");
    }

    public void end_turn()
    {
        print("method not written yet");
    }

    public void cancel_action()
    {
        print("method not written yet");
    }

    /* @method: player_turn_ready
     * note: makes the buttons active when it's the player's turn
     */
    public void player_turn()
    {
        // enable the buttons when it's the player's turn
        if (Game_Controller.GetComponent<BattleManager>().state == BattleState.PLAYERTURN)
        {
            move_button.interactable = true;
            heal_button.interactable = true;
            cancel_button.interactable = true;
            end_turn_button.interactable = true;
        }

        // this part needs to be depricated
        // if the movement button was toggled, give the player a chance to cancel
        if(Game_Controller.GetComponent<BattleManager>().state == BattleState.PLAYERMOVEMENT)
        {
            move_button.interactable = false;
            cancel_button.interactable = true;
        }
    }

    /*
     * @method: enemy_turn_active
     * Notes, disable buttons while it is the enemy's turn
     */
    public void enemy_turn()
    {
        if(Game_Controller.GetComponent<BattleManager>().state == BattleState.ENEMYTURN)
        {
            move_button.interactable = false;
            heal_button.interactable = false;
            cancel_button.interactable = false;
            end_turn_button.interactable = false;
        }
    }
}
