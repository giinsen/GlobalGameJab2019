using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Asteroid : MonoBehaviour
{

    protected Interactable interactable;
    protected Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.DetachOthers;
    protected GameObject player;
    protected Vector3 startingHandPos;
    protected Vector3 previousHandPos;
    protected float durationGrab = 0f;

    public float rotationSpeed;

    protected Vector3 startingPlayerPos;
    protected List<Vector3> positionList = new List<Vector3>();

    protected Vector3 previousAsteroidPos;

    protected bool isDetached = true;

    public float grabSpeedModifier = 0.7f;
    public float departureSpeed;

    protected GameObject handAttachementPoint;

    protected GameObject playerAttachementPoint;

    protected Hand LastHandGrab;

    protected bool twoHandGrab = false;

    private Vector3 startAsteroidPos;
    private Quaternion startAsteroidRotQ;

    private Vector3 startHeadPos;
    private Quaternion startPlayerRotQ;

    private float distToHand;

    public GameObject head;

    protected virtual void Awake()
    {
        interactable = GetComponent<Interactable>();
        player = GameObject.FindGameObjectWithTag("Player");
        handAttachementPoint = new GameObject("Attachment Point");
        handAttachementPoint.transform.parent = transform;
        handAttachementPoint.transform.localPosition = Vector3.zero;
        GetComponent<Interactable>().handFollowTransform = handAttachementPoint.transform;

        playerAttachementPoint = new GameObject("Player Attachment Point");
        playerAttachementPoint.transform.parent = transform;
        playerAttachementPoint.transform.localPosition = Vector3.zero;

        startAsteroidPos = transform.position;
        startAsteroidRotQ = transform.rotation;
    }

    protected virtual void Update()
    {
        transform.Rotate(Vector3.one * Time.deltaTime * rotationSpeed, Space.Self);
    }

    protected virtual void HandHoverUpdate(Hand hand)
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
                startingHandPos = hand.transform.localPosition;
                previousHandPos = startingHandPos;
                previousAsteroidPos = transform.position;

                durationGrab = 0f;
                //player.transform.SetParent(this.gameObject.transform);
                //startingPlayerPos = player.transform.position;
                ClearPositionList();
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;

                handAttachementPoint.transform.position = hand.transform.position;

                hand.TriggerHapticPulse(0.15f, 1 / 0.15f, 100f);

                //playerAttachementPoint.transform.position = player.transform.position;

                //player.transform.parent.transform.parent = transform;
                playerAttachementPoint.transform.position = player.transform.position;
                player.transform.parent = playerAttachementPoint.transform;

                //startHeadPos = head.transform.position;//head.transform.position + (player.transform.position - head.transform.position) ;
                //startPlayerRotQ = head.transform.rotation;
                //distToHand = Vector3.Distance(head.transform.position, player.transform.position);
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
                //player.transform.parent = null;
                handAttachementPoint.transform.position = player.transform.position;
                //player.transform.parent = handAttachementPoint.transform;
                hand.TriggerHapticPulse(0.15f, 1 / 0.15f, 50f);

            }
        }


    }

    protected virtual void HandAttachedUpdate(Hand hand)
    {
        if (!isDetached)
        {
            UpdatePositionList(player.transform.position);
            //Debug.Log("grabing start hand position :  " + hand.transform.position);
            durationGrab += Time.deltaTime;
            //Debug.Log("grab is on the way : " + hand.transform.position);

            


            //Vector3 asteroidMovement = transform.position - previousAsteroidPos;
            Vector3 playerMovement = (previousHandPos - hand.transform.localPosition) * grabSpeedModifier;

            //player.transform.position = player.transform.position + playerMovement + asteroidMovement;
            player.transform.localPosition += playerMovement;

            //player.transform.position = playerAttachementPoint.transform.position;

            previousHandPos = hand.transform.localPosition;
            //previousAsteroidPos = transform.position;



        }
        if (hand.IsGrabEnding(this.gameObject))
        {
            hand.DetachObject(gameObject);
            //Debug.Log("grabing ending, duration grab : " + durationGrab);
        }

    }

    protected virtual void OnDetachedFromHand(Hand hand)
    {
        if (!twoHandGrab)
        {
            isDetached = true;
            //player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.SetParent(null);
            //player.transform.parent = null;
            //Vector3 totalMovement = (startingPlayerPos - player.transform.position);
            player.GetComponent<Rigidbody>().AddForce(GetDirectionPositionList() * departureSpeed);
            //Debug.Log("grabing finish : " + GetDirectionPositionList());
        }
    }

    private void UpdatePositionList(Vector3 v)
    {
        positionList.Add(v);
        if (positionList.Count >= 20)
        {
            positionList.RemoveAt(0);
        }
    }

    protected Vector3 GetDirectionPositionList()
    {
        if (positionList.Count <= 0)
            return Vector3.zero;
        Vector3 pos = positionList[positionList.Count - 1] - positionList[0];
        return pos;
    }

    protected void ClearPositionList()
    {
        positionList.Clear();
    }


}
