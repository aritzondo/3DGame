using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    #region PUBLIC VARIABLES
    public float speed;
    public Vector3 initialPosition;
    public float maxDistance = 20.0f;
    #endregion

    #region PRIVATE VARIABLES
    private float orientation;
    
    #endregion

    #region PROPERTIES

    #endregion

    // Use this for initialization
    void Start () {
        orientation = 1.0f;
        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update () {
        if(Mathf.Approximately(initialPosition.z + maxDistance, transform.position.z))
        {
            orientation = -1.0f;
        }

        if(transform.position == initialPosition)
        {
            orientation = 1.0f;
        }

        transform.position += transform.forward * orientation * speed;
	}
	
	#region METHODS
	
	#endregion
	
}
