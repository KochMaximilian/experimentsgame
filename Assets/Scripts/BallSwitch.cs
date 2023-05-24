using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSwitch : MonoBehaviour, Iswitch {
    public bool triggered = false;

    [SerializeField] private int socketId = 0;

    public bool IsTriggered() {
        return triggered;
    }

    private void OnTriggerEnter(Collider other){
        BallScript bs = other.GetComponent<BallScript>();
        if (bs != null) {
            if (bs.GetSocketId() == socketId) {
                triggered = true;
            }
        }
    }

    private void OnTriggerExit(Collider other){
        triggered = false;
    }
}
