
using UnityEngine;

public class move_with_cursor : MonoBehaviour
{
    private float panBorder = 10f,
                 zoom_limit = 40f,
                 exclusion_zone = 175f;

    public static float zoom_speed = 200f,
                        panSpeed = 15f;


    /* default access */
    Vector3 pos;
    Vector3 mapSize; // un used

    /* retrieve or specify mapsize */
    public Vector3 map_size = new Vector3(120f, 0, 120f);

    /*********** SINGLETON BEGIN *************/
    public static move_with_cursor Instance { get; private set; }
    /***********  SINGLETON END   ************/

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

    void Update()
    {
        /**************************************************************/
        /* The following controls where the camera shall move as the  */
        /* cursor is set on the edge of the map; only adjust camera   */
        /* on unity and forget about adjusting the code.              */
        /**************************************************************/

        pos = transform.position;

        /* move up */
        if (Input.mousePosition.y >= Screen.height - panBorder)
        {
            pos.x += Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * panSpeed * Time.deltaTime;
            pos.z += Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * panSpeed * Time.deltaTime;

        }

        /* move down */
        if(Input.mousePosition.y <= panBorder)
        {
            pos.x -= Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * panSpeed * Time.deltaTime;
            pos.z -= Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * panSpeed * Time.deltaTime;
        }
        
        /* move right */
        if(Input.mousePosition.x >= Screen.width - panBorder)
        {
            pos.x += Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * panSpeed * Time.deltaTime;
            pos.z -= Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * panSpeed * Time.deltaTime;
        }

        /* move left */
        if(Input.mousePosition.x <= panBorder)
        {
            pos.x -= Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * panSpeed * Time.deltaTime;
            pos.z += Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * panSpeed * Time.deltaTime;
        }

        /* zoom in */
        if(Input.mouseScrollDelta.y  > 0 && pos.y != 15f)
        {
            pos.z += Mathf.Sin(transform.eulerAngles.x * Mathf.PI / 180) * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * zoom_speed * Time.deltaTime;
            pos.y -= Mathf.Cos(transform.eulerAngles.x * Mathf.PI / 180) * zoom_speed * Time.deltaTime;
            pos.x += Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * zoom_speed * Time.deltaTime;

        }

        /* zoom out */
        if (Input.mouseScrollDelta.y < 0 && pos.y != zoom_limit)
        {
            pos.z -= Mathf.Sin(transform.eulerAngles.x * Mathf.PI / 180) * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * zoom_speed * Time.deltaTime;
            pos.y += Mathf.Cos(transform.eulerAngles.x * Mathf.PI / 180) * zoom_speed * Time.deltaTime;
            pos.x -= Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * zoom_speed * Time.deltaTime;
        }

        /* Limitation of map size */
        pos.x = Mathf.Clamp(pos.x,-(map_size.x - 60f), map_size.x - 60f);
        pos.z = Mathf.Clamp(pos.z, -(map_size.z - 60f), map_size.z - 60f);
        pos.y = Mathf.Clamp(pos.y, 15f, zoom_limit);

        transform.position = pos;
    }

    /* Public Methods */

    /*
     * @method: exclusion_zone_method
     * @ret: bool
     * notes: prevents any activity at a portion of the camera that has a panel
     */
    public bool exclusion_zone_method()
    {
        if (Input.mousePosition.y <= exclusion_zone)
            return false;
        else
            return true;
    }
}
