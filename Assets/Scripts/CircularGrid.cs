using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;


public class CircularGrid : MonoBehaviour
{
    [SerializeField] Material lineMaterial;
    [SerializeField] public int gridSizeX = 10;
    [SerializeField] public int gridSizeZ = 10;
    
    [SerializeField] public int lineY = -10;
    [SerializeField] public int lineLengthZ = 150;
    [SerializeField] float spaceBetweenXLines = 0.1f;
    [SerializeField] float spaceBetweenZLines = 0.1f;
    [SerializeField] float moveSpeed = 5.0f; // Speed of grid movement
    [SerializeField] float lineWidth = 0.3f;

    public static int gridSize = 10;
    public static float radius = 20;

    GameObject gridParent;
    GameObject lineXParent;
    GameObject lineZParent;
    List<GameObject> gridZLines = new List<GameObject>();

    void Start()
    {
        gridParent = new GameObject("CircularGrid");
        lineXParent = new GameObject("XLines");
        lineZParent = new GameObject("ZLines");
        lineXParent.transform.parent = gridParent.transform;
        lineZParent.transform.parent = gridParent.transform;

        GenerateCircularGrid();
    }

    // Update is called once per frame
    void Update()
    {
        MoveGrid();
    }



    void GenerateCircularGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            float angle = i * (360f / gridSize);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            GameObject lineObject = new GameObject("XLine");
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            lineObject.transform.parent = lineXParent.transform;

            lineRenderer.material = lineMaterial;

            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            Vector3 startPoint = new Vector3(x, y, 0);
            Vector3 endPoint = new Vector3(x, y, lineLengthZ);


            DrawLine(startPoint, endPoint, lineRenderer);
        }



        for (int y = 0; y < gridSizeZ; y++)
        {
            createZCircle();
        }
    }

    private void DrawLine(Vector3 startPoint, Vector3 endPoint, LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }


    void MoveGrid()
    {
        for (int i = 0; i < gridZLines.Count; i++)
        {
            GameObject yLine = gridZLines[i];
            yLine.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (gridZLines[0].transform.position.z < 0)
        {
            Destroy(gridZLines[0]);
            gridZLines.RemoveAt(0);

            createZCircle();
        }


    }


    void createZCircle()
    {
        GameObject lineObject = new GameObject("ZLine");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineObject.transform.parent = lineZParent.transform;
        lineRenderer.useWorldSpace = false;

        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = gridSize + 1;
        float z = gridZLines.Count * spaceBetweenZLines;

        for (int i = 0; i < gridSize; i++)
        {
            float angle = i * (360f / gridSize);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            

            Vector3 point = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, point);
            if (i == 0) { lineRenderer.SetPosition(gridSize, point); }
        }

        lineObject.transform.Translate(0, 0, z);
        gridZLines.Add(lineObject);
    }


}
