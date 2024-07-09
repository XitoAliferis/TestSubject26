using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccentManager : MonoBehaviour
{
	public DynamicTilesetDefinition lightHallwayTilesetDefinition;
	public DynamicTilesetDefinition lightStorageRoomTilesetDefinition;


	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> decorsForHallwayLight;
	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> decorsForStorageLight;

	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> decorsHallwayProgressionLight;

	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> easyEnemies;
	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> mediumEnemies;
	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> hardEnemies;

	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> easyObstacles;
	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> mediumObstacles;
	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> hardObstacles;

	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> boons;

	public GameObject lightEnvironment;
	public EnvironmentColorset lightColorset;

	private List<Floor> generators = null;

	private struct Floor {
		public FloorGenerator<DynamicTile> generator;
		public GameObject environment;
		public EnvironmentColorset colorset;
	}

	void Awake() {
		var lightHallway = lightHallwayTilesetDefinition.CreateTileset();
		var storageRoom = lightStorageRoomTilesetDefinition.CreateTileset();

		generators = new List<Floor>() {

			new Floor() {
				generator = new CompositeFloorGenerator<DynamicTile>(
					new HallwaysAndRoomsFloorGenerator(lightHallway, storageRoom,
					 hallways: new RandomIntBetween(5, 8),
					 hallwayLength: new RandomIntBetween(3, 10),
					 rooms: new RandomIntBetween(2, 4),
					 roomLength: new RandomIntBetween(2, 3),
					 roomWidth: new RandomIntBetween(1, 2)
					),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(1,1), "Hallway", decorsHallwayProgressionLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(3,10), "Hallway", decorsForHallwayLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(2,5), "Room", decorsForStorageLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(2,3), "Room", boons.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(2,3), "", easyEnemies.ToArray())
				),
				environment = lightEnvironment,
				colorset = lightColorset
			},

			new Floor() {
				generator = new CompositeFloorGenerator<DynamicTile>(
					new HallwaysAndRoomsFloorGenerator(lightHallway, storageRoom,
					 hallways: new RandomIntBetween(6, 10),
					 hallwayLength: new RandomIntBetween(4, 10),
					 rooms: new RandomIntBetween(4, 6),
					 roomLength: new RandomIntBetween(2, 3),
					 roomWidth: new RandomIntBetween(1, 2)
					),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(1,1), "Hallway", decorsHallwayProgressionLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(5,12), "Hallway", decorsForHallwayLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(3,5), "Room", decorsForStorageLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(2,4), "Room", boons.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(2,4), "", easyEnemies.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(1,2), "", easyObstacles.ToArray())
				),
				environment = lightEnvironment,
				colorset = lightColorset
			},

			new Floor() {
				generator = new CompositeFloorGenerator<DynamicTile>(
					new HallwaysAndRoomsFloorGenerator(lightHallway, storageRoom,
					 hallways: new RandomIntBetween(7, 10),
					 hallwayLength: new RandomIntBetween(5, 11),
					 rooms: new RandomIntBetween(5, 10),
					 roomLength: new RandomIntBetween(2, 5),
					 roomWidth: new RandomIntBetween(1, 2)
					),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(1,1), "Hallway", decorsHallwayProgressionLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(10,15), "Hallway", decorsForHallwayLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(5,12), "Room", decorsForStorageLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(3,7), "Room", boons.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(3,6), "", easyEnemies.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(1,3), "", easyObstacles.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(1,2), "", mediumObstacles.ToArray())
				),
				environment = lightEnvironment,
				colorset = lightColorset
			},

			new Floor() {
				generator = new CompositeFloorGenerator<DynamicTile>(
					new HallwaysAndRoomsFloorGenerator(lightHallway, storageRoom,
					 hallways: new RandomIntBetween(10, 20),
					 hallwayLength: new RandomIntBetween(5, 14),
					 rooms: new RandomIntBetween(6, 15),
					 roomLength: new RandomIntBetween(2, 6),
					 roomWidth: new RandomIntBetween(1, 4)
					),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(1,1), "Hallway", decorsHallwayProgressionLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(10,25), "Hallway", decorsForHallwayLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(6,17), "Room", decorsForStorageLight.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(6,20), "Room", boons.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(4,10), "", easyEnemies.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(1,5), "", easyObstacles.ToArray()),
					new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(2,10), "", mediumObstacles.ToArray())
				),
				environment = lightEnvironment,
				colorset = lightColorset
			}

		};
	}

	public GameObject Generate(int floor, int seed) {
		System.Random RNG = new System.Random(seed);
		int floorSeed = RNG.Next();
		for (int i = 0; i < floor; i++) { floorSeed = RNG.Next(); }
		if (generators.Count <= floor) { floor = generators.Count - 1; } //Loop the last floor forever
		var floorObject = generators[floor].generator.Generate(floorSeed).PlaceAll(Vector3.zero, "Floor " + floor);
		Instantiate(generators[floor].environment, floorObject.transform);
		generators[floor].colorset.Apply();
		return floorObject;
	}
}
