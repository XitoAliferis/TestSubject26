using UnityEngine;

public class TileEnvironmentGenerator : MonoBehaviour {
	public DynamicTilesetDefinition tileset;

	void Start() {
		new TileBrush(tileset.CreateTileset())
			.DrawSquare(new Vector2(1, -1), new Vector2(-1, 6))
			.DrawSquare(new Vector2(1, -4), new Vector2(-4, -2))
			.DrawSquare(new Vector2(-1, 7), new Vector2(4, 9))
		.Dry().PlaceAll(new Vector3(0,0,0), "Battle Environment").transform.parent = transform;
	}
}