#nullable enable
using System;
using Unity.VisualScripting;
using UnityEngine;

public class DecorFloorDecorator<T> : FloorDecorator<T> where T : Tile
{
	[Serializable]
	public record DecorTileDecoratorDefinition {
		public GameObject prefab;
		public TileDecorationSlot.Type type;
		public TileDecorationSlot.Affinity affinities;
		[Min(1)]
		public int priority;

		public DecorTileDecoratorDefinition(GameObject prefab, TileDecorationSlot.Type type, TileDecorationSlot.Affinity affinities, int priority) {
			this.prefab = prefab;
			this.type = type;
			this.priority = priority;
			this.affinities = affinities;
		}
    }

	private RandomIntBetween amount;
	private string targetTag;
	private DecorTileDecoratorDefinition[] items;

	public DecorFloorDecorator(RandomIntBetween amount, string targetTag, params DecorTileDecoratorDefinition[] items) {
		this.amount = amount;
		this.targetTag = targetTag;
		this.items = items;
	}

    public Floor<T> Decorate(int seed, Floor<T> floor)
    {
        System.Random RNG = new System.Random(seed);

		Floor<T> isolatedFloor = floor.IsolateTag(targetTag);

		int count = amount.Sample(RNG);
		int totalPriority = 0;
		foreach (var item in items) {
			totalPriority += item.priority;
		}
		for (int i = 0; i < count; i++) {
			int roll = RNG.Next(0, totalPriority + 1);
			foreach (var item in items)
			{
				if (roll <= item.priority) {
					var slot = isolatedFloor.QueryTileDecorations(RNG, item.type, item.affinities);
					if (slot != null) {
						slot.Occupied = true;
						slot.prefab = item.prefab;
					}
					break;
				} else{
					roll -= item.priority;
				}
			}
		}
		return floor;
    }
}