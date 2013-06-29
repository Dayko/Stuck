using UnityEngine;
using System.Collections;

public class InputTouch : MonoBehaviour {

	// TODO : Detect single touch, slide, velocity, click on GUI button...

	private float timeStartTouch;
	private float timeEndTouch;
	private Vector2 positionStartTouch;
	private Vector2 positionEndTouch;

	// Tweaking
	//private float maxTimeToDetectSingleTap = 1.0f;
	private float maxDistanceToDetectSingleTap = 40.0f;

	private bool singleTap;


	void Update () {
		// Re-init all values to false
		singleTap = false;

		// Start of touch (only mouse right now)
		if (Input.GetMouseButtonDown(0)) {
			timeStartTouch = Time.time;
			positionStartTouch = Input.mousePosition;
		}

		// End of touch (only mouse right now)
		if (Input.GetMouseButtonUp(0)) {
			timeEndTouch = Time.time;
			positionEndTouch = Input.mousePosition;

			if (Vector2.Distance(positionStartTouch, positionEndTouch) < maxDistanceToDetectSingleTap)
				singleTap = true;
		}
	}

	// **********************
	// *  Public accessors  *
	// **********************

	// Quick touch on screen (without moving)
	// -----------------------------------------------
	public bool GetSingleTap() {
		return singleTap;
	}

}
