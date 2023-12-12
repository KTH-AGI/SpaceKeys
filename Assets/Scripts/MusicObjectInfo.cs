using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicObjectInfo
{
    public string noteName;
    public float value;

    public string noteName2 = null;
    public float value2;

    public MusicObjectInfo(string musicObject, float value)
    {
        this.noteName = musicObject;
        this.value = value;
    }

    public string getName()
    {
        return this.noteName;
    }

    public MusicObjectInfo(string musicObject, float value, string musicObject2, float value2)
    {
        this.noteName = musicObject;
        this.noteName2 = musicObject2;
        this.value = value;
        this.value2 = value2;
    }
}