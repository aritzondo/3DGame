using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    #region PUBLIC VARIABLES
    public Transform[] waypoints;
    public float distToSlow;
    public float distToChangeWaypoint;
    public float stopTime;
    public float maxSpeed;
    public float accel;
    #endregion

    #region PRIVATE VARIABLES
    private bool canMove;
    private int currentWaypoint;
    private float velocity;
    private float dist;
    private float orientation;
    private bool timeRun;
    private float targetTime;

    #endregion

    #region PROPERTIES
    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
    #endregion

    // Use this for initialization
    void Start ()
    {
        transform.position = waypoints[0].position;
        currentWaypoint = 1;
        canMove = true;

        if(canMove)
        {
            timeRun = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (timeRun)
        {
            targetTime = Time.time + stopTime;
        }

        if (canMove)
        {
            MovePlatform();
        }
	}
	
	#region METHODS
	
    public void MovePlatform()
    {
        float dt = Time.deltaTime;
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        dist = direction.magnitude;

        orientation = Mathf.Clamp(dist, -1.0f, 1);
        float velOffset = (maxSpeed * orientation) - velocity;
        velocity += Mathf.Clamp(velOffset, -accel * dt, accel * dt);

        if (dist <= distToSlow)
        {
            orientation = Mathf.Clamp(dist, 0.0f, 0.0f);  

            if (dist <= distToChangeWaypoint)
            {
                timeRun = false;

                if(Time.time >= targetTime)
                {
                    timeRun = true;
                    currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                }               
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, velocity * dt);
    }


    //STAY ON THE PLATFORM


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = null;

        }
    }

    #endregion

}
