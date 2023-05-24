using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRight : MonoBehaviour
{
    public static bool rightBallIn;

    // Start is called before the first frame update
    void Start()
    {
        rightBallIn = false;
    }

    private void OnTriggerEnter(Collider ball)
    {
        if (ball.tag == "LevelSolverRight")
        {
            Debug.Log("right ball in");
            rightBallIn = true;
        }
    }

}
