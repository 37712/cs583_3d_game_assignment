using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicMovement : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent nav; // needed for the point and clic
    
    public float maxMoveDistancePerTurn = 30;

    // might not even need these, not sure
    private bool running = false;
    private bool shooting = false; 


    void Start()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /***** THIS PART IS FOR ANIMATION **********/
        animation_running();
        animation_shoot();
    }

    /*
     * @method: animation_running
     * notes: invokes shooting action of player unit.
     */
    private void animation_running()
    {
        // remaining distance is between 0 and 35
        if (nav.remainingDistance >= nav.stoppingDistance)
        {
            //animation
            running = true;
        }
        else
        {
            //animation
            running = false;
        }

        // set animation state
        animator.SetBool("running", running);
    }

    /*
     * @method: animation_shoot
     * notes: invokes animation of shooting of player unit.
     */
    private void animation_shoot()
    {
        //shooting animation
        if (Input.GetKey(KeyCode.W))
        {
            shooting = true;
            Debug.Log("We shoot");
        }
        else
            shooting = false;

        //set animation shooting state
        animator.SetBool("shooting", shooting);
    }

    /*
     * @method: mymethod
     * @param: NavMeshAgent nav
     * notes: reports, I believe, where the unit is moving and it's remaining travel distance?
     */
    void mymethod(NavMeshAgent nav)
    {
        print("destination" + nav.destination);
        print("remaining" + nav.remainingDistance);
    }

    /*
     * @method: onMouseMove
     * notes: this one is responsible for moving unit around with the click of the mouse
     */

     // need to fix this
    public void onMouseMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 adjustment_of_position; // This vector shall assist in proberly positioning the player in terms of (X,Z)

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && move_with_cursor.Instance.exclusion_zone_method())
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                /*************** ARMANDO COMMENT AND CODE ****************************/

                // I am going to make the unit land on an XZ coordinate that is rounded to the whole number
                adjustment_of_position = new Vector3(Mathf.Floor(hit.point.x), hit.point.y, Mathf.Floor(hit.point.z));

                // "hit.point" was replaced with "adjustment_of_position"

                print("vector distance = " + Vector3.Distance(nav.destination, adjustment_of_position));
                // if destination is within 35 something
                if (Vector3.Distance(nav.destination, adjustment_of_position) <= maxMoveDistancePerTurn)
                {
                    nav.destination = adjustment_of_position;
                }
            }
        }
    }
}

