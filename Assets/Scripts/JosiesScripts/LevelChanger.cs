using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public static bool leftBallIn;

    // Start is called before the first frame update
    void Start()
    {
        leftBallIn = false;
    }

    private void OnTriggerEnter(Collider ball)
    {
        if (ball.tag == "LevelSolverLeft")
        {
            Debug.Log("left ball in");
            leftBallIn = true;
        }
    }
}
