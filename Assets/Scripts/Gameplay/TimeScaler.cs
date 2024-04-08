using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    

    public bool IsPaused = false;
    public float SlowMoTransitionTime;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (IsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
