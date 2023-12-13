using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence
{
    private static int lenghtOfSongInSamples = 2000;
    public static MusicObjectInfo[] sequence = new MusicObjectInfo[lenghtOfSongInSamples];
    private static float playerZ = 25f;  //GameObject.FindWithTag("Player").transform.position.z;;

    // The offset in number of seconds
    private static float timeOffset;
    // / interval between samples
    public static int offset;
    private static float sixteenthTime = GenerateObjects.creationInterval;
    private static float additionalTimeOffset = 12f;

    void Start() {
        timeOffset = (GenerateObjects.positionZ - playerZ) * MusicObjectMovement.movementSpeed;
        offset = (int)((timeOffset + additionalTimeOffset) / sixteenthTime);
    }

    static Sequence()
    {
        // Intro
        // sequence[musicNotationToOffsetSample(3, 1)] = new MusicObjectInfo("Asteroid Field", 0.5f);
        //sequence[musicNotationToOffsetSample(5, 1)] = new MusicObjectInfo("Asteroid Field", 0.5f);
        //sequence[musicNotationToOffsetSample(7, 1)] = new MusicObjectInfo("Supernova", 0.5f);
        //sequence[musicNotationToOffsetSample(11, 1)] = new MusicObjectInfo("Star Cluster Big 1", 0.5f);
        //sequence[musicNotationToOffsetSample(15, 1)] = new MusicObjectInfo("Star Cluster Big 2", 0.5f);

        // Verse
        sequence[musicNotationToOffsetSample(19, 1)] = new MusicObjectInfo("StarBb2", 0.3f);
        sequence[musicNotationToOffsetSample(19, 4)] = new MusicObjectInfo("StarAb2", 0.3f);
        sequence[musicNotationToOffsetSample(20, 1)] = new MusicObjectInfo("StarBb2", 0.3f);
        sequence[musicNotationToOffsetSample(21, 1)] = new MusicObjectInfo("StarEb3", 0.3f);
        sequence[musicNotationToOffsetSample(22, 1)] = new MusicObjectInfo("StarC3", 0.3f);
        sequence[musicNotationToOffsetSample(22, 4)] = new MusicObjectInfo("StarAb2", 0.3f);

        sequence[musicNotationToOffsetSample(23, 1)] = new MusicObjectInfo("StarBb2", 0.3f);
        sequence[musicNotationToOffsetSample(23, 4)] = new MusicObjectInfo("StarAb2", 0.35f);
        sequence[musicNotationToOffsetSample(24, 1)] = new MusicObjectInfo("StarBb2", 0.38f);
        sequence[musicNotationToOffsetSample(25, 1)] = new MusicObjectInfo("StarEb3", 0.45f);
        sequence[musicNotationToOffsetSample(26, 1)] = new MusicObjectInfo("StarC3", 0.5f);
        sequence[musicNotationToOffsetSample(26, 4)] = new MusicObjectInfo("StarAb2", 0.55f);

        sequence[musicNotationToOffsetSample(27, 1)] = new MusicObjectInfo("StarBb2", 0.58f);
    }

    /* Input time at which the audio should be triggered, returns corresponding offset sample 
    bar, beat and sixteenth are 1-indexed 
    offset is to compensate for the distance between spawning position and player position */
    private static int musicNotationToOffsetSample(int bar, int beat, int sixteenth = 1)
    {
        float timeInSeconds = (float)(bar - 1) * sixteenthTime * 16 + (float)(beat - 1) * sixteenthTime * 4 + (float)(sixteenth - 1) * sixteenthTime;
        int offsetSample = (int) (timeInSeconds / sixteenthTime) - offset - 1;
        if (offsetSample >= 0)
        {
            return offsetSample;
        }
        else
        {
            return lenghtOfSongInSamples;  // TODO: change this later to a better solution
        }
    }

}
