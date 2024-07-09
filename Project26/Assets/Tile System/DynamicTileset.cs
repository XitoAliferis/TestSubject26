using System.Collections.Generic;
using UnityEngine;

public record DynamicTileset {
	public Quaternion rotationUp;
	public Quaternion rotationRight;
	public Quaternion rotationDown;
	public Quaternion rotationLeft;

	public GameObject constantSegment;
	public GameObject cornerSegment;
	public GameObject wallSegment;
	public GameObject wallFillLeftSegment;
	public GameObject wallFillRightSegment;
	public GameObject overhangSegment;

	public TileDecorationSlotDefinition[] decorationSlots;

	public DynamicTileset(
		GameObject constantSegment,
		GameObject cornerSegment,
		GameObject wallSegment,
		GameObject wallFillLeftSegment,
		GameObject wallFillRightSegment,
		GameObject overhangSegment,
		float rotationUp,
		float rotationRight,
		float rotationDown,
		float rotationLeft,
		TileDecorationSlotDefinition[] decorationSlotDefinitions
	) {
		this.constantSegment = constantSegment;
		this.cornerSegment = cornerSegment;
		this.wallSegment = wallSegment;
		this.wallFillLeftSegment = wallFillLeftSegment;
		this.wallFillRightSegment = wallFillRightSegment;
		this.overhangSegment = overhangSegment;

		this.rotationUp = Quaternion.Euler(0, rotationUp, 0);
		this.rotationRight = Quaternion.Euler(0, rotationRight, 0);
		this.rotationDown = Quaternion.Euler(0, rotationDown, 0);
		this.rotationLeft = Quaternion.Euler(0, rotationLeft, 0);

		this.decorationSlots = decorationSlotDefinitions;
	}

	public DynamicTile Stamp(DynamicTile.NeighborDirections directions) {
		TileDecorationSlot[] newSlots = new TileDecorationSlot[decorationSlots.Length];
		for (int i = 0; i < newSlots.Length; i++) {
			newSlots[i] = decorationSlots[i].AsSlot();
		}
		var newTile = new DynamicTile(this, newSlots);
		newTile.neighbors = directions;
		return newTile;
	}
}