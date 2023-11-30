using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Mediapipe.RenderAnnotation.Types;

public class PrismsGenerator : MonoBehaviour
{
    [SerializeField] float spaceBetweenPrisms = 2f;
    [SerializeField] public GameObject prism;

    [SerializeField] int numberOfPrisms = 20;

    GameObject prismParent;
    GameObject prismLeft;
    GameObject prismRight;
    List<GameObject> prismsLeft = new List<GameObject>();
    List<GameObject> prismsRight = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        prismParent = new GameObject("prisms");
        prismLeft = new GameObject("prisms left");
        prismRight = new GameObject("prisms right");
        prismLeft.transform.parent = prismParent.transform;
        prismRight.transform.parent = prismParent.transform;

        for (int y = 0; y < numberOfPrisms; y++)
        {
            createPrisms();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (prismsLeft[0].transform.position.z < 0)
        {
            Destroy(prismsLeft[0]);
            prismsLeft.RemoveAt(0);
            Destroy(prismsRight[0]);
            prismsRight.RemoveAt(0);

            createPrisms();
        }
    }

    void createPrisms()
    {
        float zPosition = prismsLeft.Count * spaceBetweenPrisms;

        Vector3 startPointLeft = new Vector3(-26, 0, zPosition);
        Vector3 startPointRight = new Vector3(26, 0, zPosition);

        GameObject newPrismLeft = Instantiate(prism, startPointLeft, Quaternion.identity);
        GameObject newPrismRight = Instantiate(prism, startPointRight, Quaternion.identity);

        newPrismLeft.transform.parent = prismLeft.transform;
        newPrismRight.transform.parent = prismRight.transform;

        prismsLeft.Add(newPrismLeft);
        prismsRight.Add(newPrismRight);
    }
}
