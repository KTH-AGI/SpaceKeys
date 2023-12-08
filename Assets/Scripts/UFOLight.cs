using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOLight : MonoBehaviour
{
    int gridSize = CircularGrid.gridSize;
    float radius = CircularGrid.radius;

    [SerializeField] Material lightMaterial;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float lineWidth = 0.5f;
    [SerializeField] int zSpawn = 50;
    [SerializeField] int playerPosition = 25;
    GameObject circularLight;

    //LineRenderer lineRenderer;

    void Start()
    {
        //lineRenderer = gameObject.AddComponent<LineRenderer>();
        createRoundLight();
    }

    // Update is called once per frame
    void Update()
    {
 
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

        float zPosition = transform.position.z;

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

}
