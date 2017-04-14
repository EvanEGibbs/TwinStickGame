using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Replay : MonoBehaviour {

	//number of frames to record for
	private const int bufferFrames = 123;
	private MyKeyFrame lastPosition;
	private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
	private GameManager gameManager;
	//track which frame is being recorded and playback accordingly. Ticks up each frame
	private int frameRecordNumber = 0;
	//track how many frames were recorded, in case the frames recorded are less than the number of total frames possible to be recorded
	private int framesRecorded = 0;
	//flag for whether in playback mode or not
	private bool startPlayback = false;

	private Rigidbody rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		gameManager = GameObject.FindObjectOfType<GameManager>();
		lastPosition = new MyKeyFrame(Time.time, transform.position, transform.rotation);
	}
	
	void Update () {
		//solves for error when clicking play in unity editor, thinks frame one the fire button is down?
		if (Time.frameCount > 1) {
			if (gameManager.recording) {
				Record();
			}
			else {
				PlayBack();
			}
			frameRecordNumber += 1;
			if (frameRecordNumber >= bufferFrames) {
				frameRecordNumber = 0;
			}
			//if releasing the playback button, reset the objects and clear the recordings, reset the variables
			if (CrossPlatformInputManager.GetButtonUp("Fire1")) {
				frameRecordNumber = 0;
				framesRecorded = 0;
				startPlayback = false;
				transform.position = lastPosition.position;
				transform.rotation = lastPosition.rotation;
				keyFrames = new MyKeyFrame[bufferFrames];
			}
		}
	}

	void PlayBack() {
		//if first frame of playback
		if (!startPlayback) {
			lastPosition = new MyKeyFrame(Time.time, transform.position, transform.rotation);
			// if the buffer frames are fully cached, the frame can stay the same to go to the first frame of the playback recorded automatically. Otherwise needs to be set to frame 0.
			if (framesRecorded < bufferFrames) {
				frameRecordNumber = 0;
			}
			startPlayback = true;
		}
		//loop frame number so it only records on the max number of buffer frames
		if (frameRecordNumber >= framesRecorded) {
			frameRecordNumber = 0;
		}
		rigidBody.isKinematic = true;
		transform.position = keyFrames[frameRecordNumber].position;
		transform.rotation = keyFrames[frameRecordNumber].rotation;
	}

	private void Record() {
		rigidBody.isKinematic = false;
		float time = Time.time;
		keyFrames[frameRecordNumber] = new global::MyKeyFrame(time, transform.position, transform.rotation);
		framesRecorded += 1;
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