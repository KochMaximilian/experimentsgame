using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayerController : MonoBehaviour
{
    private Rigidbody playerRB;    
    public float horizontalInput;
    private Vector3 respawnPos;
    public string playerInputHorizontal = "Horizontal";
    public KeyCode playerInputJump = KeyCode.W;
    public KeyCode playerInputAction = KeyCode.F;
    public float speed = 5.0f;
    public float gravityMod = 1;
    public float jumpHeight = 10f;
    public bool allowSmoothJump = true; 
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public Vector3 direction = Vector3.right;
    public bool isOnGround = true;
    public bool active = false;
    #region AudioThings
    // audioclips and audio things
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip Step;
    public Vector3 startPosAudio;
    public Vector3 endPosAudio;
    public float audPlayThreshold = 0.5f;
    public float passedTime;
    public float audioVolumeWalk = 1.0f;
    public float timeNeededToPass = 0.2f;
    #endregion
    virtual public void Start(){
        playerRB = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        respawnPos = transform.position;
    }

    // Update is called once per frame
    virtual public void Update()
    {
        if (active){
            Move();
        }
        if (transform.position.y < -20){
            transform.position = respawnPos;
        }
        MovingSound();
    }

    // Player walking sounds
    // checks if the player is on ground & moving & sfx isn't playing
    // if not plays the walking sound
    private void MovingSound(){
        startPosAudio = playerRB.transform.position;
        passedTime -= Time.deltaTime;
        if (passedTime < -timeNeededToPass){
            endPosAudio = playerRB.transform.position;
            passedTime = 0;
        }
        if (isOnGround == true && ((startPosAudio.x - endPosAudio.x) > audPlayThreshold || (startPosAudio.x - endPosAudio.x) < -audPlayThreshold) && GetComponent<AudioSource>().isPlaying == false){
            playerAudio.volume = UnityEngine.Random.Range(audioVolumeWalk / 1.3f, audioVolumeWalk);
            playerAudio.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
            playerAudio.Play();

        }
    }

    virtual public void Move() {
        horizontalInput = Input.GetAxis(playerInputHorizontal);
        if (horizontalInput < -0.1) {
            direction = Vector3.left;

        } else if (horizontalInput > 0.1) {
            direction = Vector3.right;
        }
        //transform.Rotate(0f, direction.x * -180f, 0f, Space.World);
        transform.rotation = Quaternion.AngleAxis(direction.x * 90f + 90f, Vector3.up);
        transform.Translate(-direction * Time.deltaTime * horizontalInput * speed);
        Vector3 position = transform.position;
        transform.position = position;
        if (Input.GetKeyDown(playerInputJump) && isOnGround){
            Jump();
        } 
        Action();
    }

    virtual public void Jump() {
        float yVelocity = playerRB.velocity.y;
        playerRB.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        // vertical motion less then zero (falling):
        if (playerRB.velocity.y < 0 && allowSmoothJump) {
            // apply our fallmultiplier to our gravity
            playerRB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (playerRB.velocity.y > 0 && Input.GetKeyDown(playerInputJump)) {
            playerRB.velocity -= Vector3.up * Physics.gravity.y *  (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        //playerRB.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        isOnGround = false;
        playerAudio.PlayOneShot(jumpSound, 1.0f);
        
    }

    virtual public void OnCollisionEnter(Collision collision) {
        Vector3 normal = collision.GetContact(0).normal;
        if (normal.y > 0) {
            isOnGround = true;
        } 
    }

    virtual public void Action() {}
}
