using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter", menuName = "Combat Subsystem/Encounter")]
public class Encounter : ScriptableObject {
	public List<GameObject> enemies;
	public GameObject environment;
	public EnvironmentColorset colorset;
}