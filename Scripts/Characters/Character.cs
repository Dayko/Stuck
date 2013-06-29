using UnityEngine;
using System.Collections;

// ******************************************************************************************
// * Main class for every main characters
// ******************************************************************************************
public class Character : MonoBehaviour {

	// Global informations
	public enum State {
		Idle,							// Wait
		Sleep,							// Sleep on ground
		Move,							// Is moving from his position
		SpecialMode,					// Each character in his own special mode
	};
	public State state;
	/*private State _state;
	public State state {
		get { return _state; }
		set { _state = value; }
	}*/
	public Game.CharacterType type;		// Profession


	// Move State - Position
	private CharacterController controller;
	private Vector3 moveTargetPos;
	private float moveDistanceToStartSpeedDown = 0.1f;
	private float moveMaxSpeed = 10f;
	private float moveCurrentSpeed;

	private Transform cameraTransform; // tmp


	
	void Start () {
		state = State.Idle;

		controller = GetComponent<CharacterController>();
		cameraTransform = Camera.main.transform;
		moveTargetPos = transform.position;
	}


	// Is it the character with the actual focus ?
	// ----------------------------------------------
	public bool IsSelected() {
		return (Game.chrSelected == type);
	}


	// Switch the selected character
	// ----------------------------------------------
	public void SwitchFocusCharacter(Game.CharacterType newChrSelected) {
		// TODO : check if current selected is not locked
		Game.chrSelected = newChrSelected;
	}


	// Set position to move
	// ----------------------------------------------
	public void SetTargetPos(Vector3 pos) {
		if (EnterState(State.Move)) {
			moveTargetPos = pos;
		}
	}


	void OnGUI() {
		int btnSize = Screen.height / 8;
		Rect iconPosition = new Rect(0, 40 + type.GetHashCode() * (btnSize + 10), btnSize + (IsSelected() ? 10 : 0), btnSize);
		if (GUI.Button(iconPosition, Resources.Load("GUI/Textures/IconCharacter" + type.GetHashCode()) as Texture)) {
			SwitchFocusCharacter(type);
		}

		// Specific OnGUI for each state
		switch (state) {
			case State.Sleep:
				// TODO : Add "Zzz" near character icon (and also over his head, but maybe not by passing in OnGUI)

			default:
				//Debug.LogWarning("Character State '" + state + "' not defined in OnGUI()");
				break;
		}
	}




	// Check transition from a state to another
	// ----------------------------------------------
	public bool EnterState(State newState) {
		// Get current state
		switch (state) {
			case State.Idle:
				// Accept whatever the new state is
				state = newState;
				return true;

			case State.Move:
				// Accept whatever the new state is
				state = newState;
				return true;

			case State.SpecialMode:
				// NEVER allow to change state while his in special mode
				return false;

			default:
				Debug.LogWarning("Character State '" + state + "' not defined in EnterState()");
				return false;
		}
	}


	// (Un)Lock character in his special mode
	// ------------------------------------------------
	public void EnterSpecialMode() {
		EnterState(Character.State.SpecialMode);
	}

	public void LeaveSpecialMode() {
		state = State.Idle;
	}


	
	// Main update
	// ----------------------------------------------
	void Update() {
		UpdateMouse();
		UpdateCamera();

		switch (state) {
			case State.Idle:
			case State.Move:
				UpdateStateMovement();
				break;
		}
	}


	// Moving update
	// ----------------------------------------------
	private void UpdateStateMovement() {
		if (state == State.Move) {
			// Check distance
			float distance = Vector3.Distance(transform.position, moveTargetPos);
			if (distance < moveDistanceToStartSpeedDown) {
				state = State.Idle;
				moveCurrentSpeed = 0;
			} else if (distance < (3 * moveDistanceToStartSpeedDown) && moveCurrentSpeed >= .5)
				moveCurrentSpeed -= 0.1f;
			else if (distance < (3 * moveDistanceToStartSpeedDown))
				state = State.Idle;
			else
				moveCurrentSpeed += 0.1f;
		} else if (moveCurrentSpeed > 0)
			moveCurrentSpeed -= 0.1f;
		else
			return;

		// Look target
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveTargetPos - transform.position), Time.deltaTime * 8);

		// Move to target
		moveCurrentSpeed = Mathf.Clamp01(moveCurrentSpeed);
		Vector3 normal = (moveTargetPos - transform.position);
		controller.Move(normal.normalized * moveCurrentSpeed * moveMaxSpeed * Time.deltaTime);

	}


	// Update for mouse input
	// ----------------------------------------------
	private void UpdateMouse() {
		if (Game.inputTouch.GetSingleTap()) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			// Temporary limit raycast to avoid clicking simultaneously on a button and to move a character
			if (Physics.Raycast(ray, out hit) && Input.mousePosition.x > 100 && Input.mousePosition.y > 100) {
				if (hit.collider.transform == transform) {
					SwitchFocusCharacter(type);
				} else if (hit.collider.tag != "Player" && IsSelected()) { // Move selected character
					SetTargetPos(hit.point);
				}
			}
		}
	}


	private void UpdateCamera() {
		// For selected character only
		if (IsSelected()) {
			// Update camera position
			cameraTransform.position = Vector3.Lerp(cameraTransform.position, new Vector3(transform.position.x, cameraTransform.position.y, transform.position.z - 30), Time.deltaTime * 4);

		}
	}
}