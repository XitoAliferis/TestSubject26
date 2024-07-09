using System;
using System.Collections.Generic;
using UnityEngine;

public class HallwaysAndRoomsFloorGenerator : FloorGenerator<DynamicTile> {
    private DynamicTileset hallwayTileset;
    private DynamicTileset roomTileset;

	RandomIntBetween hallways;
	RandomIntBetween hallwayLength;
	RandomIntBetween rooms;
	RandomIntBetween roomLength;
	RandomIntBetween roomWidth;

    public HallwaysAndRoomsFloorGenerator(DynamicTileset hallwayTileset, DynamicTileset roomTileset, RandomIntBetween hallways, RandomIntBetween hallwayLength, RandomIntBetween rooms, RandomIntBetween roomLength, RandomIntBetween roomWidth) {
		this.hallwayTileset = hallwayTileset;
		this.roomTileset = roomTileset;

		this.hallways = hallways;
		this.hallwayLength = hallwayLength;
		this.rooms = rooms;
		this.roomLength = roomLength;
		this.roomWidth = roomWidth;
	}

	public Floor<DynamicTile> Generate(int seed) {
		System.Random RNG = new System.Random(seed);

		TileBrush hallwaysBrush = new TileBrush(hallwayTileset);
		hallwaysBrush.Set(1,0);

		int hallwayCount = hallways.Sample(RNG);
		int totalRetriesRemaining = 100;
		for (int i = 0; i < hallwayCount; i++) {
			var previousState = new HashSet<Vector2>(hallwaysBrush.Positions);

			Vector2 newHallwayStart = hallwaysBrush.getRandomPosition(RNG);
			int direction = RNG.NextBool()||i==0 ? 1 : -1;
			Func<Vector2, int, bool, TileBrush> orientation = (hallwaysBrush.IsOccupiedAt(newHallwayStart + new Vector2(1, 0)) || hallwaysBrush.IsOccupiedAt(newHallwayStart + new Vector2(-1, 0))) ? hallwaysBrush.DrawHorizontalLine : hallwaysBrush.DrawVerticalLine;
			orientation.Invoke(newHallwayStart, direction * hallwayLength.Sample(RNG), true);

			if (hallwaysBrush.IsOccupiedAt(0,0)) {
				hallwaysBrush.Positions = previousState;
				if (totalRetriesRemaining-- != 0) {
					i--;
				}
			}
		}
		Floor<DynamicTile> floor = hallwaysBrush.Dry("Hallway");
		floor.tiles.Add(new Vector2(0,0), hallwayTileset.Stamp(DynamicTile.NeighborDirections.UP));
		{if (floor.At(1,0) is DynamicTile tile) {tile.MarkNeighbor(DynamicTile.NeighborDirections.DOWN);tile.UnmarkNeighbor(DynamicTile.NeighborDirections.BOTTOM_LEFT | DynamicTile.NeighborDirections.BOTTOM_RIGHT);}}
		{if (floor.At(1,-1) is DynamicTile tile) {tile.UnmarkNeighbor(DynamicTile.NeighborDirections.BOTTOM_RIGHT);}}
		{if (floor.At(1,1) is DynamicTile tile) {tile.UnmarkNeighbor(DynamicTile.NeighborDirections.BOTTOM_LEFT);}}


		totalRetriesRemaining = 100;
		int roomCount = rooms.Sample(RNG);
		for (int i = 0; i < roomCount; i++) {
			Vector2? basePositionMaybe = floor.GetRandomPosition(RNG, "Hallway");
			if (basePositionMaybe == null) {
				Debug.LogError("Failed to find hallway to place room...? What.");
				break;
			}
			DynamicTile baseTile = floor.At(basePositionMaybe.Value);
			Vector2 basePosition = basePositionMaybe.Value;
			
			List<Vector2> validOffsets = new List<Vector2>();
			if (!baseTile.neighbors.HasFlag(DynamicTile.NeighborDirections.UP)) {validOffsets.Add(new Vector2(1, 0));}
			if (!baseTile.neighbors.HasFlag(DynamicTile.NeighborDirections.DOWN)) {validOffsets.Add(new Vector2(-1, 0));}
			if (!baseTile.neighbors.HasFlag(DynamicTile.NeighborDirections.RIGHT)) {validOffsets.Add(new Vector2(0, 1));}
			if (!baseTile.neighbors.HasFlag(DynamicTile.NeighborDirections.LEFT)) {validOffsets.Add(new Vector2(0, -1));}

			if (validOffsets.Count == 0) {
				if (totalRetriesRemaining-- != 0) {
					i--;
				}
				continue;
			}

			Vector2 offset = validOffsets[RNG.Next(validOffsets.Count)];

			if (floor.IsOccupiedAt(basePosition + offset)) {
				if (totalRetriesRemaining-- != 0) {
					i--;
				}
				continue;
			}

			int width = roomWidth.Sample(RNG);
			int length = roomLength.Sample(RNG);
			
			TileBrush room = new TileBrush(roomTileset);

			//Draw center line
			Vector2 position = basePosition + offset;
			for (int drawn = 0; drawn < length; drawn++) {
				if (floor.IsOccupiedAt(position)) {
					break;
				}
				room.Set(position);
				position += offset;
			}

			//get offset rotated left
			Vector2 leftOffset = new Vector2(offset.y, -offset.x);
			
			//Draw width probe, going left
			for (int probeDistance = 0; probeDistance < width; probeDistance++) {
				position = basePosition + offset + leftOffset * (probeDistance + 1);
				if (floor.IsOccupiedAt(position)) { //First position is occupied, end left probe.
					break;
				}
				for (int drawn = 0; drawn < length; drawn++) {
					if (floor.IsOccupiedAt(position)) {
						break;
					}
					room.Set(position);
					position += offset;
				}
			}

			//Draw width probe, going right
			for (int probeDistance = 0; probeDistance < width; probeDistance++) {
				position = basePosition + offset + -leftOffset * (probeDistance + 1);
				if (floor.IsOccupiedAt(position)) { //First position is occupied, end right probe.
					break;
				}
				for (int drawn = 0; drawn < length; drawn++) {
					if (floor.IsOccupiedAt(position)) {
						break;
					}
					room.Set(position);
					position += offset;
				}
			}

			Floor<DynamicTile> roomFloor = room.Dry("Room");
			if (offset == new Vector2(1, 0)) {
				baseTile.MarkNeighbor(DynamicTile.NeighborDirections.UP);
				baseTile.UnmarkNeighbor(DynamicTile.NeighborDirections.TOP_LEFT | DynamicTile.NeighborDirections.TOP_RIGHT);
				roomFloor.At(basePosition + offset).MarkNeighbor(DynamicTile.NeighborDirections.DOWN);
				roomFloor.At(basePosition + offset).UnmarkNeighbor(DynamicTile.NeighborDirections.BOTTOM_LEFT | DynamicTile.NeighborDirections.BOTTOM_RIGHT);
			}
			if (offset == new Vector2(-1, 0)) {
				baseTile.MarkNeighbor(DynamicTile.NeighborDirections.DOWN);
				baseTile.UnmarkNeighbor(DynamicTile.NeighborDirections.BOTTOM_LEFT | DynamicTile.NeighborDirections.BOTTOM_RIGHT);
				roomFloor.At(basePosition + offset).MarkNeighbor(DynamicTile.NeighborDirections.UP);
				roomFloor.At(basePosition + offset).UnmarkNeighbor(DynamicTile.NeighborDirections.TOP_LEFT | DynamicTile.NeighborDirections.TOP_RIGHT);
			}
			if (offset == new Vector2(0, 1)) {
				baseTile.MarkNeighbor(DynamicTile.NeighborDirections.RIGHT);
				baseTile.UnmarkNeighbor(DynamicTile.NeighborDirections.TOP_RIGHT | DynamicTile.NeighborDirections.BOTTOM_RIGHT);
				roomFloor.At(basePosition + offset).MarkNeighbor(DynamicTile.NeighborDirections.LEFT);
				roomFloor.At(basePosition + offset).UnmarkNeighbor(DynamicTile.NeighborDirections.TOP_LEFT | DynamicTile.NeighborDirections.BOTTOM_LEFT);
			}
			if (offset == new Vector2(0, -1)) {
				baseTile.MarkNeighbor(DynamicTile.NeighborDirections.LEFT);
				baseTile.UnmarkNeighbor(DynamicTile.NeighborDirections.TOP_LEFT | DynamicTile.NeighborDirections.BOTTOM_LEFT);
				roomFloor.At(basePosition + offset).MarkNeighbor(DynamicTile.NeighborDirections.RIGHT);
				roomFloor.At(basePosition + offset).UnmarkNeighbor(DynamicTile.NeighborDirections.TOP_RIGHT | DynamicTile.NeighborDirections.BOTTOM_RIGHT);
			}
			// baseTile.FinalizeNeighborsToDecorationSlotValidity();
			// roomFloor.At(basePosition + offset).FinalizeNeighborsToDecorationSlotValidity();
			roomFloor.AddTo(floor);
		}

		return floor;
	}
}