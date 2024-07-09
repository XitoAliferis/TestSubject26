using System;
using System.Collections.Generic;
using UnityEngine;
using static Combatant;

public class CombatManager : MonoBehaviour {

	private EnvironmentColorset previousColorset;

	public GameObject playerPrefab;
	public GameObject cameraPrefab;
	
	[HideInInspector]
	public List<Combatant> enemies;
	[HideInInspector]
	public List<MonsterUIController> enemyUIControllers;
	[HideInInspector]
	public Combatant player;

	public void Begin(Encounter encounter) {
		previousColorset = EnvironmentColorset.FromCurrent();

		GameObject environment = Instantiate(encounter.environment, this.transform);
		encounter.colorset.Apply();

		Transform positionToSpawnPlayer = environment.transform.Find("Player Spawn").transform;

		Transform positionToSpawnEnemySingular = environment.transform.Find("Enemy Spawn Singular").transform;

		Transform positionToSpawnEnemyDuoA = environment.transform.Find("Enemy Spawn Duo A").transform;
		Transform positionToSpawnEnemyDuoB = environment.transform.Find("Enemy Spawn Duo B").transform;

		Transform positionToSpawnEnemyTrioA = environment.transform.Find("Enemy Spawn Trio A").transform;
		Transform positionToSpawnEnemyTrioB = environment.transform.Find("Enemy Spawn Trio B").transform;
		Transform positionToSpawnEnemyTrioC = environment.transform.Find("Enemy Spawn Trio C").transform;

		//Camera positions (see below after all the processing stuff)
		cameraPositionDefault = environment.transform.Find("Camera Position Standard").transform;
		cameraPositionFocusEnemy = environment.transform.Find("Camera Position Focus Enemy").transform;	

		lerpCamera = Instantiate(cameraPrefab, cameraPositionDefault.transform.position, cameraPositionDefault.transform.rotation, this.transform).GetComponent<LerpCamera>();
		SetCameraPosition(CameraPosition.DEFAULT);	

		player = Instantiate(playerPrefab, positionToSpawnPlayer, false).GetComponent<Combatant>();

		if (encounter.enemies.Count == 1) {
			enemies.Add(Instantiate(encounter.enemies[0], positionToSpawnEnemySingular, false).GetComponent<Combatant>());
		} else if (encounter.enemies.Count == 2) {
			enemies.Add(Instantiate(encounter.enemies[0], positionToSpawnEnemyDuoA, false).GetComponent<Combatant>());
			enemies.Add(Instantiate(encounter.enemies[1], positionToSpawnEnemyDuoB, false).GetComponent<Combatant>());
		} else if (encounter.enemies.Count == 3) {
			enemies.Add(Instantiate(encounter.enemies[0], positionToSpawnEnemyTrioA, false).GetComponent<Combatant>());
			enemies.Add(Instantiate(encounter.enemies[1], positionToSpawnEnemyTrioB, false).GetComponent<Combatant>());
			enemies.Add(Instantiate(encounter.enemies[2], positionToSpawnEnemyTrioC, false).GetComponent<Combatant>());
		} else {
			Debug.LogError("Encounter has too many enemies we never accounted for four!!");
		}

		player.Manager = this;
		for (int i = 0; i < enemies.Count; i++) {
			Combatant enemy = enemies[i];
			enemy.Manager = this;
			var ui = enemy.GetComponent<MonsterUIController>();
			enemyUIControllers.Add(ui);
			ui.index = i;
		}

		//TODO: intro AnimatedAction
		PlayerAction();
	}

	private void SelectMove(Combatant combatant, Action<Move, List<Combatant>> then) {
		AnimatedAction selection = combatant.DecideNextMove();
		selection.onComplete = () => {
			then.Invoke(combatant.GetDecidedMove(), combatant.GetDecidedTargets());
		};
		selection.Start();
	}
	private void BeginMove(Move move, Combatant host, List<Combatant> targets, Action then) {
		currentMoveHost = host;
		runAfterMoveEnd = then;
		currentMove = move.Perform(host, targets).GetEnumerator();
		ProcessCurrentMoveContinuation();
	}
	private IEnumerator<AnimatedAction> currentMove;
	private Combatant currentMoveHost;
	private Action runAfterMoveEnd;
	private void ProcessCurrentMoveContinuation() { //Welcome to functional programming hell but worse because its not actually functional.
		if (player.GetHealth() == 0) { //Check if the player is dead and end if so.
			player.PlayAnimation(AnimationType.DEAD);
			EndDueToPlayerDeath();
			return;
		}
		for (int i = 0; i < enemies.Count; i++) { //Check if any monster died, and remove it from the list. If it happened to be the thing currently attacking, end the attack.
			Combatant enemy = enemies[i];
			if (enemy.GetHealth() == 0) {
				enemies.RemoveAt(i);
				enemyUIControllers.RemoveAt(i);
				enemy.PlayAnimation(AnimationType.DEAD);
				i--;
				if (enemies.Count == 0) { //Oh everyone died.
					EndDueToEnemiesCleared();
					return;
				}
				if (currentMoveHost == enemy) { //The thing currently attacking died, skip the rest of the move.
					runAfterMoveEnd.Invoke();
					return;
				}
			}
		}
		if (currentMove.MoveNext()) {
			AnimatedAction action = currentMove.Current;
			action.onComplete = () => {
				ProcessCurrentMoveContinuation();
			};
			action.Start();
		} else {
			runAfterMoveEnd.Invoke();
		}
	}
	private void PlayerAction() {
		SetCameraPosition(CameraPosition.DEFAULT);
		SelectMove(player, (move, targets) => {
			BeginMove(move, player, targets, () => {
				EnemyAction(0);
			});
		});
	}
	private void EnemyAction(int index) {
		SetCameraPosition(CameraPosition.FOCUS_ENEMY);
		SelectMove(enemies[index], (move, targets) => {
			BeginMove(move, enemies[index], targets, () => {
				if (index < enemies.Count - 1) {
					EnemyAction(index + 1);
				} else {
					PlayerAction();
				}
			});
		});
	}

	private void EndDueToPlayerDeath() {
		Debug.Log("Done: Player death");
		PurgeChildren();
		previousColorset.Apply();
	}
	private void EndDueToEnemiesCleared() {
		Debug.Log("Done: All enemies cleared");
		PurgeChildren();
		previousColorset.Apply();
		GameManager.Instance.EndEncounter();
	}


	private Transform cameraPositionDefault;
	private Transform cameraPositionFocusEnemy;
	private LerpCamera lerpCamera;
	public enum CameraPosition {
		DEFAULT, FOCUS_ENEMY
	}
	public void SetCameraPosition(CameraPosition position) {
		lerpCamera.target = position switch {
			CameraPosition.DEFAULT => cameraPositionDefault,
			CameraPosition.FOCUS_ENEMY => cameraPositionFocusEnemy,
			_ => cameraPositionDefault
		};
	}

	internal void PurgeChildren()
	{
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}
}