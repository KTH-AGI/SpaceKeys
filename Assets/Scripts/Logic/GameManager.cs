using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public PlayerController playerController;
    
    [FormerlySerializedAs("worldSpaceCanvas")] [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TextMeshProUGUI  comboText; // UI text for displaying combo count
    [SerializeField]
    private TextMeshProUGUI  scoreText; // UI text for displaying score
    [SerializeField]
    private TextMeshProUGUI hitQualityTextPrefab; // Prefab for the hit quality text
    
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
    void FixedUpdate()
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
            ShowHitQualityText(notePosition, hitQuality);
        }
        else
        {
            comboCount++;
            score += (int)((100+comboCount*2) * scoreFactor * scoreMultiplier);
            if (comboCount % 10 == 0)
            {
                scoreMultiplier += 0.1f;
            }
            // Call to show the hit quality text at the music object's position
            ShowHitQualityText(notePosition, hitQuality);
            Debug.Log("Hit quality: " + hitQuality);
        }
        

        UpdateUI();
    }
    
    private void ShowHitQualityText(Vector3 noteWorldPosition, string hitQuality)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(noteWorldPosition);
        TextMeshProUGUI textInstance = Instantiate(hitQualityTextPrefab, canvas.transform);

        // Set the text to display hit quality
        textInstance.text = hitQuality;

        // Convert screen coordinates to Canvas local coordinates
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPosition, null, out Vector2 localPoint);
        // Set the text's local position within the Canvas
        textInstance.rectTransform.anchoredPosition = localPoint;
        
        // Start the animation coroutine
        StartCoroutine(AnimateText(textInstance.rectTransform, textInstance));
    }
    
    private IEnumerator AnimateText(RectTransform rectTransform, TextMeshProUGUI textInstance)
    {
        // Fade in and move up
        float duration = 0.5f; // Duration of the rise and fade in
        float pauseDuration = 0.5f; // Duration of the pause at peak
        float textHeight = textInstance.preferredHeight; // Approximate height of the text
        Vector2 startPos = rectTransform.anchoredPosition+ new Vector2(0, -textHeight/2);
        Vector2 endPos = startPos + new Vector2(0, textHeight/2); // End position after rise

        // Initialize alpha to 0 (transparent)
        textInstance.color = new Color(textInstance.color.r, textInstance.color.g, textInstance.color.b, 0);

        // Rise and fade in animation
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            // Linear interpolation from start to end
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, normalizedTime);
            // Fade in
            textInstance.color = new Color(textInstance.color.r, textInstance.color.g, textInstance.color.b, normalizedTime);
            yield return null;
        }

        // Ensure the final values are set after the animation
        rectTransform.anchoredPosition = endPos;
        textInstance.color = new Color(textInstance.color.r, textInstance.color.g, textInstance.color.b, 1);

        // Wait for a while at the peak
        yield return new WaitForSeconds(pauseDuration);

        // Fade out animation
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            // Fade out
            textInstance.color = new Color(textInstance.color.r, textInstance.color.g, textInstance.color.b, 1 - normalizedTime);
            yield return null;
        }

        // Destroy the text instance after the animation
        Destroy(textInstance.gameObject);
    }

    private void UpdateUI()
    {
        comboText.text = "Combo: " + comboCount.ToString();
        scoreText.text = "Score: " + score.ToString();
    }
    
    private void ResetCombo()
    {
        comboCount = 0;
        scoreMultiplier = 1.0f;
        UpdateUI();
    }
}
