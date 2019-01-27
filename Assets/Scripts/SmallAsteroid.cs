using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SmallAsteroid : Asteroid
{

    public float asteroidDepartudeSpeed = 150f;

    public bool rotateAroundSomething;
    public GameObject something;
    public float rotationSpeedAroundSomething;

    private Vector3 initialVelocity = Vector3.zero; // always zero for the time being
    private Vector3 initialPosition;

    private static List<SmallAsteroid> asteroids = new List<SmallAsteroid>();

    private void Start()
    {
        initialPosition = transform.position;
        asteroids.Add(this);
    }

    protected override void Update()
    {
        base.Update();
        if (rotateAroundSomething)
        {
            transform.RotateAround(something.transform.position, Vector3.up, rotationSpeedAroundSomething * Time.deltaTime);
        }

    }

    public static void ResetPositions()
    {
        foreach(SmallAsteroid ast in asteroids)
        {
            ast.ResetThis();
        }
    }

    public void ResetThis()
    {
        transform.position = initialPosition;
        //ici
    }

   /*protected override void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None)
        {
            if (interactable.attachedToHand == null)
            {
                twoHandGrab = false;
                isDetached = false;
                LastHandGrab = hand;
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

                //Debug.Log("grabing start hand position :  " + hand.transform.position);
                startingHandPos = hand.transform.position;
                previousHandPos = startingHandPos;
                previousAsteroidPos = transform.position;

                durationGrab = 0f;
                //player.transform.SetParent(this.gameObject.transform);
                //startingPlayerPos = player.transform.position;
                ClearPositionList();

                //gameObject.GetComponent<Rigidbody>().velocity = (gameObject.GetComponent<Rigidbody>().velocity + player.GetComponent<Rigidbody>().velocity) / 2f;

                gameObject.GetComponent<Rigidbody>().velocity = (gameObject.GetComponent<Rigidbody>().velocity + player.GetComponent<Rigidbody>().velocity);

                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
             
                Debug.Log("##########  " + gameObject.GetComponent<Rigidbody>().velocity);
                handAttachementPoint.transform.position = hand.transform.position;

                hand.TriggerHapticPulse(0.15f, 1 / 0.15f, 100f);

                //player.transform.parent.transform.parent = transform;
                player.transform.parent = handAttachementPoint.transform;
            }
            else
            {
                twoHandGrab = true;
                //interactable.attachedToHand = null;
                LastHandGrab.DetachObject(gameObject);
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags);


                //hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
                LastHandGrab = hand;
                durationGrab = 0f;
                startingHandPos = hand.transform.position;
                previousHandPos = startingHandPos;
                previousAsteroidPos = transform.position;

                ClearPositionList();
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player.transform.parent = null;
                handAttachementPoint.transform.position = hand.transform.position;
                player.transform.parent = handAttachementPoint.transform;
                hand.TriggerHapticPulse(0.15f, 1 / 0.15f, 50f);

            }
        }
    } */

    protected override void OnDetachedFromHand(Hand hand)
    {
        isDetached = true;
        //player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.SetParent(null);
        //Vector3 totalMovement = (startingPlayerPos - player.transform.position);
        player.GetComponent<Rigidbody>().AddForce(GetDirectionPositionList() * departureSpeed);
        gameObject.GetComponent<Rigidbody>().AddForce(-GetDirectionPositionList() * asteroidDepartudeSpeed);
        Debug.Log("grabing finish : " + GetDirectionPositionList());
    }
}
