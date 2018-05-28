using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorMovement : MonoBehaviour {
    public float speed = 1.0f;
    public GameObject center;
    public float timeOut = 1.0f;
    public float timeUp = 2.0f;
    public float outMovement = 1.0f;
    public float upMovement = 5.0f;
    public int sceneIndex = -1;
        
    private float movingTime = 0.0f;
    private Vector3 initialPosition;
    private Vector3 desiredPosition;
    private State state = State.Close;
    
    private enum State
    {
        Open,
        Close,
        Out,
        Up,
        In,
        Down
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        float dt = Time.deltaTime;
        movingTime += dt;
        switch (state)
        {
            case (State.Close):
                break;
            case (State.Open):
                break;
            case (State.Out):
                if (movingTime < timeOut)
                {
                    float t = movingTime / timeOut;
                    transform.position = Vector3.Lerp(transform.position, desiredPosition, t);
                }
                else
                {
                    state = State.Up;
                    movingTime = 0;
                    desiredPosition = transform.position + Vector3.up * upMovement;
                }
                break;
            case (State.Up):
                if (movingTime < timeUp)
                {
                    float t = movingTime / timeUp;
                    transform.position = Vector3.Lerp(transform.position, desiredPosition, t);
                }
                else
                {
                    state = State.Open;
                }
                break;
            case (State.Down):
                if(movingTime < timeUp)
                {
                    float t = movingTime / timeUp;
                    transform.position = Vector3.Lerp(transform.position, desiredPosition, t);
                }
                else
                {
                    state = State.In;
                    movingTime = 0;
                    desiredPosition = initialPosition;
                }
                break;
            case (State.In):
                if(movingTime < timeOut)
                {
                    float t = movingTime / timeOut;
                    transform.position = Vector3.Lerp(transform.position, initialPosition, t);
                }
                else
                {
                    state = State.Close;
                }
                break;
                
        }
    }

    public void startMovement()
    {
        initialPosition = transform.position;
        state = State.Out;
        if(center == null)
        {
            Debug.Log(this.gameObject);
        }
        Vector3 centerAtDoorHeight =  center.transform.position - Vector3.up * center.transform.position.y;
        Vector3 outVector = transform.position - centerAtDoorHeight;
        outVector = outVector.normalized;
        desiredPosition = transform.position + outVector * outMovement;
        movingTime = 0;
    }

    public void startClosing()
    {
        state = State.Down;
        desiredPosition = transform.position - Vector3.up * upMovement;
        movingTime = 0;
    }

    public bool isClosed()
    {
        return state == State.Close;
    }


    public IEnumerator LoadAsyncScene()
    {
        int number_scenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        /*Debug.Assert(sceneIndex < 0 || number_scenes>=number_scenes-1, 
                    "ERROR LOADING SCENE: choose a valid index (0-"+number_scenes+")");*/

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    public IEnumerator UnloadAsyncScene()
    {
        int number_scenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        /*Debug.Assert(sceneIndex < 0 || number_scenes >= number_scenes - 1,
                    "ERROR UNLOADING SCENE: choose a valid index (0-" + number_scenes + ")");*/

        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
