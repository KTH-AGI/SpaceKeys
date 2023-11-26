using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class LineGrid : MonoBehaviour
{
    [SerializeField] Material lineMaterial;
    [SerializeField] public int gridSizeX = 10;
    [SerializeField] public int gridSizeY = 10;
    [SerializeField] public int lineY = -10;
    [SerializeField] public int lineLengthZ = 150;
    [SerializeField] float spaceBetweenXLines = 0.1f;
    [SerializeField] float spaceBetweenYLines = 0.1f;
    [SerializeField] float moveSpeed = 5.0f; // Speed of grid movement
    [SerializeField] float lineWidth = 0.3f;

    GameObject gridParent;
    GameObject lineXParent;
    GameObject lineYParent;
    List<GameObject> gridYLines = new List<GameObject>();

    void Start()
    {
        gridParent = new GameObject("LineGrid");
        lineXParent = new GameObject("XLines");
        lineYParent = new GameObject("YLines");
        lineXParent.transform.parent = gridParent.transform;
        lineYParent.transform.parent = gridParent.transform;

        GenerateLineGrid();
    }

    // Update is called once per frame
    void Update()
    {
        MoveGrid();
    }



    void GenerateLineGrid()
    {
        for (int x = -gridSizeX + 1; x < gridSizeX; x++)
        {
            GameObject lineObject = new GameObject("XLine");
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            lineObject.transform.parent = lineXParent.transform;

            //lineRenderer.material = new Material(Shader.Find("Standard"));
            lineRenderer.material = lineMaterial;
            //lineRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));

            //lineRenderer.startColor = Color.white;
            //lineRenderer.endColor = Color.white;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            float spacingX = x * spaceBetweenXLines;

            Vector3 startPoint = new Vector3(spacingX, lineY, 0);
            Vector3 endPoint = new Vector3(spacingX, lineY, lineLengthZ);
            DrawLine(startPoint, endPoint, lineRenderer);
        }

        

        for (int y = 0; y < gridSizeY; y++)
        {
            createYLine();
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
        for (int i = 0; i < gridYLines.Count; i++)
        {
            GameObject yLine = gridYLines[i];
            yLine.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (gridYLines[0].transform.position.z < 0)
        {
            Destroy(gridYLines[0]);
            gridYLines.RemoveAt(0);

            createYLine();
        }


    }

    void createYLine()
    {
        float zPosition = gridYLines.Count * spaceBetweenYLines;
        float startX = (-gridSizeX + 1) * spaceBetweenXLines;
        float endX = (gridSizeX - 1) * spaceBetweenXLines;

        GameObject lineObject = new GameObject("YLine");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        lineObject.transform.parent = lineYParent.transform;
        lineRenderer.useWorldSpace = false;

        //lineRenderer.material = new Material(Shader.Find("Standard"));
        //lineRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        lineRenderer.material = lineMaterial;

        //lineRenderer.startColor = Color.white;
        //lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3 startPoint = new Vector3(startX, lineY, 0);
        Vector3 endPoint = new Vector3(endX, lineY, 0);
        
        DrawLine(startPoint, endPoint, lineRenderer);
        lineObject.transform.Translate(0, 0, zPosition);

        gridYLines.Add(lineObject);
    }

}
