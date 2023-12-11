using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public PlayerController playerController;
    private bool isGamePaused = false;
    
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        // Subscribe to the OnFistDetected event of the PlayerController
        if (playerController != null)
        {
            playerController.OnFistDetected += HandleFistDetected;
        }
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from the OnFistDetected event to prevent memory leaks
        if (playerController != null)
        {
            playerController.OnFistDetected -= HandleFistDetected;
        }
    }
    
    // Be called when the OnFistDetected event is triggered
    private void HandleFistDetected(bool isFistMade)
    {
        // Implement logic to respond to both fists being detected
        if (isFistMade)
        {
            PauseGame();
            Debug.Log("Both hands have made fists.");
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamePaused && Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
        }
    }
    
    // Property to get the value of isGamePaused
    public bool IsGamePaused
    {
        get { return isGamePaused; }
    }
    
    // Method to pause the game
    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0; 
        Debug.Log("Game is paused. Press Space to resume.");
    }

    // Method to resume the game
    private void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1; 
        Debug.Log("Game resumed.");
    }
}
