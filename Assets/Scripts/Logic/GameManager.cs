using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public PlayerController playerController;
    
    [FormerlySerializedAs("worldSpaceCanvas")] [SerializeField]
    private Canvas canvas;
    
    // UI text for displaying combo count
    [SerializeField]
    private TextLayers   pointTextLayers; 
    // UI text for displaying score
    [SerializeField]
    private TextLayers  comboTextLayers; 
    
    // Sprites for the hit quality images
    [SerializeField] private Sprite[] hitQualitySprites;
    // Prefab for the hit quality image
    [SerializeField] private Image hitQualityImagePrefab; 

    
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
            ShowHitQualityImage(notePosition, hitQuality);
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
            ShowHitQualityImage(notePosition, hitQuality);
            Debug.Log("Hit quality: " + hitQuality);
        }
        

        UpdateUI();
    }
    
    private void ShowHitQualityImage(Vector3 noteWorldPosition, string hitQuality)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(noteWorldPosition);
        Image imageInstance = Instantiate(hitQualityImagePrefab, canvas.transform);

        // Set the image to display hit quality
        imageInstance.sprite= GetHitQualitySprite(hitQuality);;

        // Convert screen coordinates to Canvas local coordinates
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPosition, null, out Vector2 localPoint);
        // Set the image's local position within the Canvas
        imageInstance.rectTransform.anchoredPosition = localPoint;
        
        // Start the animation coroutine
        StartCoroutine(AnimateImage(imageInstance.rectTransform, imageInstance));
    }
    
    private IEnumerator AnimateImage(RectTransform rectTransform, Image imageInstance)
    {
        // Fade in and move up
        float duration = 0.5f; // Duration of the rise and fade in
        float pauseDuration = 0.5f; // Duration of the pause at peak
        float imageHeight = imageInstance.preferredHeight; // Approximate height of the image
        Vector2 startPos = rectTransform.anchoredPosition+ new Vector2(0, 0);
        Vector2 endPos = startPos + new Vector2(0, 50); // End position after rise

        // Initialize alpha to 0 (transparent)
        imageInstance.color = new Color(imageInstance.color.r, imageInstance.color.g, imageInstance.color.b, 0);

        // Rise and fade in animation
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            // Linear interpolation from start to end
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, normalizedTime);
            // Fade in
            imageInstance.color = new Color(imageInstance.color.r, imageInstance.color.g, imageInstance.color.b, normalizedTime);
            yield return null;
        }

        // Ensure the final values are set after the animation
        rectTransform.anchoredPosition = endPos;
        imageInstance.color = new Color(imageInstance.color.r, imageInstance.color.g, imageInstance.color.b, 1);

        // Wait for a while at the peak
        yield return new WaitForSeconds(pauseDuration);

        // Fade out animation
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            // Fade out
            imageInstance.color = new Color(imageInstance.color.r, imageInstance.color.g, imageInstance.color.b, 1 - normalizedTime);
            yield return null;
        }

        // Destroy the image instance after the animation
        Destroy(imageInstance.gameObject);
    }
    
    // Method to get the hit quality sprite based on the hit quality name
    private Sprite GetHitQualitySprite(string hitQuality)
    {
        // 根据命中质量名称返回对应的Sprite
        switch (hitQuality)
        {
            case "Stellar":
                return hitQualitySprites[0];
            case "Cosmic":
                return hitQualitySprites[1];
            case "Galactic":
                return hitQualitySprites[2];
            case "Orbital":
                return hitQualitySprites[3];
            case "Miss":
                return hitQualitySprites[4];
            default:
                return null;
        }
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
