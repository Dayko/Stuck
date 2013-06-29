using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	private float speedCoeff = 8.0f;

	private bool focusOnSelectedChr;
	private Vector3 focusPosOffset = new Vector3(0, 0, -30);
	private float focusSpeedCoeff = 4.0f;

	private float zoomHeightCurrent;
	private float zoomHeightMin = 100;
	private float zoomHeightMax = 200;



	void Start() {
		zoomHeightCurrent = zoomHeightMin;
	}


	public void FocusOnSelectedChr() {
		focusOnSelectedChr = true;
	}


	void Update() {
		Vector3 targetPos = transform.position;
		float targetSpeed = Time.deltaTime * speedCoeff;

		// Slide camera with finger
		Vector2 speedPos = Game.inputTouch.GetTouchSpeed();
		if (Game.inputTouch.GetSliding() || speedPos.magnitude > 0.5f) {targetPos = new Vector3(targetPos.x - speedPos.x, targetPos.y, targetPos.z - speedPos.y);
			focusOnSelectedChr = false;
		}
		// Focus camera on selected player
		else if (focusOnSelectedChr) {
			Vector3 chrSelectedPos = Game.GetChrSelected().transform.position;
			targetPos = new Vector3(chrSelectedPos.x + focusPosOffset.x, targetPos.y, chrSelectedPos.z + focusPosOffset.z);
			targetSpeed = Time.deltaTime * focusSpeedCoeff;

			if (Vector3.Distance(transform.position, targetPos) < 0.5f)
				focusOnSelectedChr = false;
		}
		
		
		transform.position = Vector3.Lerp(transform.position, targetPos, targetSpeed);
	}
}
