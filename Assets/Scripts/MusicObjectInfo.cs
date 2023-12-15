using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicObjectInfo
{
    public string[] noteNames = new string[2];
    Position[] positions = new Position[2];

    public MusicObjectInfo(string musicObjectName, float y_value, float x_value=0)
    {
        this.noteNames[0] = musicObjectName;
        positions[0] = new Position(x_value, y_value);
    }

    public bool isTwo()
    {
        return !string.IsNullOrEmpty(noteNames[1]);
    }

    public string getName(int index)
    {
        return this.noteNames[index];
    }

    public float getXValue(int index)
    {
        return this.positions[index].x;
    }

    public float getYValue(int index)
    {
        return this.positions[index].y;
    }

    public MusicObjectInfo(string musicObject, string musicObject2, float y_value, float y_value2, float x_value = 0, float x_value2 = 0)
    {
        this.noteNames[0] = musicObject;
        positions[0] = new Position(x_value, y_value);

        this.noteNames[1] = musicObject2;
        positions[1] = new Position(x_value2, y_value2);
    }

    public struct Position
    {
        public float x;
        public float y;

        public Position(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}