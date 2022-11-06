using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell
{
    private float x, y;

    public BoardCell(float x, float y)
    {
        this.X = x;
        this.Y = y;
    }

    public float X { get => x; set => x = value; }
    public float Y { get => y; set => y = value; }
}
