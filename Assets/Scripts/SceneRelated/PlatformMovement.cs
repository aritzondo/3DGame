using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    #region PUBLIC VARIABLES
    public Transform[] waypoints;
    public float maxDist;
    public float maxSpeed;
    public float accel;
    #endregion

    #region PRIVATE VARIABLES
    private bool canMove;
    private int currentWaypoint;
    private float velocity;
    private float dist;
    private float orientation;
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
        currentWaypoint = 0;
        canMove = true;
    }

    // Update is called once per frame
    void Update ()
    {
        Debug.Log(orientation);
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
                
        if (dist <= maxDist)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            orientation = 0;

        }

        orientation = Mathf.Clamp(dist, -1, 1);
        float velOffset = (maxSpeed * orientation) - velocity;
        velocity += Mathf.Clamp(velOffset, -accel * dt, accel * dt);
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
