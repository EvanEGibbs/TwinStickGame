using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour {

	private GameObject playerObject;
	//private Vector3 startCoordinates;

	void Start () {
		playerObject = GameObject.FindGameObjectWithTag("Player");
		//startCoordinates = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	void LateUpdate () {
		//transform.position = startCoordinates + playerObject.transform.position;
		transform.LookAt(playerObject.transform);
	}
}
