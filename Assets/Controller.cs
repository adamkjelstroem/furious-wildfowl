using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour
{
    private Vector3 direction;
    private float rotation;

    float camDistanceFactor = 2;
    float camDeltaY = 1;

    public GameObject cam;

    public float forwardYeetForce = 3;
    public float upwardYeetForce = 3;
    public float turnSpeed = 0.02f;

    public int numYeet = 0;

    public int powerUpYeetForceIncrease= 3;

    // Start is called before the first frame update
    void Start()
    {
        computeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
        rotation += turnSpeed * Input.GetAxis("Horizontal");
        
        computeDirection();


        cam.transform.rotation = Quaternion.LookRotation(direction,Vector3.up);
        cam.transform.position = transform.position - direction * camDistanceFactor + Vector3.up * camDeltaY;


        if(Input.GetKeyDown("space")){
            //yeet
            GetComponent<Rigidbody>().AddForce(direction * forwardYeetForce + Vector3.up * upwardYeetForce, ForceMode.Impulse);
            numYeet += 1;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger hit!");

        if(other.tag == "powerUp"){
            //we hit the power up!
            Destroy(other.gameObject);

            forwardYeetForce += powerUpYeetForceIncrease;
            upwardYeetForce += powerUpYeetForceIncrease;
        }
    }

    void computeDirection(){
        direction = new Vector3((float)Math.Sin(rotation), 0, (float)Math.Cos(rotation));
    }
}
