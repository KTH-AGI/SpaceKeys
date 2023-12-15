using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLight : MonoBehaviour
{
    int gridSize = CircularGrid.gridSize;
    float radius = CircularGrid.radius;
    private bool hasMissed = false;

    [SerializeField] Material missedNoteMaterial;
    [SerializeField] Material lightMaterial;
    static float lineWidth; // = 0.5f;
    static float zSpawn;
    static float playerPosition;
    GameObject circularLight;
    
    // Event for when the player misses a note
    public static event Action<Vector3> OnMissNote;

    //LineRenderer lineRenderer;

    void Start()
    {
        //lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineWidth = NoteLight.lineWidth;
        zSpawn = NoteLight.zSpawn;
        playerPosition = NoteLight.playerPosition;
        createRoundLight();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float zPosition = transform.position.z;
        if (zPosition < zSpawn && zPosition > playerPosition) //25 is the player position, need to change this later
        {
            circularLight.SetActive(true);
            LineRenderer lineRenderer = circularLight.GetComponent<LineRenderer>();
            for (int i = 0; i < gridSize; i++)
            {
                float angle = i * (360f / gridSize);
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;


                Vector3 point = new Vector3(x, y, zPosition);
                lineRenderer.SetPosition(i, point);
                if (i == 0) { lineRenderer.SetPosition(gridSize, point); }
            }
        }
        else {
            if (Mathf.Abs(zPosition - playerPosition)< 0.1 ) { Debug.Log("Time at player is: " + Time.time); }
            circularLight.SetActive(false);
            if (zPosition < playerPosition && !hasMissed) { 
                
                // Trigger the OnMissWithPosition event
                OnMissNote?.Invoke(this.transform.position);
                
                hasMissed = true;
            }
        }
        
    }


    void createRoundLight()
    {
        circularLight = new GameObject("Light");
        LineRenderer lineRenderer = circularLight.AddComponent<LineRenderer>();
        circularLight.transform.parent = gameObject.transform;
        //lineRenderer.useWorldSpace = false;
        lineRenderer.material = lightMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = gridSize + 1;

        for (int i = 0; i < gridSize; i++)
        {
            float angle = i * (360f / gridSize);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;


            Vector3 point = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, point);
            if (i == 0) { lineRenderer.SetPosition(gridSize, point); }
        }
        circularLight.SetActive(false);

    }

}
