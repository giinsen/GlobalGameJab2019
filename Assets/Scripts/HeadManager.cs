using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadManager : MonoBehaviour {

    private Rigidbody rb;
    public GameObject player;

    [Header("Helmet movement")]
    public GameObject helmetTarget;

    public float rotationSpeed;

    public float clampDistance;
    public float easeinDistance;
    public float easeinMinSpeed;
    public float easeoutDistance;
    public float easeoutMinSpeed;

	// Use this for initialization
	void Start ()
    {
        //rb = player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = helmetTarget.transform.position;

        Quaternion diff = transform.rotation * Quaternion.Inverse(helmetTarget.transform.rotation);
        float diffAngle = Quaternion.Angle(transform.rotation, helmetTarget.transform.rotation);
        //Debug.Log(diffAngle);

        float currentRotationSpeed = 0;

        // clamp
        if (diffAngle > clampDistance)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, helmetTarget.transform.rotation, diffAngle - clampDistance);
            return;
        }

        // ease in
        else if (diffAngle <= easeinDistance)
        {
            currentRotationSpeed = Mathf.Lerp(easeinMinSpeed, rotationSpeed, (diffAngle - easeinDistance)/(clampDistance - easeinDistance)); // 0 -> 1
        }

        //normal rot
        else if (diffAngle < easeoutDistance)
        {
            currentRotationSpeed = rotationSpeed;
        }

        // ease out
        else if (diffAngle >= easeoutDistance)
        {
            currentRotationSpeed = Mathf.Lerp(easeoutDistance, rotationSpeed, (diffAngle - 0) / (easeoutDistance - 0)); // 1 -> 0
        }

        else
        {
            Debug.LogError("Unexpected case in helmet rotation !!");
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, helmetTarget.transform.rotation, currentRotationSpeed * Time.deltaTime);
        //transform.rotation = helmetTarget.transform.rotation;

    }

    private void OnTriggerEnter(Collider other)
    {
        //print("player collision");
        //rb.velocity = Vector3.zero;
    }
}
