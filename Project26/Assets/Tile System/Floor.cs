#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Floor<T> where T : Tile
{
	private const float TILE_SIZE = 6f;
	public Dictionary<Vector2, T> tiles = new Dictionary<Vector2, T>();
	
	public Floor(Dictionary<Vector2, T> tiles) {
		this.tiles = tiles;
	}
	public Floor() {
	}

	public GameObject PlaceAll(Vector3 at, string name = "Floor") {
		GameObject me = new GameObject(name);
		me.transform.position = at;
		foreach (var tile in tiles) {
			GameObject placed = tile.Value.PlaceAt(at + new Vector3(tile.Key.y * TILE_SIZE, 0, tile.Key.x * TILE_SIZE));
			placed.transform.parent = me.transform;
		}
		return me;
	}

	public void PasteOnTop<O>(Floor<O> other) where O : T {
		foreach (var tile in other.tiles) {
			tiles[tile.Key] = tile.Value;
		}
	}
	public void PasteUnder<O>(Floor<O> other) where O : T {
		foreach (var tile in other.tiles) {
			if (!tiles.ContainsKey(tile.Key)) {
				tiles[tile.Key] = tile.Value;
			}
		}
	}

	public void AddTo(Floor<T> other) {
		other.PasteOnTop(this);
	}


	public bool IsOccupiedAt(int x, int y) => tiles.ContainsKey(new Vector2(x, y));
	public bool IsOccupiedAt(Vector2 position) => tiles.ContainsKey(position);

    public T? At(int x, int y) => At(new Vector2(x, y));
	public T? At(Vector2 position)
	{
		if (!tiles.ContainsKey(position)) return null;
		return tiles[position];
	}

	public Vector2? GetRandomPosition(System.Random RNG, string tag = "") {
		var matchedTiles = tiles.Where(tile => tile.Value.Tag.StartsWith(tag));

		if (matchedTiles.Count() == 0) {
			return null;
		}
		return matchedTiles.ElementAt(RNG.Next(matchedTiles.Count())).Key;
	}

	public Floor<T> IsolateTag(string tag) {
		return new Floor<T>(tiles.Where(tile => tile.Value.Tag.StartsWith(tag)).ToDictionary(tile => tile.Key, tile => tile.Value));
	}

	public TileDecorationSlot? QueryTileDecorations(System.Random RNG, TileDecorationSlot.Type type, TileDecorationSlot.Affinity affinity) {
		List<TileDecorationSlot> validSlots = new List<TileDecorationSlot>();
		foreach (var tile in tiles) {
			var slot = tile.Value.QueryTileDecorations(RNG, type, affinity);
			if (slot != null) {
				validSlots.Add(slot);
			}
		}
		if (validSlots.Count == 0) {
			return null;
		}
		return validSlots.ElementAt(Random.Range(0, validSlots.Count));
	}
	public List<TileDecorationSlot> QueryTileDecorationsAll(System.Random RNG, TileDecorationSlot.Type type, TileDecorationSlot.Affinity affinity) {
		List<TileDecorationSlot> validSlots = new List<TileDecorationSlot>();
		foreach (var tile in tiles) {
			var slot = tile.Value.QueryTileDecorations(RNG, type, affinity);
			if (slot != null) {
				validSlots.Add(slot);
			}
		}
		return validSlots;
	}
}