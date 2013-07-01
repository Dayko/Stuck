using UnityEngine;
using System.Collections;

public class NightEffect : MonoBehaviour {

	private float effectSpeed = 1.0f;

	private Color colorDay;
	public Color colorNight = Color.black;


	void Start() {
		if (light)
			colorDay = light.color;
		else if (renderer && renderer.material)
			colorDay = renderer.material.color;
	}


	void Update() {
		if (light)
			UpdateLight();
		else if (renderer && renderer.material)
			UpdateTexture();
	}


	// Color transition for light component
	// --------------------------------------------
	private void UpdateLight() {
		if (Game.gameLogic.IsNight()) {
			light.color = Color.Lerp(light.color, colorNight, effectSpeed * Time.deltaTime);
		} else {
			light.color = Color.Lerp(light.color, colorDay, effectSpeed * Time.deltaTime);
		}
	}


	// Color transition for texture material
	// --------------------------------------------
	private void UpdateTexture() {
		if (Game.gameLogic.IsNight()) {
			renderer.material.color = Color.Lerp(renderer.material.color, colorNight, effectSpeed * Time.deltaTime);
		} else {
			renderer.material.color = Color.Lerp(renderer.material.color, colorDay, effectSpeed * Time.deltaTime);
		}
	}
}
