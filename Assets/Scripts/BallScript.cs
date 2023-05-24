using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour, IPickupAble {
    public Vector3 pickupOffset = new Vector3(2,1,0);
    public bool respawn = true;
    private Vector3 respawnLocation;
    [SerializeField] 
    private int socketId = 0;
    void Start(){
        respawnLocation = transform.position;
    }

    void Update(){
        if (transform.position.y < -20) {
            if (respawn) {
                transform.position = respawnLocation;
            } else {
                Destroy(gameObject);
            }
        }
    }

    public int GetSocketId() {
        return socketId;
    }

    public Vector3 GetPickUpOffset(){
        return pickupOffset;
    }
}

