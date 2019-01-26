using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public int checkpointLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerManager.Checkpoint(checkpointLevel, transform.position, transform.rotation);

            GetComponent<Collider>().enabled = false;
            GetComponentInChildren<Light>().color = Color.green;
        }
    }
}
