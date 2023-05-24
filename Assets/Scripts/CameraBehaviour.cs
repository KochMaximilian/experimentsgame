using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject FirePlayer;
    public GameObject IcePlayer;
    public Vector3 Offset = new Vector3(0,5,-10);
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (IcePlayer != null) {
            //transform.position = IcePlayer.transform.position + Offset;
            transform.position = MeanPosition(IcePlayer.transform.position, FirePlayer.transform.position) + Offset;
        } 
    }
    Vector3 MeanPosition(Vector3 one, Vector3 two) {
        two = two - one;
        two = two / 2;
        return two + one;

    }
}
