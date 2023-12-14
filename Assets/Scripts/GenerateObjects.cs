using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateObjects : MonoBehaviour
{
    [SerializeField] public GameObject StarAb2;
    [SerializeField] public GameObject StarBb2;
    [SerializeField] public GameObject StarC3;
    [SerializeField] public GameObject StarDb3;
    [SerializeField] public GameObject StarEb3;
    [SerializeField] public GameObject StarF3;
    [SerializeField] public GameObject StarAb3;

    [SerializeField] public GameObject Supernova;
    [SerializeField] public GameObject AsteroidField;
    [SerializeField] public GameObject BlackHole;
    [SerializeField] public GameObject StarClusterBig;
    [SerializeField] public GameObject StarCluster1;
    [SerializeField] public GameObject StarCluster2;
    [SerializeField] public GameObject Nebula;
    [SerializeField] public GameObject SpaceProbe;

    MusicObjectInfo[] sequence = Sequence.sequence;

    [SerializeField] public static float positionZ = 145;  // 180
    // [SerializeField] public static float positionY = -8;
    private float r;
    
    public static float creationInterval = 0.125f;
    
    private List<GameObject> notes = new List<GameObject>();
    private int index = 0;
    GameObject noteParent;

    void Start()
    {
        noteParent = new GameObject("notes");
        InvokeRepeating("generateSequence", 1f, creationInterval);
        Invoke("playBackgroundAudio", 1f);
        r = CircularGrid.radius;
    }

    void Update()
    {   
        deleteNotes();
    }

    void deleteNotes()
    {
        if (notes.Count > 0)
        {
            if (notes[0].transform.position.z < 0 || !notes[0].activeSelf)
            {
                Destroy(notes[0]);
                notes.RemoveAt(0);
            }
        }
    }

    GameObject objectSwitch(string noteName, float positionX, float positionY)
    {
        GameObject newNote = null;
        Vector3 objPosition;

        switch (noteName)
        {  
            case "StarAb2":
                objPosition = new Vector3(-12, positionY, positionZ);
                newNote = Instantiate(StarAb2, objPosition, Quaternion.identity);
                return newNote;

            case "StarBb2":
                objPosition = new Vector3(-8, positionY, positionZ);
                newNote = Instantiate(StarBb2, objPosition, Quaternion.identity);
                return newNote;

            case "StarC3":
                objPosition = new Vector3(-4, positionY, positionZ);
                newNote = Instantiate(StarC3, objPosition, Quaternion.identity);
                return newNote;

            case "StarDb3":
                objPosition = new Vector3(0, positionY, positionZ);
                newNote = Instantiate(StarDb3, objPosition, Quaternion.identity);
                return newNote;

            case "StarEb3":
                objPosition = new Vector3(4, positionY, positionZ);
                newNote = Instantiate(StarEb3, objPosition, Quaternion.identity);
                return newNote;

            case "StarF3":
                objPosition = new Vector3(8, positionY, positionZ);
                newNote = Instantiate(StarF3, objPosition, Quaternion.identity);
                return newNote;

            case "StarAb3":
                objPosition = new Vector3(12, positionY, positionZ);
                newNote = Instantiate(StarAb3, objPosition, Quaternion.identity);
                return newNote;



            case "AsteroidField":
                objPosition = new Vector3(5.2f, positionY, positionZ);
                newNote = Instantiate(AsteroidField, objPosition, Quaternion.identity);
                return newNote;

            case "Nebula":
                objPosition = new Vector3(positionX, positionY, positionZ);
                newNote = Instantiate(Nebula, objPosition, Quaternion.identity);
                return newNote;

            case "StarClusterBig":
                objPosition = new Vector3(positionX, positionY, positionZ);
                newNote = Instantiate(StarClusterBig, objPosition, Quaternion.identity);
                return newNote;

            case "StarCluster1":
                objPosition = new Vector3(positionX, positionY, positionZ);
                newNote = Instantiate(StarCluster1, objPosition, Quaternion.identity);
                return newNote;

            case "StarCluster2":
                objPosition = new Vector3(positionX, positionY, positionZ);
                newNote = Instantiate(StarCluster2, objPosition, Quaternion.identity);
                return newNote;

            case "BlackHole":
                objPosition = new Vector3(positionX, positionY, positionZ);
                newNote = Instantiate(BlackHole, objPosition, Quaternion.identity);
                return newNote;

            case "SpaceProbe":
                objPosition = new Vector3(positionX, positionY, positionZ);
                newNote = Instantiate(SpaceProbe, objPosition, Quaternion.identity);
                return newNote;

            case "End":
                AudioManager.instance.CleanUp();
                break;

            default:
                break;

        }
        return newNote;
    }






    void generateSequence()
    {
        if (sequence[index] != null)
        {
            MusicObjectInfo musicObject = sequence[index];
            float positionY = (musicObject.getYValue(0) * 2f - 1f) * r;
            float positionX = musicObject.getXValue(0);
            GameObject newNote = objectSwitch(musicObject.getName(0), positionX, positionY);
            
            if (newNote != null) {
                newNote.transform.parent = noteParent.transform;
                notes.Add(newNote);
            }
            
            if (musicObject.isTwo())
            {
                Debug.Log("TWO MUSIC NOTES GENERATED");
                float positionY2 = (musicObject.getYValue(1) * 2f - 1f) * r;
                float positionX2 = musicObject.getXValue(1);
                GameObject newNote2 = objectSwitch(musicObject.getName(1), positionX2, positionY2);
                newNote2.transform.parent = noteParent.transform;
                notes.Add(newNote2);
            }
            Debug.Log("Time for note is: " + Time.time);
        }
        index++;
    }

    void playBackgroundAudio()
    {  
        AudioManager.instance.StartBackgroundMusic();
    }


}
