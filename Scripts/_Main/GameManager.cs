using UnityEngine;
using System.Collections;

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
	public static CharacterType chrSelected = CharacterType.MAGICIAN;


	// Register every important section needed
	// -------------------------------------------------
	public static void Init() {
		// Don't forget to add the script component in _GameManager object in Unity Editor Inspector if needed
		gameLogic = GameObject.FindObjectOfType(typeof(GameLogic)) as GameLogic;
		inputTouch = GameObject.FindObjectOfType(typeof(InputTouch)) as InputTouch;
		mainCamera = GameObject.FindObjectOfType(typeof(MainCamera)) as MainCamera;

		gameState = GameState.PLAY;
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