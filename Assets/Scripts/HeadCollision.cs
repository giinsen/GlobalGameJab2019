using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollision : MonoBehaviour {

    private Rigidbody rb;
    public GameObject player;
	// Use this for initialization
	void Start () {
        rb = player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("player collision");
        rb.velocity = Vector3.zero;
    }
}
