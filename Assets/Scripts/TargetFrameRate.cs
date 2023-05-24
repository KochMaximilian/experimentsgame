using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    public int targetFrame = 90;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 90;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetFrame != Application.targetFrameRate)
        {
            Application.targetFrameRate = targetFrame;
        }
    }
}
