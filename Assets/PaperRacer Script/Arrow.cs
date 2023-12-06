using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float spinSpeed;
    public float speedMultiplier;
    public GameObject targetObject;
    void Update()
    {
        transform.Rotate(0f, (spinSpeed * speedMultiplier) * Time.deltaTime, 0f, Space.Self);
    }
}
