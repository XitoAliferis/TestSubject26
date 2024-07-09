using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTile : Tile
{
	[Flags]
	public enum NeighborDirections {
		UP =           0b00000001,
		DOWN =         0b00000010,
		LEFT =         0b00000100,
		RIGHT =        0b00001000,
		TOP_RIGHT =    0b00010000,
		TOP_LEFT =     0b00100000,
		BOTTOM_RIGHT = 0b01000000,
		BOTTOM_LEFT =  0b10000000
	}
	public NeighborDirections neighbors = 0;

	[Flags]
	public enum HolepunchDirections
	{
		UP =           0b00000001,
		DOWN =         0b00000010,
		LEFT =         0b00000100,
		RIGHT =        0b00001000,
	}
	public HolepunchDirections holepunchDirections = 0;

	DynamicTileset tileset;
	
	public DynamicTile(DynamicTileset tileset, TileDecorationSlot[] decorationSlots) : base(decorationSlots) {
		this.tileset = tileset;
	}

	public override GameObject PlaceAt(Vector3 position)
	{
		GameObject me = new GameObject(Tag.Length>0? Tag + " " : "" + "Dynamic Tile with " + neighbors.ToString() + "");
		me.transform.position = position;
		GameObject.Instantiate(tileset.constantSegment, position, Quaternion.identity, me.transform);

		if (!neighbors.HasFlag(NeighborDirections.UP) && !holepunchDirections.HasFlag(HolepunchDirections.UP)) {GameObject.Instantiate(tileset.wallSegment, position, tileset.rotationUp, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.RIGHT) && !holepunchDirections.HasFlag(HolepunchDirections.RIGHT)) {GameObject.Instantiate(tileset.wallSegment, position, tileset.rotationRight, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.DOWN) && !holepunchDirections.HasFlag(HolepunchDirections.DOWN)) {GameObject.Instantiate(tileset.wallSegment, position, tileset.rotationDown, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.LEFT) && !holepunchDirections.HasFlag(HolepunchDirections.LEFT)) {GameObject.Instantiate(tileset.wallSegment, position, tileset.rotationLeft, me.transform);}

		if (!neighbors.HasFlag(NeighborDirections.UP) && !neighbors.HasFlag(NeighborDirections.LEFT)) {GameObject.Instantiate(tileset.cornerSegment, position, tileset.rotationUp, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.UP) && !neighbors.HasFlag(NeighborDirections.RIGHT)) {GameObject.Instantiate(tileset.cornerSegment, position, tileset.rotationRight, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.DOWN) && !neighbors.HasFlag(NeighborDirections.LEFT)) {GameObject.Instantiate(tileset.cornerSegment, position, tileset.rotationLeft, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.DOWN) && !neighbors.HasFlag(NeighborDirections.RIGHT)) {GameObject.Instantiate(tileset.cornerSegment, position, tileset.rotationDown, me.transform);}

		if (!neighbors.HasFlag(NeighborDirections.UP) && neighbors.HasFlag(NeighborDirections.LEFT)) {GameObject.Instantiate(tileset.wallFillLeftSegment, position, tileset.rotationUp, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.RIGHT) && neighbors.HasFlag(NeighborDirections.UP)) {GameObject.Instantiate(tileset.wallFillLeftSegment, position, tileset.rotationRight, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.DOWN) && neighbors.HasFlag(NeighborDirections.RIGHT)) {GameObject.Instantiate(tileset.wallFillLeftSegment, position, tileset.rotationDown, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.LEFT) && neighbors.HasFlag(NeighborDirections.DOWN)) {GameObject.Instantiate(tileset.wallFillLeftSegment, position, tileset.rotationLeft, me.transform);}

		if (!neighbors.HasFlag(NeighborDirections.UP) && neighbors.HasFlag(NeighborDirections.RIGHT)) {GameObject.Instantiate(tileset.wallFillRightSegment, position, tileset.rotationUp, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.RIGHT) && neighbors.HasFlag(NeighborDirections.DOWN)) {GameObject.Instantiate(tileset.wallFillRightSegment, position, tileset.rotationRight, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.DOWN) && neighbors.HasFlag(NeighborDirections.LEFT)) {GameObject.Instantiate(tileset.wallFillRightSegment, position, tileset.rotationDown, me.transform);}
		if (!neighbors.HasFlag(NeighborDirections.LEFT) && neighbors.HasFlag(NeighborDirections.UP)) {GameObject.Instantiate(tileset.wallFillRightSegment, position, tileset.rotationLeft, me.transform);}

		if (neighbors.HasFlag(NeighborDirections.UP | NeighborDirections.LEFT) && !neighbors.HasFlag(NeighborDirections.TOP_LEFT)) {GameObject.Instantiate(tileset.overhangSegment, position, tileset.rotationUp, me.transform);}
		if (neighbors.HasFlag(NeighborDirections.UP | NeighborDirections.RIGHT) && !neighbors.HasFlag(NeighborDirections.TOP_RIGHT)) {GameObject.Instantiate(tileset.overhangSegment, position, tileset.rotationRight, me.transform);}
		if (neighbors.HasFlag(NeighborDirections.DOWN | NeighborDirections.RIGHT) && !neighbors.HasFlag(NeighborDirections.BOTTOM_RIGHT)) {GameObject.Instantiate(tileset.overhangSegment, position, tileset.rotationDown, me.transform);}
		if (neighbors.HasFlag(NeighborDirections.DOWN | NeighborDirections.LEFT) && !neighbors.HasFlag(NeighborDirections.BOTTOM_LEFT)) {GameObject.Instantiate(tileset.overhangSegment, position, tileset.rotationLeft, me.transform);}

		foreach (TileDecorationSlot slot in DecorationSlots) {
			if (slot.prefab == null) {continue;}
			var added = slot.Place(position);
			added.transform.parent = me.transform;
		}

		return me;
	}

	public void MarkNeighbor(NeighborDirections direction) {
		neighbors |= direction;
	}
	public void UnmarkNeighbor(NeighborDirections direction) {
		neighbors &= ~direction;
	}

    protected override TileDecorationSlot.Affinity GetSlotAffinities(TileDecorationSlot slot)
    {
        var affinities = base.GetSlotAffinities(slot);

		if (slot.Directions.HasFlag(TileDecorationSlot.Edge.NORTH) && !neighbors.HasFlag(NeighborDirections.UP)) {affinities |= TileDecorationSlot.Affinity.WITH_WALL;}
		if (slot.Directions.HasFlag(TileDecorationSlot.Edge.EAST) && !neighbors.HasFlag(NeighborDirections.RIGHT)) {affinities |= TileDecorationSlot.Affinity.WITH_WALL;}
		if (slot.Directions.HasFlag(TileDecorationSlot.Edge.SOUTH) && !neighbors.HasFlag(NeighborDirections.DOWN)) {affinities |= TileDecorationSlot.Affinity.WITH_WALL;}
		if (slot.Directions.HasFlag(TileDecorationSlot.Edge.WEST) && !neighbors.HasFlag(NeighborDirections.LEFT)) {affinities |= TileDecorationSlot.Affinity.WITH_WALL;}

		//Add WITHOUT_WALL for configurations we could reasonably grantee
		affinities |= slot.Directions switch {
			TileDecorationSlot.Edge.NORTH => neighbors.HasFlag(NeighborDirections.UP)? TileDecorationSlot.Affinity.WITHOUT_WALL : TileDecorationSlot.Affinity.NONE,
			TileDecorationSlot.Edge.EAST => neighbors.HasFlag(NeighborDirections.RIGHT)? TileDecorationSlot.Affinity.WITHOUT_WALL : TileDecorationSlot.Affinity.NONE,
			TileDecorationSlot.Edge.SOUTH => neighbors.HasFlag(NeighborDirections.DOWN)? TileDecorationSlot.Affinity.WITHOUT_WALL : TileDecorationSlot.Affinity.NONE,
			TileDecorationSlot.Edge.WEST => neighbors.HasFlag(NeighborDirections.LEFT)? TileDecorationSlot.Affinity.WITHOUT_WALL : TileDecorationSlot.Affinity.NONE,
			TileDecorationSlot.Edge.NORTH | TileDecorationSlot.Edge.EAST => neighbors.HasFlag(NeighborDirections.UP | NeighborDirections.RIGHT | NeighborDirections.TOP_RIGHT)? TileDecorationSlot.Affinity.WITHOUT_WALL : TileDecorationSlot.Affinity.NONE,
			TileDecorationSlot.Edge.NORTH | TileDecorationSlot.Edge.WEST => neighbors.HasFlag(NeighborDirections.UP | NeighborDirections.LEFT | NeighborDirections.TOP_LEFT)? TileDecorationSlot.Affinity.WITHOUT_WALL : TileDecorationSlot.Affinity.NONE,
			TileDecorationSlot.Edge.SOUTH | TileDecorationSlot.Edge.EAST => neighbors.HasFlag(NeighborDirections.DOWN | NeighborDirections.RIGHT | NeighborDirections.BOTTOM_RIGHT)? TileDecorationSlot.Affinity.WITHOUT_WALL : TileDecorationSlot.Affinity.NONE,
			TileDecorationSlot.Edge.SOUTH | TileDecorationSlot.Edge.WEST => neighbors.HasFlag(NeighborDirections.DOWN | NeighborDirections.LEFT | NeighborDirections.BOTTOM_LEFT)? TileDecorationSlot.Affinity.WITHOUT_WALL : TileDecorationSlot.Affinity.NONE,
			_ => TileDecorationSlot.Affinity.NONE
		};

		return affinities;
    }
}