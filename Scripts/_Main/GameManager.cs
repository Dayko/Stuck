using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Game {
	// Main components
	public static GameLogic gameLogic;
	public static InputTouch inputTouch;
	public static MainCamera mainCamera;

	// Game state
	public enum GameState {
		PLAY,
		PAUSE
	}
	public static GameState gameState;

	// Characters accessors
	public const int CHR_MAGICIAN	= 0;
	public const int CHR_BUILDER	= 1;
	public const int CHR_WARRIOR	= 2;
	public const int CHR_CAMELEON 	= 3;
	public static int chrSelected;

	public static List<GameObject> charactersGO;
	public static List<Character> characters;


	// Register every important section needed
	// -------------------------------------------------
	public static void Init() {
		// Don't forget to add the script component in _GameManager object in Unity Editor Inspector if needed
		gameLogic = GameObject.FindObjectOfType(typeof(GameLogic)) as GameLogic;
		inputTouch = GameObject.FindObjectOfType(typeof(InputTouch)) as InputTouch;
		mainCamera = GameObject.FindObjectOfType(typeof(MainCamera)) as MainCamera;

		gameState = GameState.PLAY;

		chrSelected = 0; // Magician
		charactersGO = new List<GameObject>();
		if (!GameObject.FindGameObjectWithTag("Character1")) {
			Debug.LogError("Set tags 'CharacterN' to each GameObject character (with N between 1 and 4)");
		}
		charactersGO.Add(GameObject.FindGameObjectWithTag("Character1"));
		charactersGO.Add(GameObject.FindGameObjectWithTag("Character2"));
		charactersGO.Add(GameObject.FindGameObjectWithTag("Character3"));
		charactersGO.Add(GameObject.FindGameObjectWithTag("Character4"));
		characters = new List<Character>();
		characters.Add(charactersGO[CHR_MAGICIAN].GetComponent<Character>());
		characters.Add(charactersGO[CHR_BUILDER].GetComponent<Character>());
		characters.Add(charactersGO[CHR_WARRIOR].GetComponent<Character>());
		characters.Add(charactersGO[CHR_CAMELEON].GetComponent<Character>());
	}



	public static GameObject GetChrGOSelected() {
		return charactersGO[chrSelected];
	}


	public static Character GetChrSelected() {
		return characters[chrSelected];
	}
}





// Only call Game class Init() automatically
// ----------------------------------------------
public class GameManager : MonoBehaviour {
	void Awake() {
		Game.Init();
		Application.runInBackground = true;
	}
}