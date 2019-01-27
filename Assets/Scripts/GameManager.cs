using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour {


    public static GameManager instance;

    public Hand lHand;
    public Hand rHand;
    
    public float velocityCheckTime = 0.3f;

    [HideInInspector]
    public Vector3 lastVelocity;
    private Vector3 oldPos;

    public float smoothOnGrab = 40;

    [HideInInspector]
    public Rigidbody rb;
	// Use this for initialization
	void Awake () {
        instance = this;
        rb = GetComponent<Rigidbody>();
        oldPos = transform.position;
        InvokeRepeating("GetVelocity", velocityCheckTime, velocityCheckTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void GetVelocity()
    {
        lastVelocity = transform.position - oldPos;
        oldPos = transform.position;
    }

}
