using UnityEngine;
using System.Collections;

public class Builder : MonoBehaviour {

	private Character parentChr;
	private bool isInSpecialMode = false;




	void Awake() {
		parentChr = gameObject.GetComponent<Character>();
	}


	void OnGUI() {
		if (!parentChr.IsSelected())
			return;

		if (!isInSpecialMode)
			OnGUINormal();
		else
			OnGUISpecialMode();
	}


	// GUI Update when selected and in normal mode
	// ------------------------------------------------
	private void OnGUINormal() {
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 100), "Build !")) {
			parentChr.EnterSpecialMode();
			isInSpecialMode = true;
		}
	}


	// GUI Update when selected and in special mode
	// ------------------------------------------------
	private void OnGUISpecialMode() {
		if (GUI.Button(new Rect(150, Screen.height - 80, 60, 60), "< Return")) {
			parentChr.LeaveSpecialMode();
			isInSpecialMode = false;
		}
	}


	void Update () {
	
	}
}
