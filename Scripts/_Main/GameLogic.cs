﻿using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	// Day-Night time cycle
	private int dayCurrent;
	private bool isDayStarting;
	private bool isNight;
	private bool isNightStarting;
	private float timeCurrent;
	private float timeStartNight = 50;
	private float timeEndNight = 75;

	// Timeline
	private Vector2 tlSize = new Vector2(300, 10);
	private Vector2 tlOffset = new Vector2(0, 20);


	void Start() {
		dayCurrent = 1;
		timeCurrent = 40; // tmp
	}


	// A bright new day is starting
	// --------------------------------------------
	private void StartNewDay() {
		if (!isNight)
			return;
		isNight = false;
		isDayStarting = true;
		dayCurrent++;
		timeCurrent = 0;
		Debug.Log("Day " + dayCurrent + " start !");
	}


	// Night falling, starting attack
	// --------------------------------------------
	private void StartNight() {
		if (isNight)
			return;
		isNight = true;
		isNightStarting = true;
		Debug.Log("Night is falling, watch out !");
	}


	public bool IsDayStarting() {
		return isDayStarting;
	}


	public bool IsNight() {
		return isNight;
	}


	public bool IsNightStarting() {
		return isNightStarting;
	}


	void Update() {
		if (Game.gameState != Game.GameState.PLAY)
			return;

		UpdateTime();
	}


	// Update day-night cycle
	// --------------------------------------------
	private void UpdateTime() {
		isDayStarting = false; // True during only one frame
		isNightStarting = false; // True during only one frame


		timeCurrent += Time.deltaTime;

		if (!isNight && timeCurrent >= timeStartNight) {
			StartNight();
		} else if (isNight && timeCurrent >= timeEndNight) {
			StartNewDay();
		}
	}




	void OnGUI() {
		
		// Display Timeline
		Rect tlRect = new Rect(Screen.width / 2 - tlSize.x / 2 + tlOffset.x, tlOffset.y, tlSize.x, tlSize.y);
		GUI.HorizontalSlider(tlRect, timeCurrent, 0, timeEndNight);
	}

}
