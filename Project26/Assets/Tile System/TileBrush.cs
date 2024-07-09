using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileBrush {
	private DynamicTileset tileset;

	public HashSet<Vector2> Positions { get; set; } = new HashSet<Vector2>();

	public TileBrush(DynamicTileset tileset) {
		this.tileset = tileset;
	}

	public TileBrush Set(Vector2 position, bool existence = true) {
		if (existence) {
			Positions.Add(position);
		} else {
			Positions.Remove(position);
		}
		return this;
	}
	public TileBrush Set(int x, int y, bool existence = true) => Set(new Vector2(x, y), existence);

	public TileBrush DrawVerticalLine(Vector2 start, int offset, bool existence = true) {
		int diff = 1;
		if (offset < 0) {
			diff = -1;
		}
		var current = start;
		for (int i = 0; i <= Mathf.Abs(offset); i++)
		{
			Set(current, existence);
			current.x += diff;
		}
		return this;
	}
	public TileBrush DrawVerticalLine(int startX, int startY, float offset, bool existence = true) => DrawVerticalLine(new Vector2(startX, startY), (int)offset, existence);
	public TileBrush DrawHorizontalLine(Vector2 start, int offset, bool existence = true) {
		int diff = 1;
		if (offset < 0) {
			diff = -1;
		}
		var current = start;
		for (int i = 0; i <= Mathf.Abs(offset); i++)
		{
			Set(current, existence);
			current.y += diff;
		}
		return this;
	}
	public TileBrush DrawHorizontalLine(int startX, int startY, float offset, bool existence = true) => DrawHorizontalLine(new Vector2(startX, startY), (int)offset, existence);

	public TileBrush DrawSquare(Vector2 corner, Vector2 otherCorner, bool existence = true) {
		var difference = otherCorner - corner;
		var diff = new Vector2(1, 1);

		if (difference.x < 0) {
			diff.x = -1;
		}
		if (difference.y < 0) {
			diff.y = -1;
		}

		var current = corner;
		for (int i = 0; i <= Mathf.Abs(difference.x); i++) {
			var thisRow = current;
			for (int j = 0; j <= Mathf.Abs(difference.y); j++) {
				Set(current, existence);
				current += new Vector2(0, diff.y);
			}
			current = thisRow + new Vector2(diff.x, 0);
		}
		return this;
	}
	public TileBrush DrawSquare(int startX, int startY, int endX, int endY, bool existence = true) => DrawSquare(new Vector2(startX, startY), new Vector2(endX, endY), existence);

	public Floor<DynamicTile> Dry(string tag = "") {
		var tiles = new Dictionary<Vector2, DynamicTile>();
		foreach (var position in Positions) {
			DynamicTile.NeighborDirections directions = 0;
			if (Positions.Contains(position + new Vector2(1, 0))) {directions |= DynamicTile.NeighborDirections.UP;}
			if (Positions.Contains(position + new Vector2(0, 1))) {directions |= DynamicTile.NeighborDirections.RIGHT;}
			if (Positions.Contains(position + new Vector2(-1, 0))) {directions |= DynamicTile.NeighborDirections.DOWN;}
			if (Positions.Contains(position + new Vector2(0, -1))) {directions |= DynamicTile.NeighborDirections.LEFT;}
			if (Positions.Contains(position + new Vector2(1, -1))) {directions |= DynamicTile.NeighborDirections.TOP_LEFT;}
			if (Positions.Contains(position + new Vector2(1, 1))) {directions |= DynamicTile.NeighborDirections.TOP_RIGHT;}
			if (Positions.Contains(position + new Vector2(-1, 1))) {directions |= DynamicTile.NeighborDirections.BOTTOM_RIGHT;}
			if (Positions.Contains(position + new Vector2(-1, -1))) {directions |= DynamicTile.NeighborDirections.BOTTOM_LEFT;}
			tiles[position] = tileset.Stamp(directions);
			tiles[position].Tag = tag;
		}
		return new Floor<DynamicTile>(tiles);
	}


	public bool IsOccupiedAt(Vector2 position) => Positions.Contains(position);
	public bool IsOccupiedAt(int x, int y) => Positions.Contains(new Vector2(x, y));

	public Vector2 getRandomPosition(System.Random RNG) => Positions.ElementAt(RNG.Next(Positions.Count));
}