using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFloorBehavior : MonoBehaviour
{
	public DynamicTilesetDefinition hallwayTilesetDefinition;
	public DynamicTilesetDefinition roomTilesetDefinition;

	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> decorsForHallway;
	public List<DecorFloorDecorator<DynamicTile>.DecorTileDecoratorDefinition> decorsForStorage;

	public int start;
	public int count;
	public Vector3 spacing = new Vector3(0, 100, 0);

	void Start() {
		FloorGenerator<DynamicTile> generator = new CompositeFloorGenerator<DynamicTile>(
			new HallwaysAndRoomsFloorGenerator(hallwayTilesetDefinition.CreateTileset(), roomTilesetDefinition.CreateTileset(), 
				new RandomIntBetween(10, 20),
				new RandomIntBetween(4, 15),
				new RandomIntBetween(4, 15),
				new RandomIntBetween(2, 4),
				new RandomIntBetween(1, 3)
			),
			new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(10,40), "Hallway", decorsForHallway.ToArray()),
			new DecorFloorDecorator<DynamicTile>(new RandomIntBetween(10,30), "Room", decorsForStorage.ToArray())
		);

		for (int i = 0; i < count; i++) {
			Floor<DynamicTile> floor = generator.Generate(i + start);
			floor.PlaceAll(spacing * i);
		}
	}
}
