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
    private AudioManager audioManager;
    
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
    private void Start () {
        initialPosition = transform.position;
        audioManager = AudioManager.Instance;
    }
	
	// Update is called once per frame
    private void Update () {

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
                    audioManager.Stop((int)AudioManager.SoundGeneral.DOOR_HORIZONTAL);
                    audioManager.Stop((int)AudioManager.SoundGeneral.DOOR_VERTICAL);
                    audioManager.Play((int)AudioManager.SoundGeneral.DOOR_VERTICAL);
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
                    audioManager.Stop((int)AudioManager.SoundGeneral.DOOR_HORIZONTAL);
                    audioManager.Stop((int)AudioManager.SoundGeneral.DOOR_VERTICAL);
                    audioManager.Play((int)AudioManager.SoundGeneral.DOOR_HORIZONTAL);
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
        audioManager.Stop((int)AudioManager.SoundGeneral.DOOR_HORIZONTAL);
        audioManager.Stop((int)AudioManager.SoundGeneral.DOOR_VERTICAL);
        audioManager.Play((int)AudioManager.SoundGeneral.DOOR_HORIZONTAL);
        state = State.Out;
        Vector3 centerAtDoorHeight =  center.transform.position - Vector3.up * (center.transform.position.y - initialPosition.y);
        Vector3 outVector = initialPosition - centerAtDoorHeight;
        outVector = outVector.normalized;
        desiredPosition = initialPosition + outVector * outMovement;
        movingTime = 0;
    }

    public void startClosing()
    {
        audioManager.Stop((int)AudioManager.SoundGeneral.DOOR_HORIZONTAL);
        audioManager.Stop((int)AudioManager.SoundGeneral.DOOR_VERTICAL);
        audioManager.Play((int)AudioManager.SoundGeneral.DOOR_VERTICAL);
        state = State.Down;
        desiredPosition = transform.position + Vector3.up * (initialPosition.y-transform.position.y);
        movingTime = 0;
    }

    public bool isClosed()
    {
        return state == State.Close;
    }

    public void resetIniPos()
    {
        initialPosition = transform.position;
    }


    public IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    public IEnumerator UnloadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
