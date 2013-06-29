using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Game {
	
	public static GameLogic gameLogic;
	public static InputTouch inputTouch;
	public static MainCamera mainCamera;

	public enum GameState {
		PLAY,
		PAUSE
	}
	public static GameState gameState;

	
	public enum CharacterType {
		MAGICIAN,
		BUILDER,
		WARRIOR,
		CAMELEON
	};
	public static CharacterType chrTypeSelected;
	public static Dictionary<CharacterType, GameObject> charactersGO;
	public static Dictionary<CharacterType, Character> characters;


	// Register every important section needed
	// -------------------------------------------------
	public static void Init() {
		// Don't forget to add the script component in _GameManager object in Unity Editor Inspector if needed
		gameLogic = GameObject.FindObjectOfType(typeof(GameLogic)) as GameLogic;
		inputTouch = GameObject.FindObjectOfType(typeof(InputTouch)) as InputTouch;
		mainCamera = GameObject.FindObjectOfType(typeof(MainCamera)) as MainCamera;

		gameState = GameState.PLAY;

		chrTypeSelected = CharacterType.MAGICIAN;
		charactersGO = new Dictionary<CharacterType, GameObject>();
		if (!GameObject.FindGameObjectWithTag("Character1")) {
			Debug.LogError("Set tags 'CharacterN' to each GameObject character (with N between 1 and 4)");
		}
		charactersGO.Add(CharacterType.MAGICIAN, GameObject.FindGameObjectWithTag("Character1"));
		charactersGO.Add(CharacterType.BUILDER, GameObject.FindGameObjectWithTag("Character2"));
		charactersGO.Add(CharacterType.WARRIOR, GameObject.FindGameObjectWithTag("Character3"));
		charactersGO.Add(CharacterType.CAMELEON, GameObject.FindGameObjectWithTag("Character4"));
		characters = new Dictionary<CharacterType, Character>();
		characters.Add(CharacterType.MAGICIAN, charactersGO[CharacterType.MAGICIAN].GetComponent<Character>());
		characters.Add(CharacterType.BUILDER, charactersGO[CharacterType.BUILDER].GetComponent<Character>());
		characters.Add(CharacterType.WARRIOR, charactersGO[CharacterType.WARRIOR].GetComponent<Character>());
		characters.Add(CharacterType.CAMELEON, charactersGO[CharacterType.CAMELEON].GetComponent<Character>());
	}



	public static GameObject GetChrGOSelected() {
		return charactersGO[chrTypeSelected];
	}


	public static Character GetChrSelected() {
		return characters[chrTypeSelected];
	}
}





// Only call Game class Init() automatically
// ----------------------------------------------
public class GameManager : MonoBehaviour {
	void Awake() {
		Game.Init();
		DontDestroyOnLoad(gameObject);
	}
}