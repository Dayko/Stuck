using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	public int chrIdTarget;

	private float speed = 2.0f;
	private float speedRndCoeff = 6.0f;


	void Start () {
		chrIdTarget = Random.Range(0, 4);
	}
	

	void Update () {
		Vector3 targetPosition = Game.charactersGO[chrIdTarget].transform.position;
		// Move NPC toward character targeted
		if (Vector3.Distance(transform.position, targetPosition) > 2) {
			transform.LookAt(targetPosition);
			transform.Translate(new Vector3(Random.Range(-speedRndCoeff, speedRndCoeff) * Time.deltaTime, 0, (speed + Random.Range(-speedRndCoeff, speedRndCoeff)) * Time.deltaTime));

		// When close enough => attack !
		} else {
			transform.RotateAround(Vector3.up, Mathf.Cos(Time.time * 10) * Time.deltaTime);
		}
	}
}
