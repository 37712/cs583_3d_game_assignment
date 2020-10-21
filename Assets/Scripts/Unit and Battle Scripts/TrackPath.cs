using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************************/
/* Creates a path for each enemy to follow as opposed to following random   */
/* generation of movement by enemies since it cannot work as anticipated.   */
/*                                                                          */
/* First, create the path by placing "path nodes" at the desired places for */
/* the enemy to follow along.  The nodes are in an array for consecutive    */
/* ordering of which path to follow.                                        */
/****************************************************************************/

public class TrackPath : MonoBehaviour
{
    /* Private Serialized Field */
    [SerializeField]
    private static GameObject[] path_node;  // must be shared across objects
    [SerializeField]
    private int start_at_array;


    /************ SINGLETON BEGIN ****************/
    //public static TrackPath Instance { get; private set; }
    /************ SINGLETON END ******************/

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Public Methods */

    /*
     * @method: current_path_node
     * notes: returns the 
     */
}
