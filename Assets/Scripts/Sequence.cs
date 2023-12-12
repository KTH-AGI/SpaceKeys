using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence
{
    public static MusicObjectInfo[] sequence = new MusicObjectInfo[2000];

    static Sequence()
    {
        // Intro
        sequence[1] = new MusicObjectInfo("StarBb2", 0.5f);
        sequence[2] = new MusicObjectInfo("StarAb2", 0.5f);
        sequence[3] = new MusicObjectInfo("StarEb3", 0.5f);
        sequence[4] = new MusicObjectInfo("StarBb2", 0.5f);
        //sequence[31] = new MusicObjectInfo("Asteroid Field", 0.5f);
        //sequence[63] = new MusicObjectInfo("Asteroid Field", 0.5f);
        //sequence[95] = new MusicObjectInfo("Supernova", 0.5f);
        //sequence[159] = new MusicObjectInfo("Star Cluster Big 1", 0.5f);
        //sequence[223] = new MusicObjectInfo("Star Cluster Big 2", 0.5f);

        // Verse
        sequence[286] = new MusicObjectInfo("StarBb2", 0.3f );
        sequence[299] = new MusicObjectInfo("StarAb2", 0.3f );
        sequence[303] = new MusicObjectInfo("StarBb2", 0.3f );
        sequence[319] = new MusicObjectInfo("StarEb3", 0.3f );
        sequence[335] = new MusicObjectInfo("StarDb3", 0.3f );
        sequence[347] = new MusicObjectInfo("StarAb2", 0.3f );

        sequence[351] = new MusicObjectInfo("StarBb2", 0.3f );
        sequence[363] = new MusicObjectInfo("StarAb2", 0.35f );
        sequence[367] = new MusicObjectInfo("StarBb2", 0.38f );
        sequence[383] = new MusicObjectInfo("StarEb3", 0.45f );
        sequence[399] = new MusicObjectInfo("StarDb3", 0.5f );
        sequence[411] = new MusicObjectInfo("StarAb2", 0.55f );

        sequence[415] = new MusicObjectInfo("StarBb2", 0.58f );
    }

}
