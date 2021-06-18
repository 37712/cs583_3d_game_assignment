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
    public GameObject GameController;
    public GameObject TurnStatus;
    public GameObject victory_panel;
    public GameObject louser_panel;
    private void Start()
    {

    }

    void Update()
    {
        BattleState state = GameController.GetComponent<BattleManager>().state;
        switch(state)
        {
            case BattleState.PLAYERTURN:
                TurnStatus.GetComponent<Text>().text = "PLAYERTURN";
                break;
            case BattleState.ENEMYTURN:
                TurnStatus.GetComponent<Text>().text = "ENEMYTURN";
                break;
            case BattleState.PLAYERMOVEMENT:
                TurnStatus.GetComponent<Text>().text = "PLAYERMOVEMENT";
                break;
            case BattleState.ENEMYMOVEMENT:
                TurnStatus.GetComponent<Text>().text = "ENEMYMOVEMENT";
                break;
            case BattleState.WON:
                TurnStatus.GetComponent<Text>().text = "WON";
                break;
            case BattleState.LOST:
                TurnStatus.GetComponent<Text>().text = "LOST";
                break;
        } 
    }
}
