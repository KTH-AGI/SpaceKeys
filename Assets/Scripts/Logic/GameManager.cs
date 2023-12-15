using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public PlayerHandTrackingController playerController;
    
    [FormerlySerializedAs("worldSpaceCanvas")] [SerializeField]
    private Canvas canvas;
    
    // UI text for displaying combo count
    [SerializeField]
    private TextLayers   pointTextLayers; 
    // UI text for displaying score
    [SerializeField]
    private TextLayers  comboTextLayers; 
    // Image for displaying hit quality
    [SerializeField]
    private HitQualityImageLayers imageLayers;
    [SerializeField]
    private PauseScreenLayers pauseScreenLayers;
    
    private int comboCount = 0; // Current combo count
    private float scoreMultiplier = 1.0f; // Score multiplier
    private int score = 0; // Current score
    
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
        MusicObject.OnCollisionNote += OnCollisionNote;
        NoteLight.OnMissNote += OnMissNote;
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
    public void PauseGame()
    {
        isGamePaused = true;
        pauseScreenLayers.ShowPauseScreen();
        Debug.Log("Game is paused. Press Space to resume.");
        Time.timeScale = 0;
        imageLayers.DestroyHitQualityImage();
        AudioManager.instance.Pause();
    }

    // Method to resume the game
    public void ResumeGame()
    {
        isGamePaused = false;
        pauseScreenLayers.HidePauseScreen();
        Debug.Log("Game resumed.");
        Time.timeScale = 1;
        AudioManager.instance.Resume();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CompleteScene");
        
    }
    
    public void LoadHomeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HomeScene");
    }

    private void OnCollisionNote(Vector3 notePos, Vector3 playerPos, float radius)
    {
        notePos.z=playerPos.z;
        float distance = Vector3.Distance(notePos, playerPos);

        Debug.Log(distance+"   "+radius);
        if (distance < 0.2f * radius)
        {
            UpdateScoreAndCombo("Stellar", 1.0f, notePos);
        }
        else if (distance < 0.4f * radius)
        {
            UpdateScoreAndCombo("Cosmic", 0.8f, notePos);
        }
        else if (distance < 0.6f * radius)
        {
            UpdateScoreAndCombo("Galactic", 0.6f, notePos);
        }
        else if (distance < radius)
        {
            UpdateScoreAndCombo("Orbital", 0.2f, notePos, resetCombo: true);
            ResetCombo();
        }
    }
    
    private void OnMissNote(Vector3 notePos)
    {
        UpdateScoreAndCombo("Miss", 0, notePos, resetCombo: true);
        ResetCombo();
    }
    
    private void UpdateScoreAndCombo(string hitQuality, float scoreFactor, Vector3 notePosition, bool resetCombo = false)
    {
        if (resetCombo)
        {
            comboCount = 0;
            scoreMultiplier = 1.0f;
            score += (int)(100 * scoreFactor);
            imageLayers.ShowHitQualityImage(notePosition, hitQuality);
        }
        else
        {
            comboCount++;
            score += (int)((100+comboCount*2) * scoreFactor * scoreMultiplier);
            if (comboCount % 10 == 0)
            {
                scoreMultiplier += 0.1f;
            }
            // Call to show the hit quality image at the music object's position
            imageLayers.ShowHitQualityImage(notePosition, hitQuality);
            Debug.Log("Hit quality: " + hitQuality);
        }
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        pointTextLayers.UpdateScoreIncrementally(score);
        
        comboTextLayers.UpdateCombo(comboCount);
    }
    
    private void ResetCombo()
    {
        comboCount = 0;
        scoreMultiplier = 1.0f;
        UpdateUI();
    }
}
