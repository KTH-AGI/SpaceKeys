using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    [SerializeField] public GameObject note;
    [SerializeField] public float positionZ = 180;
    [SerializeField] public float positionY = -8;
    [SerializeField] public float creationInterval = 1.0f;

    private List<GameObject> notes = new List<GameObject>();

    public float[] possiblePositionsX;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        possiblePositionsX = new float[]{ -14.9f, -9f, -3.0f, 3f, 9f, 14.9f };
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= creationInterval)
        {
            // Instantiate a new object
            int randomIndex = Random.Range(0, possiblePositionsX.Length);
            Vector3 position = new Vector3(possiblePositionsX[randomIndex], -8, 180);
            GameObject newNote = Instantiate(note, position, Quaternion.identity);

            // Add the new object to the list
            notes.Add(newNote);

            // Reset the timer
            timer = 0.0f;
        }

        if (notes[0].transform.position.z < 0)
        {
            Destroy(notes[0]);
            notes.RemoveAt(0);
        }


    }
}
