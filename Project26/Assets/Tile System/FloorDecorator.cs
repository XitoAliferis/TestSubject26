using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FloorDecorator<T> where T : Tile
{
	public Floor<T> Decorate(int seed, Floor<T> floor);
}
