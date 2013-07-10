using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	private enum CameraState {FREE, FOCUS}
	private CameraState cameraState;

	private Vector3 nextPos;
	private float nextSpeed;
	private float speedCoeff = 5.0f;

	private Vector3 focusTargetPos;
	private Vector3 focusPosOffset = new Vector3(0, 0, -30);
	private float focusSpeedCoeff = 4.0f;

	private float zoomHeightCurrent;
	private float zoomHeightMin = 100;
	private float zoomHeightMax = 200;



	void Start() {
		zoomHeightCurrent = zoomHeightMin;
	}


	public void FocusOnPosition(Vector3 position) {
		cameraState = CameraState.FOCUS;
		focusTargetPos = position;
	}


	void Update() {
		nextPos = transform.position;
		nextSpeed = Time.deltaTime * speedCoeff;

		switch (cameraState) {
			case CameraState.FREE : // Slide camera with finger
				UpdateFree();
				break;
			case CameraState.FOCUS :// Focus camera on a specific position
				UpdateFocus();
				break;
		}
		
		transform.position = Vector3.Lerp(transform.position, nextPos, nextSpeed);
	}


	private void UpdateFree() {
		if (Game.GetChrSelected().IsInSpecialMode())
			return;

		if (Game.inputTouch.GetSliding()) {
			Vector2 speedPos = Game.inputTouch.GetTouchPosSpeed();
			nextPos = new Vector3(nextPos.x - speedPos.x, nextPos.y, nextPos.z - speedPos.y);
		}
	}


	private void UpdateFocus() {
		// End of focus if touch sliding or we're near the target position
		if (Game.inputTouch.GetSliding() || Vector3.Distance(transform.position, nextPos) < 0.5f) {
			cameraState = CameraState.FREE;
		}

		Vector3 chrSelectedPos = Game.GetChrSelected().transform.position;
		nextPos = new Vector3(chrSelectedPos.x + focusPosOffset.x, nextPos.y, chrSelectedPos.z + focusPosOffset.z);
		nextSpeed = Time.deltaTime * focusSpeedCoeff;
	}
}
