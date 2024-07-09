using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tileset", menuName = "Tile Subsystem/Dynamic Tileset")]
public class DynamicTilesetDefinition : ScriptableObject
{
	public GameObject prefab;

	public string constantSegmentName = "Constant";
	public string cornerSegmentName = "Corner";
	public string wallFillLeftSegmentName = "Wall Fill Left";
	public string wallFillRightSegmentName = "Wall Fill Right";
	public string wallMiddleSegmentName = "Wall Middle";
	public string overhangSegmentName = "Overhang";

	[Range(-180, 180)]
	public float rotationUp = -90;

	[Range(-180, 180)]
	public float rotationRight = 0;

	[Range(-180, 180)]
	public float rotationLeft = -180;

	[Range(-180, 180)]
	public float rotationDown = 90;

	public DynamicTileset CreateTileset() {
		return new DynamicTileset(
			prefab.transform.Find(constantSegmentName).gameObject,
			prefab.transform.Find(cornerSegmentName).gameObject,
			prefab.transform.Find(wallMiddleSegmentName).gameObject,
			prefab.transform.Find(wallFillLeftSegmentName).gameObject,
			prefab.transform.Find(wallFillRightSegmentName).gameObject,
			prefab.transform.Find(overhangSegmentName).gameObject,
			rotationUp,
			rotationRight,
			rotationDown,
			rotationLeft,
			prefab.GetComponentsInChildren<TileDecorationSlotDefinition>()
		);
	}
}
