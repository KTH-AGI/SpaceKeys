using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float minX = -10f; // Adjust the minimum x-coordinate as needed
    [SerializeField] float maxX = 10f;  // Adjust the maximum x-coordinate as needed
    [SerializeField] float minZ = -5f; // Adjust the minimum z-coordinate as needed
    [SerializeField] float maxZ = 5f;  // Adjust the maximum z-coordinate as needed
    // Start is called before the first frame update
    public float moveSpeed = 5.0f;
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        // Get input from the arrow keys or WASD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

        // Update player position
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        ClampPosition();
    }

    void ClampPosition()
    {
        // Define boundaries for player movement
        
        // Clamp the player's position within the defined boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minZ, maxZ);
        transform.position = clampedPosition;
    }
}
