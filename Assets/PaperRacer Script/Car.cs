using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    Vector3 carPos;
    Vector3 targetPos;
    Vector3 direction;

    Rigidbody rb;
    public Arrow target;
    GameObject targetObject;
    
    public float launchForce;
    public float launchMultiplier;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetObject = target.targetObject;
    }

    // Update is called once per frame
    void Update()
    {
        target.speedMultiplier = launchMultiplier;
        carPos = transform.position;
        targetPos = targetObject.transform.position;
        direction = targetPos - carPos;
        transform.forward = direction;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
    }

    void Launch()
    {
        rb.velocity = transform.forward * (launchForce * launchMultiplier);
    }
}
