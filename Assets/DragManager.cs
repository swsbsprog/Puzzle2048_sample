﻿using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Direction { First, Up,Down, Left, Right, Last}  
public class DragManager : MonoBehaviour
{
    public UnityEvent<Direction> onMove;

    public void OnSwipe(Vector2 swipe)
    {
        Direction dir = GetDirection(swipe);
        print($"{dir}, {swipe.x}, {swipe.y}");
        onMove?.Invoke(dir);
    }

    private Direction GetDirection(Vector2 move)
    {
        if (Math.Abs(move.x) > Math.Abs(move.y))
            return move.x > 0 ? Direction.Right : Direction.Left;   
        else
            return move.y > 0 ? Direction.Up : Direction.Down;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) onMove?.Invoke(Direction.Up);
        if (Input.GetKeyDown(KeyCode.A)) onMove?.Invoke(Direction.Left);
        if (Input.GetKeyDown(KeyCode.S)) onMove?.Invoke(Direction.Down);
        if (Input.GetKeyDown(KeyCode.D)) onMove?.Invoke(Direction.Right);
    }
}
