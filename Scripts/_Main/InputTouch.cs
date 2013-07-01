using UnityEngine;
using System.Collections;

public class InputTouch : MonoBehaviour {

	private enum TouchState { NOTHING, STARTTOUCH, TOUCH, ENDTOUCH };
	private TouchState touchState;

	// Touch Actions
	private bool singleTap;				// Quick touch on screen (without moving finger a lot)
	private bool sliding;				// Finger sliding on screen (move camera in normal mode)

	// Input informations
	private float touchTimeStart;
	private float touchTimeEnd;
	private Vector2 touchPosStart;		// Position on screen where touch start
	private Vector2 touchPosDelta;		// Distance between start and actual position
	private Vector2 touchPosSpeed;		// Actual speed of touch on screen
	private Vector2 touchPosEnd;		// Position on screen where touch end

	// Tweaking
	private float speedDecreaseClamp = 10.0f;
	private float speedDecreaseSpeedTime = 8.0f;
	private float maxDistanceToDetectSingleTap = 40.0f;



	void Start() {
		touchState = TouchState.NOTHING;
	}



	public Vector2 GetTouchStart() {
		return touchPosStart;
	}
	public Vector2 GetTouchDelta() {
		return touchPosDelta;
	}
	public Vector2 GetTouchSpeed() {
		return touchPosSpeed;
	}



	// Update input informations and then update each action
	// --------------------------------------------------------
	void Update() {
		TouchState newTouchState = touchState;

		switch (touchState) {
			case TouchState.NOTHING: // Wait for a touch
				touchPosSpeed = Vector2.ClampMagnitude(touchPosSpeed, speedDecreaseClamp);
				touchPosSpeed = Vector2.Lerp(touchPosSpeed, Vector2.zero, Time.deltaTime * speedDecreaseSpeedTime);

				if (Input.GetMouseButtonDown(0)) {
					newTouchState = TouchState.STARTTOUCH;
				}
				break;

			case TouchState.STARTTOUCH: // Start of touch (only for mouse, right now)
				touchTimeStart = Time.time;
				touchPosStart = Input.mousePosition;
				touchPosDelta = Vector2.zero;
				touchPosSpeed = Vector2.zero;

				newTouchState = TouchState.TOUCH;
				break;

			case TouchState.TOUCH: // Touch in progress
				Vector2 posDelta = touchPosDelta;
				touchPosDelta = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - touchPosStart;
				touchPosSpeed = touchPosDelta - posDelta;

				if (!Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)) {
					newTouchState = TouchState.ENDTOUCH;
				}
				
				break;

			case TouchState.ENDTOUCH: // End of touch
				touchTimeEnd = Time.time;
				touchPosEnd = Input.mousePosition;

				newTouchState = TouchState.NOTHING;
				break;

		}

		// Call actions update
		UpdateSingleTap();
		UpdateSliding();

		touchState = newTouchState;
	}

	
	// Quick touch on screen (without moving finger a lot)
	// -------------------------------------------------------
	private void UpdateSingleTap() {
		singleTap = false;

		if (touchState != TouchState.ENDTOUCH)
			return;

		if (Vector2.Distance(touchPosStart, touchPosEnd) < maxDistanceToDetectSingleTap)
			singleTap = true;
	}
	
	public bool GetSingleTap() {
		return singleTap;
	}

	

	// Finger sliding on screen (move camera in normal mode)
	// --------------------------------------------------------
	private void UpdateSliding() {
		if (touchState != TouchState.TOUCH) {
			sliding = false;
			return;
		}

		if (touchPosDelta.magnitude > maxDistanceToDetectSingleTap || touchPosSpeed.magnitude > 0.5f)
			sliding = true;
	}

	public bool GetSliding() {
		return sliding;
	}

}
