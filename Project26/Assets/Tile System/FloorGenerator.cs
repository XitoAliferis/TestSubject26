using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FloorGenerator<T> where T : Tile
{
    public Floor<T> Generate(int seed);
}
