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
    public GameObject Game_Controller;
    public GameObject Turn_status;
    public GameObject victory_panel;
    public GameObject louser_panel;

    private void Start()
    {

    }

    void Update()
    {
        BattleState state = Game_Controller.GetComponent<BattleManager>().state;
        switch(state)
        {
            case BattleState.PLAYERTURN:
                Turn_status.GetComponent<Text>().text = "PLAYERTURN";
                break;
            case BattleState.ENEMYTURN:
                Turn_status.GetComponent<Text>().text = "ENEMYTURN";
                break;
            case BattleState.PLAYERMOVEMENT:
                Turn_status.GetComponent<Text>().text = "PLAYERMOVEMENT";
                break;
            case BattleState.ENEMYMOVEMENT:
                Turn_status.GetComponent<Text>().text = "ENEMYMOVEMENT";
                break;
            case BattleState.WON:
                Turn_status.GetComponent<Text>().text = "WON";
                break;
            case BattleState.LOST:
                Turn_status.GetComponent<Text>().text = "LOST";
                break;
        } 
    }
}
