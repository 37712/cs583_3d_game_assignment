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
    public Unit PlayerUnit;
    public Text TurnStatus;
    public Text HealthStatus;
    public Text killCountStatus;
    public GameObject victory_panel;
    public GameObject louser_panel;

    private void Start()
    {

    }

    void Update()
    {
        // update GUI battle state text
        BattleState state = GameController.GetComponent<BattleManager>().state;
        switch(state)
        {
            case BattleState.PLAYERTURN:
                TurnStatus.text = "PLAYERTURN";
                break;

            case BattleState.ENEMYTURN:
                TurnStatus.text = "ENEMYTURN";
                break;

            case BattleState.PLAYERMOVEMENT:
                TurnStatus.text = "PLAYERMOVEMENT";
                break;

            case BattleState.ENEMYMOVEMENT:
                TurnStatus.text = "ENEMYMOVEMENT";
                break;

            case BattleState.WON:
                TurnStatus.text = "WON";
                print("need to implement WON panel");
                break;

            case BattleState.LOST:
                TurnStatus.text = "LOST";
                print("need to implement LOST panel");
                break;
        }

        // update GUI health status text
        HealthStatus.text = "Health = " + PlayerUnit.currentHP;

        // update GUI killCount status text
        killCountStatus.text = "killCount = " + PlayerUnit.killCount;
    }
}
