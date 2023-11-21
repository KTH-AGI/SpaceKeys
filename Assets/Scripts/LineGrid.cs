using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGrid : MonoBehaviour
{
    [SerializeField] public int gridSizeX = 10;
    [SerializeField] public int gridSizeY = 10;
    [SerializeField] public int lineY = -10;
    [SerializeField] public int lineLengthZ = 150;
    [SerializeField] float spaceBetweenXLines = 0.1f;
    [SerializeField] float spaceBetweenYLines = 0.1f;
    [SerializeField] float moveSpeed = 5.0f; // Speed of grid movement

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

            lineRenderer.material = new Material(Shader.Find("Standard"));
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;

            float spacingX = x * spaceBetweenXLines;

            Vector3 startPoint = new Vector3(spacingX, lineY, 0);
            Vector3 endPoint = new Vector3(spacingX, lineY, lineLengthZ);
            DrawLine(startPoint, endPoint, lineRenderer);
        }

        float startX = (-gridSizeX + 1) * spaceBetweenXLines;
        float endX = (gridSizeX - 1) * spaceBetweenXLines;

        for (int y = 0; y < gridSizeY; y++)
        {
            GameObject lineObject = new GameObject("YLine");
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            lineObject.transform.parent = lineYParent.transform;
            lineRenderer.useWorldSpace = false;

            lineRenderer.material = new Material(Shader.Find("Standard"));
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;


            float spacingY = y * spaceBetweenYLines;

            Vector3 startPoint = new Vector3(startX, lineY, spacingY);
            Vector3 endPoint = new Vector3(endX, lineY, spacingY);
            DrawLine(startPoint, endPoint, lineRenderer);

            gridYLines.Add(lineObject);
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
        lineYParent.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        //for (int i = 0; i < gridYLines.Count; i++)
        //{
        //    if (gridYLines[i].transform.position.z < 0)
        //    {
        //        Destroy(gridYLines[i]);
        //        gridYLines.RemoveAt(i);
        //        i--;
        //    }
        //}

        // Add new grid blocks at the end
        //for (int x = 0; x < gridSizeX; x++)
        //{
        //    GameObject grid = Instantiate(gridPrefab, new Vector3(x * (cellSize + spaceBetweenCells), -10f, -spaceBetweenCells), Quaternion.identity);
        //    gridBlocks.Add(grid);
        //}

    }

}
