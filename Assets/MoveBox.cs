using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    public Vector2Int coord = new(-1, -1);
    public int number;
    public TextMesh text;

    internal void SetNumber(int startValue)
    {
        number = startValue;
        text.text = number.ToString();
    }

    internal void Move(int x, int y, bool removeOld = true)
    {
        Vector2 movePosition = new (x, y);
        transform.position = movePosition;
        if(removeOld)
            TileManager.instance.grid[coord.x, coord.y] = null;
        TileManager.instance.grid[x, y] = this;
        coord.x = x;
        coord.y = y;
    }
}
