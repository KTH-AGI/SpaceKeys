using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject gridPrefab;
    [SerializeField] public int gridSizeX = 10;
    [SerializeField] public int gridSizeY = 10;
    [SerializeField] public float cellSize = 1.0f;
    [SerializeField] float spaceBetweenCells = 0.1f;
    [SerializeField] float moveSpeed = 15.0f; // Speed of grid movement

    GameObject gridParent; 
    List<GameObject> gridBlocks = new List<GameObject>();
    void Start()
    {
        gridParent = new GameObject("GridParent");
        GenerateGrid();
    }

    void Update()
    {
        MoveGrid();
    }


    void GenerateGrid()
    {
        for (int x = -gridSizeX; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                GameObject grid = Instantiate(gridPrefab);
                grid.transform.parent = gridParent.transform;

                grid.transform.localPosition = new Vector3(x * (cellSize + spaceBetweenCells), -10f, y * (cellSize + spaceBetweenCells));
                gridBlocks.Add(grid);

                //grid.transform.parent = gridParent.transform;
            }
        }
    }

    void MoveGrid()
    {
        // Move the entire grid along the z-axis
        gridParent.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //// Check if any grid blocks have moved out of view, and remove them
        //for (int i = 0; i < gridBlocks.Count; i++)
        //{
        //    if (gridBlocks[i].transform.position.z > gridSizeY * (cellSize + spaceBetweenCells))
        //    {
        //        Destroy(gridBlocks[i]);
        //        gridBlocks.RemoveAt(i);
        //        i--;
        //    }
        //}

        //// Add new grid blocks at the end
        //for (int x = 0; x < gridSizeX; x++)
        //{
        //    GameObject grid = Instantiate(gridPrefab, new Vector3(x * (cellSize + spaceBetweenCells), -10f, -spaceBetweenCells), Quaternion.identity);
        //    gridBlocks.Add(grid);
        //}
    }
}

