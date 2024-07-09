using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDecorationSlotDefinition : MonoBehaviour {
	public TileDecorationSlot.Type placement;
	public TileDecorationSlot.Edge directions;

	public TileDecorationSlot AsSlot() {
		return new TileDecorationSlot(placement, directions, transform.position, transform.rotation);
	}
}