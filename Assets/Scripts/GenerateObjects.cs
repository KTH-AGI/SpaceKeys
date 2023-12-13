using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] public GameObject StarGb3;

    MusicObjectInfo[] sequence = Sequence.sequence;

    [SerializeField] public static float positionZ = 145;  // 180
    // [SerializeField] public static float positionY = -8;
    public float positionY;
    private float r;

    [SerializeField] public static float creationInterval = 0.125f;
    private List<GameObject> notes = new List<GameObject>();


    public float[] possiblePositionsX;
    private float timer = 0.0f;
    private int index = 0;


    GameObject noteParent;

    void Start()
    {
        noteParent = new GameObject("notes");
        r = CircularGrid.radius;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= creationInterval)
        {
            if (sequence[index] != null)
            {
                GameObject newNote = objectSwitch(sequence[index]);
                newNote.transform.parent = noteParent.transform;
                notes.Add(newNote);
            }
            
            // Reset the timer
            timer = 0.0f;
            index++;
        }
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

    GameObject objectSwitch(MusicObjectInfo musicObject)
    {
        GameObject newNote = null;
        Vector3 objPosition;
        positionY = (musicObject.value * 2f - 1f) * r;

        switch (musicObject.getName())
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

            case "StarGb3":
                objPosition = new Vector3(12, positionY, positionZ);
                newNote = Instantiate(StarGb3, objPosition, Quaternion.identity);
                return newNote;
        }
        return newNote;
    }
}
