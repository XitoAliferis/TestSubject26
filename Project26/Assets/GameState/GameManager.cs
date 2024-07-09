using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	private void Awake() {
		Instance = this;
	}

	public AccentManager accentManager;
	public CombatManager combatManager;
	public GameObject player;
	public GameObject playerCamera;
	
	public Vector3 playerInitialPosition;

	public int Seed { get; set; } = 0; 

	private GameObject floor;


	public int CurrentFloor { get; private set; } = 0;

	public void SwitchFloorTo(int level) {
		CurrentFloor = level;
		if (floor != null) {
			Destroy(floor);
		}
		floor = accentManager.Generate(level, Seed);
		player.SetActive(false);
		player.transform.position = playerInitialPosition;
		player.transform.rotation = Quaternion.identity;
		player.SetActive(true);
	}

	public void SwitchToNextFloor() {
		CurrentFloor++;
		SwitchFloorTo(CurrentFloor);
	}

	public void ResetRun() {
		Seed = Random.Range(0, int.MaxValue);
		CurrentFloor = 0;
	}


	private bool inBattle = false;
	public void BeginEncounter(Encounter encounter) {
		if (inBattle) {return;}
		floor?.SetActive(false);
		player.SetActive(false);
		playerCamera.SetActive(false);
		inBattle = true;
		combatManager.Begin(encounter);
	}

	public void EndEncounter() {
		floor?.SetActive(true);
		player.SetActive(true);
		playerCamera.SetActive(true);
		inBattle = false;
		// combatManager.PurgeChildren();
	}






	void Start() {
		ResetRun();
		SwitchFloorTo(0);
	}
}
