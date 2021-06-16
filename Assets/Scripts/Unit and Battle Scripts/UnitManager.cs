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

    public int health; // health points
    public int view_range; // how far can unit see
    public int attack; // attack points
    public int attack_range; // how far can unit attack

    public int speed;

}

