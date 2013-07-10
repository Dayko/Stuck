using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Magician : MonoBehaviour {
	
	private Character parentChr;
	private bool isInSpecialMode = false;

	private class Trail {
		public GameObject go;
		public LineRenderer line;
		public int vertexNb;
		public Vector3 lastPos;
		public Color color1;
		public Color color2;
	}
	private List<Trail> trails;
	private Trail trailLast;

	private int trailVertexNbMax = 50;
	private float trailDistanceMin = 0.02f;



	void Awake() {
		parentChr = gameObject.GetComponent<Character>();
		trails = new List<Trail>();
		trailLast = null;
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
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 100), "Magic !")) {
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
		if (!isInSpecialMode)
			return;


		if (Game.inputTouch.GetTouchDown()) {
			CreateTrail();
		}

		if (Game.inputTouch.GetTouch()) {
			UpdateLastTrail();
		}

		if (Game.inputTouch.GetTouchUp()) {
			trailLast = null;
		}

		UpdateTrails();
	}





	private void CreateTrail() {
		Camera cam = Game.mainCamera.camera;
		Vector3 pos = cam.ScreenToWorldPoint(new Vector3(Game.inputTouch.GetTouchPosStart().x, Game.inputTouch.GetTouchPosStart().y, cam.nearClipPlane + 1));

		Trail trail = new Trail();
		trail.go = Instantiate(Resources.Load("Feedbacks/MagicianTrail"), pos, Quaternion.identity) as GameObject;
		trail.line = trail.go.GetComponent<LineRenderer>() as LineRenderer;
		trail.vertexNb = 0;
		trail.lastPos = pos;
		trail.color1 = Color.blue;
		trail.color2 = Color.blue;

		trailLast = trail;
		trails.Add(trail);
	}


	private void UpdateLastTrail() {
		if (trailLast == null)
			return;

		Camera cam = Game.mainCamera.camera;
		Vector3 pos = cam.ScreenToWorldPoint(new Vector3(Game.inputTouch.GetTouchPos().x, Game.inputTouch.GetTouchPos().y, cam.nearClipPlane + 1));
		// Add a position if far enough from last point
		if (Vector3.Distance(trailLast.lastPos, pos) > trailDistanceMin && trailLast.vertexNb < trailVertexNbMax) {
			trailLast.line.SetVertexCount(trailLast.vertexNb + 1);
			trailLast.line.SetPosition(trailLast.vertexNb, pos);
			trailLast.vertexNb += 1;
			trailLast.lastPos = pos;
		}
	}


	private void UpdateTrails() {
		for (int i = 0; i < trails.Count; i++) {
			if (trails[i] != trailLast) {
				Trail t = trails[i];
				t.color1 = Color.Lerp(t.color1, Color.clear, Time.deltaTime);
				t.color2 = Color.Lerp(t.color1, Color.clear, Time.deltaTime);
				t.line.SetColors(t.color1, t.color2);

				if (t.color1.a < 0.01f && t.color2.a < 0.01f) {
					Destroy(trails[i].go);
					trails.RemoveAt(i);
					i--;
				}
			}
		}
	}
}
