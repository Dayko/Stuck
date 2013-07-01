using UnityEngine;
using System.Collections;

public class NPCSpawner : MonoBehaviour {

	public int firstNightActive;		// Active this spawner at night N

	public GameObject npcToSpawn;

	
	void Update () {
		if (Game.gameLogic.IsNightStarting()) {
			for (int i = 0; i < 10; i++) {
				Instantiate(npcToSpawn, transform.position, Quaternion.identity);
			}
		}
	}
}
