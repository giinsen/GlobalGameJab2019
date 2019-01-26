using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadManager : MonoBehaviour {

    private Rigidbody rb;
    public GameObject player;
    public GameObject helmetTarget;

    public float rotationSpeed;
    private float currentRotationSpeed;

    public float clampValue;
    public float easeinDistance;
    public float easeoutDistance;

	// Use this for initialization
	void Start ()
    {
        rb = player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Quaternion diff = transform.rotation * Quaternion.Inverse(helmetTarget.transform.rotation);
        float diffAngle = Quaternion.Angle(transform.rotation, helmetTarget.transform.rotation);


        // clamp
        if (diffAngle > clampValue)
        {
            // mettre casque à distance clampée
        }

        else if (diffAngle > easeinDistance)
        {

        }


        Quaternion.RotateTowards(transform.rotation, helmetTarget.transform.rotation, currentRotationSpeed);
	}

    private void OnTriggerEnter(Collider other)
    {
        print("player collision");
        rb.velocity = Vector3.zero;
    }
}
