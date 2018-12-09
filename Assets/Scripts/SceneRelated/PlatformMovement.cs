﻿using System.Collections;
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
    public bool canMove;
    #endregion

    #region PRIVATE VARIABLES

    protected int currentWaypoint;
    protected float velocity;
    protected float dist;
    protected bool timeRun;
    protected float targetTime;
    protected bool changeWp;
    protected bool toOrigin;

    #endregion

    // Use this for initialization
    void Start ()
    {
        transform.position = waypoints[0].position;
        
        currentWaypoint = 1;
        changeWp = true;
        toOrigin = false;

        timeRun = canMove;
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

        if(toOrigin)
        {
            ReturnToOrigin();
        }
	}
	
    public void MovePlatform()
    {
        float dt = Time.deltaTime;
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        dist = direction.magnitude;
        float velOffset = maxSpeed - velocity;
        velocity += Mathf.Min(velOffset, accel * dt);
        
        if (dist <= distToSlow)
        {
            if (dist <= distToChangeWaypoint)
            {
                timeRun = false;

                if(Time.time >= targetTime && changeWp)
                {
                    timeRun = true;
                    currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                }               
            }
        }
        
        float disp = Mathf.Min(velocity * dt, dist);
        transform.position += direction.normalized * disp;
    }

    private void ReturnToOrigin()
    {
        if (transform.position == waypoints[0].position)
        {
            changeWp = true;
            toOrigin = false;
            canMove = false;
        }
    }


    //STAY ON THE PLATFORM

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player enter");
            other.transform.SetParent(transform);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exit");
            other.transform.SetParent(null);
        }
    }

}
