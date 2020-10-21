using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/***********************************************************************/
/* The script shall be used as a means of text update.  This should be */
/* implemetable anywhere in the game that takes advantage of the text  */
/* objects.                                                            */
/***********************************************************************/

public class GUI_Text_Updater : MonoBehaviour
{
    /* private serialized fields */
    [SerializeField]
    private Text unit_name,
                 health,
                 movement_count,    // why do we need a movement count
                 damage,            // why do we need to display damage
                 score,
                 turn_status,
                 victory;

    [SerializeField]
    private GameObject victory_panel;

    [SerializeField]
    private Unit player;

    /* public static fields */
    public static int movementCount { get; set; }

    private void Start()
    {
        victory_panel.SetActive(false);
    }

    void OnGUI()
    {
        set_text_and_image();
        set_turn_status();
    }


    /*
     * @method: set_text_and_image
     * notes: this method is called in the OnGUI method to update the text in the "player HUD"
     */
    private void set_text_and_image()
    {
        unit_name.text = "Player Name: " + player.unitName;
        damage.text = "Damage: " + player.damage.ToString();
        health.text = "Health: " + player.currentHP.ToString() + "/" + player.maxHP.ToString();
        movement_count.text = "Moves Left: " + movementCount.ToString();
        score.text = "Score: " + player.score.ToString();
    }

    /*
     * @method: set_turn_status
     * notes: tells the player who's turn it is
     */
    private void set_turn_status()
    {
        if (BattleManager.state == BattleState.PLAYERMOVEMENT || BattleManager.state == BattleState.PLAYERTURN)
            turn_status.text = "Player's Turn";
        else
            turn_status.text = "Please Wait...";

        if (BattleManager.state == BattleState.WON)
        {
            turn_status.text = "YOU WON!!!!!!!";
            victory_panel.SetActive(true);
            victory.text = "YOU WIN!!!";
        }
            

        if (BattleManager.state == BattleState.LOST)
        {
            turn_status.text = "You lost :(";
            victory_panel.SetActive(true);
            victory.text = "You lost!";
        }
            
    }

}
