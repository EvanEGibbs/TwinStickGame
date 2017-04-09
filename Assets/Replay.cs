using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour {

	private const int bufferFrames = 100;
	private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
	private GameManager gameManager;

	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.recording) {
			Record();
		}
		else {
			PlayBack();
		}
	}

	void PlayBack() {
		rigidBody.isKinematic = true;
		int frame = Time.frameCount % bufferFrames;
		transform.position = keyFrames[frame].position;
		transform.rotation = keyFrames[frame].rotation;
	}

	private void Record() {
		rigidBody.isKinematic = false;
		int frame = Time.frameCount % bufferFrames;
		float time = Time.time;
		keyFrames[frame] = new global::MyKeyFrame(time, transform.position, transform.rotation);
	}
}

/// <summary>
/// A structure for storing frame, position and rotation
/// </summary>
public struct MyKeyFrame {
	public float frameTime;
	public Vector3 position;
	public Quaternion rotation;

	public MyKeyFrame (float aTime, Vector3 aPosition, Quaternion aRotation) {
		frameTime = aTime;
		position = aPosition;
		rotation = aRotation;
	}
}