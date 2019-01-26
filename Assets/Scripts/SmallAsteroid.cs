using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SmallAsteroid : Asteroid
{

    public float asteroidDepartudeSpeed = 150f;

    protected override void Update()
    {
        transform.Rotate(Vector3.one * Time.deltaTime * 0f, Space.Self);
    }

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
