using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotaterandom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0.05f, 0.04f, 0.6f * Time.deltaTime);
	}
}
