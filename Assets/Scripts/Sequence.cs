using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private static float additionalTimeOffset = 0f;


    static Sequence()
    {
        timeOffset = Math.Abs((GenerateObjects.positionZ - playerZ) / MusicObjectMovement.movementSpeed);
        offset = (int)((timeOffset + additionalTimeOffset) / sixteenthTime);

        // Intro
        //sequence[musicNotationToOffsetSample(3)] = new MusicObjectInfo("Asteroid Field", 0.5f);
        sequence[musicNotationToOffsetSample(5)] = new MusicObjectInfo("AsteroidField", 0.5f); //possible to just add an x-value after here if you want
        //sequence[musicNotationToOffsetSample(7)] = new MusicObjectInfo("Supernova", 0.5f);
        sequence[musicNotationToOffsetSample(9)] = new MusicObjectInfo("SpaceProbe", 0.5f);
        sequence[musicNotationToOffsetSample(11)] = new MusicObjectInfo("StarClusterBig", 0.5f);

        //sequence[musicNotationToOffsetSample(13)] = new MusicObjectInfo("StarBb2", 0.5f);
        sequence[musicNotationToOffsetSample(13)] = new MusicObjectInfo("StarBb2", "StarAb2", 0.5f, 0.5f);
        
        sequence[musicNotationToOffsetSample(15)] = new MusicObjectInfo("StarClusterBig", 0.5f);
        sequence[musicNotationToOffsetSample(17)] = new MusicObjectInfo("SpaceProbe", 0.5f);
        

        // Verse 1
        int verse1StartBar = 19;  // 19-26
        writeVerseStartingAtBar(verse1StartBar, 0.3f, 0.3f);
        writeVerseStartingAtBar(verse1StartBar + 4, 0.3f, 0.55f);
        sequence[musicNotationToOffsetSample(verse1StartBar + 8, 1)] = new MusicObjectInfo("StarBb2", 0.5f);

        // Interlude 1
        int interlude1StartBar = 27;  // 27-34
        sequence[musicNotationToOffsetSample(interlude1StartBar)] = new MusicObjectInfo("StarCluster1", 0.5f, -4f);
        sequence[musicNotationToOffsetSample(interlude1StartBar + 2)] = new MusicObjectInfo("SpaceProbe", 0.5f);
        sequence[musicNotationToOffsetSample(interlude1StartBar + 4)] = new MusicObjectInfo("BlackHole", 0.5f);
        sequence[musicNotationToOffsetSample(interlude1StartBar + 6)] = new MusicObjectInfo("SpaceProbe", 0.5f);

        // Chorus 1
        int chorus1StartBar = 35;  // 35-42
        writeChorusStartingAtBar(chorus1StartBar, 0.55f, 0.65f);
        writeChorusStartingAtBar(chorus1StartBar + 4, 0.65f, 0.75f);

        // Interlude 2
        int interlude2StartBar = 43;  // 43-50
        sequence[musicNotationToOffsetSample(interlude2StartBar)] = new MusicObjectInfo("StarCluster1", 0.5f);
        sequence[musicNotationToOffsetSample(interlude2StartBar + 4)] = new MusicObjectInfo("BlackHole", 0.5f);

        // Verse 2
        int verse2StartBar = 51;  // 51-58
        writeVerseStartingAtBar(verse2StartBar, 0.3f, 0.3f);
        writeVerseStartingAtBar(verse2StartBar + 4, 0.3f, 0.55f);
        sequence[musicNotationToOffsetSample(verse2StartBar + 8)] = new MusicObjectInfo("StarBb2", 0.5f);
        
        // Chorus 2
        int chorus2StartBar = 59;  // 59-66
        writeChorusStartingAtBar(chorus2StartBar, 0.55f, 0.65f);
        sequence[musicNotationToOffsetSample(chorus2StartBar)] = new MusicObjectInfo("SpaceProbe", 0.5f);
        writeChorusStartingAtBar(chorus2StartBar + 4, 0.65f, 0.75f);
        // sequence[musicNotationToOffsetSample(chorus2StartBar + 4)] = new MusicObjectInfo("Nebula", 0.5f);

        // Bridge
        int bridgeStartBar = 67;  // 67-82
        sequence[musicNotationToOffsetSample(bridgeStartBar)] = new MusicObjectInfo("StarCluster2", 0.5f);
        sequence[musicNotationToOffsetSample(bridgeStartBar + 4)] = new MusicObjectInfo("SpaceProbe", 0.5f);
        sequence[musicNotationToOffsetSample(bridgeStartBar + 8)] = new MusicObjectInfo("StarCluster2", 0.5f);
        sequence[musicNotationToOffsetSample(bridgeStartBar + 8)] = new MusicObjectInfo("SpaceProbe", 0.5f);
        // sequence[musicNotationToOffsetSample(bridgeStartBar + 12)] = new MusicObjectInfo("WormHole", 0.5f);
        sequence[musicNotationToOffsetSample(bridgeStartBar + 14)] = new MusicObjectInfo("SpaceProbe", 0.5f);

        // Chorus 3
        int chorus3StartBar = 83;  // 83-90
        writeChorusStartingAtBar(chorus3StartBar, 0.55f, 0.65f);
        writeChorusStartingAtBar(chorus3StartBar + 4, 0.65f, 0.75f);
        // sequence[musicNotationToOffsetSample(chorus3StartBar + 4)] = new MusicObjectInfo("Quasar", 0.5f);
        sequence[musicNotationToOffsetSample(interlude2StartBar + 6)] = new MusicObjectInfo("SpaceProbe", 0.5f);

        // Outro
        int outroStartBar = 91;  // 91-99
        sequence[musicNotationToOffsetSample(outroStartBar + 8)] = new MusicObjectInfo("SpaceProbe", 0.5f);
    }

    /* Input time at which the audio should be triggered, returns corresponding offset sample 
    bar, beat and sixteenth are 1-indexed. bar > 0, beat and sixteenth in [1, 4].
    offset is to compensate for the distance between spawning position and player position */
    private static int musicNotationToOffsetSample(int bar, int beat=1, int sixteenth=1)
    {
        // TODO: create assertion for bar, beat and sixteenth
        float timeInSeconds = (float)(bar - 1) * sixteenthTime * 16 + (float)(beat - 1) * sixteenthTime * 4 + (float)(sixteenth - 1) * sixteenthTime;
        int offsetSample = (int)(timeInSeconds / sixteenthTime) - offset;
        if (offsetSample >= 0)
        {
            return offsetSample;
        }
        else
        {
            return lenghtOfSongInSamples;  // TODO: change this later to a better solution
        }
    }

    /* Writes the four bars of verse melody starting from startBar on the Sequence, 
    and applies linear automation of y-coordinate from start to end value */
    private static void writeVerseStartingAtBar(int startBar, float yAutomationStartValue, float yAutomationEndValue)
    {
        float brightnessRange = yAutomationEndValue - yAutomationStartValue;
        float stepSize = 1f/5f;
        sequence[musicNotationToOffsetSample(startBar)] = new MusicObjectInfo("StarBb2", yAutomationStartValue);
        sequence[musicNotationToOffsetSample(startBar, 4)] = new MusicObjectInfo("StarAb2", yAutomationStartValue + stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 1)] = new MusicObjectInfo("StarBb2", yAutomationStartValue + 2f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 2)] = new MusicObjectInfo("StarEb3", yAutomationStartValue + 3f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 3)] = new MusicObjectInfo("StarC3", yAutomationStartValue + 4f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 3, 4)] = new MusicObjectInfo("StarAb2", yAutomationEndValue);
    }

    /* Writes the four bars of chorus melody starting from startBar on the Sequence, 
    and applies linear automation of y-coordinate from start to end value */
    private static void writeChorusStartingAtBar(int startBar, float yAutomationStartValue, float yAutomationEndValue)
    {
        float brightnessRange = yAutomationEndValue - yAutomationStartValue;
        float stepSize = 1f/13f;
        sequence[musicNotationToOffsetSample(startBar)] = new MusicObjectInfo("StarBb2", yAutomationStartValue);
        sequence[musicNotationToOffsetSample(startBar, 1, 4)] = new MusicObjectInfo("StarEb3", yAutomationStartValue + stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar, 3)] = new MusicObjectInfo("StarF3", yAutomationStartValue + 2f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar, 3, 4)] = new MusicObjectInfo("StarEb3", yAutomationStartValue + 3f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 1)] = new MusicObjectInfo("StarDb3", yAutomationStartValue + 4f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 1, 1, 4)] = new MusicObjectInfo("StarBb2", yAutomationStartValue + 5f * stepSize * brightnessRange);

        sequence[musicNotationToOffsetSample(startBar + 2)] = new MusicObjectInfo("StarF3", yAutomationStartValue + 6f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 2, 1, 4)] = new MusicObjectInfo("StarAb3", yAutomationStartValue + 7f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 2, 3)] = new MusicObjectInfo("StarF3", yAutomationStartValue + 8f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 2, 3, 4)] = new MusicObjectInfo("StarEb3", yAutomationStartValue + 9f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 3)] = new MusicObjectInfo("StarDb3", yAutomationStartValue + 10f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 3, 1, 4)] = new MusicObjectInfo("StarBb2", yAutomationStartValue + 11f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 3, 3)] = new MusicObjectInfo("StarAb2", yAutomationStartValue + 12f * stepSize * brightnessRange);
        sequence[musicNotationToOffsetSample(startBar + 3, 3, 4)] = new MusicObjectInfo("StarBb2", yAutomationEndValue);
    }
}