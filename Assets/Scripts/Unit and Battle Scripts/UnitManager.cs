using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/********************************************************/
/* This unit manager shall communicate witht he battle  */
/* manager.  This is to, hopefully, avoid any confusion */
/* other unnecessary additions when piecing the objects */
/* in unity.                                            */
/********************************************************/

public class UnitManager : MonoBehaviour
{
    /* Private Fields */
    

    /* Private Serialized Fields */


    /* Public Field */
    public Unit playerUnit;
    public EnemyMovement[] enemyMove;

    /*********** SINGLETON BEGIN ***************/
    public static UnitManager Instance { get;  private set; }
    /*********** SINGLETON END *****************/

    void Awake()
    {
        //Initiate singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }




    void Start()
    {
        
    }

    void Update()
    {

    }

    /********************* Private Methods ************************/


    /*************** end of private methods ***********************/

    /* Public Methods */

    /*
     * @method: all_enemies_dead
     * notes: passes a message to the battle system that every unit is now dead
     */
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

