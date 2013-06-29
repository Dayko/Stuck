using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	void Awake() {
	}


	void Update() {
		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), Space.World);
	}
}
