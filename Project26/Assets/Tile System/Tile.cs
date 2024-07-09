#nullable enable

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Tile
{
	public TileDecorationSlot[] DecorationSlots { get; private set; }

	public string Tag { get; set; } = "";

	public abstract GameObject PlaceAt(Vector3 position);

	public Tile(TileDecorationSlot[] decorationSlots) {
		this.DecorationSlots = decorationSlots;
	}

	public TileDecorationSlot? QueryTileDecorations(System.Random RNG, TileDecorationSlot.Type type, TileDecorationSlot.Affinity affinity) {
		var validSlots = DecorationSlots.Where(slot => slot.Placement == type && GetSlotAffinities(slot).HasFlag(affinity));
		if (validSlots.Count() == 0) {
			return null;
		}
		if (validSlots.Count() == 1) {
			return validSlots.First();
		}
		var index = RNG.Next(validSlots.Count());
		return validSlots.ElementAt(index);
	}

	protected virtual TileDecorationSlot.Affinity GetSlotAffinities(TileDecorationSlot slot) {
		TileDecorationSlot.Affinity affinity = 0;
		affinity |= slot.Placement switch {
			TileDecorationSlot.Type.FLOOR_CENTER => TileDecorationSlot.Affinity.IS_FLOORED,
			TileDecorationSlot.Type.FLOOR_CORNER => TileDecorationSlot.Affinity.IS_FLOORED,
			TileDecorationSlot.Type.WALL_CENTER => TileDecorationSlot.Affinity.IS_NOT_FLOORED,
			_ => TileDecorationSlot.Affinity.NONE
		};
		
		if (slot.Directions.HasFlag(TileDecorationSlot.Edge.NORTH)) {affinity |= TileDecorationSlot.Affinity.IS_NORTH;}
		if (slot.Directions.HasFlag(TileDecorationSlot.Edge.EAST)) {affinity |= TileDecorationSlot.Affinity.IS_EAST;}
		if (slot.Directions.HasFlag(TileDecorationSlot.Edge.SOUTH)) {affinity |= TileDecorationSlot.Affinity.IS_SOUTH;}
		if (slot.Directions.HasFlag(TileDecorationSlot.Edge.WEST)) {affinity |= TileDecorationSlot.Affinity.IS_WEST;}
		
		if (!slot.Occupied) {affinity |= TileDecorationSlot.Affinity.NOT_OCCUPIED;}

		return affinity;
	}
}
