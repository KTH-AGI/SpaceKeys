using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    [SerializeField] public GameObject note1;
    [SerializeField] public GameObject note2;
    [SerializeField] public GameObject note3;
    [SerializeField] public GameObject note4;
    [SerializeField] public GameObject note5;

    [SerializeField] public float positionZ = 180;
    [SerializeField] public float positionY = -8;
    [SerializeField] public float creationInterval = 1.0f;

    private List<GameObject> notes = new List<GameObject>();

    public float[] possiblePositionsX;
    private float timer = 0.0f;

    GameObject noteParent;

    // Start is called before the first frame update
    void Start()
    {
        noteParent = new GameObject("notes");
        possiblePositionsX = new float[]{ -12f, -6f, 0, 6f, 12f };
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= creationInterval)
        {
            // Instantiate a new object
            int randomIndex = UnityEngine.Random.Range(0, possiblePositionsX.Length);
            int randomY = UnityEngine.Random.Range(-10, 10);
            Vector3 position = new Vector3(possiblePositionsX[randomIndex], randomY, 180);

            GameObject newNote = null;
            switch (randomIndex)
            {
                case 0: 
                    newNote = Instantiate(note1, position, Quaternion.identity);
                    break;

                case 1: 
                    newNote = Instantiate(note2, position, Quaternion.identity);
                    break;

                case 2: 
                    newNote = Instantiate(note3, position, Quaternion.identity);
                    break;

                case 3: 
                    newNote = Instantiate(note4, position, Quaternion.identity);
                    break;

                case 4: 
                    newNote = Instantiate(note5, position, Quaternion.identity);
                    break;
            }

            newNote.transform.parent = noteParent.transform;
            // Add the new object to the list
            notes.Add(newNote);

            // Reset the timer
            timer = 0.0f;
        }

        if (notes.Count > 0) {
            if (notes[0].transform.position.z < 0 || !notes[0].activeSelf)
        {
                Destroy(notes[0]);
                notes.RemoveAt(0);
            }
        }


    }
}
