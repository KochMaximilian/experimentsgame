using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayerController : GenericPlayerController, IPickupAble {
    int jumps = 2;
    public Vector3 pickupOffset = new Vector3(2,1,0);
    private ParticleSystem fire;
    #region Audio
    //AudioStuffs
    private AudioSource playerAudio;
    public AudioClip fireBreath;
    public float Volume = 1.0f;
    private bool fireIsPlaying = false;
    #endregion

    override public void Start(){
        base.Start();  
        fire = GetComponent<ParticleSystem>();
        playerAudio = GetComponent<AudioSource>();

    }
    override public void Update()
    {
        base.Update();
        // Checks if fire particle effect has stopped
        // and cuts the audio if it has stopped
        if (fire.isStopped && fireIsPlaying == true)
        {
            playerAudio.Stop();
            fireIsPlaying = false;
        }
    }

    public Vector3 GetPickUpOffset(){
        return pickupOffset;
    }
    override public void Jump() {
        base.Jump();
        jumps-=1;
        if (jumps != 0) {
            //isOnGround = true;
            base.isOnGround = true;
        }
    }
    override public void OnCollisionEnter(Collision collision) {
        Vector3 normal = collision.GetContact(0).normal;
        if (normal.y > 0) {
            base.isOnGround = true;
            jumps = 2;
        } 
    }
    override public void Action(){
        if (Input.GetKeyDown(playerInputAction)) {
            fire.Play();
            RaycastHit hit;
            Vector3 pos = transform.position;
            if (Physics.SphereCast(pos, 2f, direction, out hit, 1.5f)) {
                IIce iceObject = hit.transform.GetComponent<IIce>();
                if (iceObject != null) {
                    iceObject.Shrink();
                }
            }
            //Plays the fireBreath Soundeffect
            playerAudio.PlayOneShot(fireBreath, Volume);
            fireIsPlaying = true;
        }
    }
}
