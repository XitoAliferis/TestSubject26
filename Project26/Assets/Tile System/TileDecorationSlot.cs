using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDecorationSlot
{
	[Flags]
	public enum Affinity {
		NONE =                0b000000000, //Always has none
		WITH_WALL =           0b000000001,
		WITHOUT_WALL =        0b000000010,
		IS_NORTH =            0b000000100,
		IS_EAST =             0b000001000,
		IS_SOUTH =            0b000010000,
		IS_WEST =             0b000100000,
		IS_FLOORED =          0b001000000,
		IS_NOT_FLOORED =      0b010000000,
		NOT_OCCUPIED =        0b100000000
	}
	[Flags]
	public enum Edge {
		NONE   = 0b00000000,
		NORTH  = 0b00000001,
		EAST   = 0b00000010,
		SOUTH  = 0b00000100,
		WEST   = 0b00001000
	}
	public enum Type {
		WALL_CENTER, FLOOR_CORNER, FLOOR_CENTER
	}
	public Type Placement { get; private set; }
	public Edge Directions { get; private set; }
	public Vector3 Position { get; private set; }
	public Quaternion Rotation { get; private set; }
	public bool Occupied { get; set; }
	public GameObject prefab = null;

	public TileDecorationSlot(Type placement, Edge directions, Vector3 position, Quaternion rotation) {
		this.Placement = placement;
		this.Directions = directions;
		this.Position = position;
		this.Rotation = rotation;
		this.Occupied = false;
	}

	public GameObject Place(Vector3 offset) {
		return GameObject.Instantiate(prefab, Position + offset, Rotation);
	}
}
