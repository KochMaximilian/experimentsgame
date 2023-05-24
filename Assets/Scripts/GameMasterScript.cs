using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterScript : MonoBehaviour
{
    public bool multiplayer = false;
    // Start is called before the first frame update
    GenericPlayerController playerOne;
    GenericPlayerController playerTwo;
    public GameObject icePlayer;
    public GameObject firePlayer;
    //Loading next level
    private IEnumerator coroutine;
    private bool levelLoaded = false;
    public float waitUntilLoad;
    public bool bothAreIn;
    //Audio Stuffs
    private AudioSource gameAudio;
    public AudioClip victoryJingle;
    public float Volume = 1.0f;
    public string nextLevel;

    public BallSwitch[] switches;

    void Start(){
        playerOne = icePlayer.GetComponent<GenericPlayerController>();
        playerTwo = firePlayer.GetComponent<GenericPlayerController>();
        playerOne.active = true;
        playerTwo.active = false;
        if (multiplayer) {
            playerTwo.active = true;
            playerOne.playerInputHorizontal = "Horizontal_ice";
            playerOne.playerInputJump = KeyCode.W;
            playerTwo.playerInputHorizontal = "Horizontal_fire";
            playerTwo.playerInputJump = KeyCode.UpArrow;
            playerTwo.playerInputAction = KeyCode.RightShift;
        }
        bothAreIn = false;
        gameAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update(){
        if (!multiplayer) {
            if (Input.GetKeyDown("x")) {
                playerOne.active = !(playerOne.active);
                playerTwo.active = !(playerTwo.active);
            }
        }

        /*if (LevelChanger.leftBallIn && ColliderRight.rightBallIn)
        {
            bothAreIn = true;
        }*/

        bool ts = true;
        foreach (BallSwitch bs in switches) {
            if (bs != null) {
                ts = ts && bs.IsTriggered();
            } else {
                ts = false;
            }
        }
        if(ts && !levelLoaded) {
            // plays the victoryJingle. the if prevents it
            // from playing every frame
            if (GetComponent<AudioSource>().isPlaying == false){
                gameAudio.PlayOneShot(victoryJingle, Volume);
            }
            Debug.Log("Both are in");
            coroutine = LoadNextLevel(waitUntilLoad, nextLevel);
            Debug.Log(nextLevel);
            Debug.Log("Starting coroutine");
            StartCoroutine(coroutine);
            Debug.Log("Coroutine started");
            levelLoaded = true;

        }
    }

    IEnumerator LoadNextLevel(float seconds, string level)
    {
        Debug.Log("Waiting for seconds: ");
        yield return new WaitForSeconds(seconds);
        Debug.Log("About to load level");
        SceneManager.LoadScene(level);
    }


}
