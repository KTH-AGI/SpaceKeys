using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HitQualityImageLayers : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image hitQualityImagePrefab;
    [SerializeField] private Sprite[] hitQualitySprites;
    [SerializeField] private Vector2 leftScreenPosition = new Vector2(-700, 0); 
    [SerializeField] private Vector2 rightScreenPosition = new Vector2(700, 0);
    private Coroutine animateCoroutine;
    private Image imageInstance;
    
    public void ShowHitQualityImage(Vector3 noteWorldPosition, string hitQuality)
    {
        
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(noteWorldPosition);
        imageInstance = Instantiate(hitQualityImagePrefab, canvas.transform);


        // Set the image to display hit quality
        imageInstance.sprite = GetHitQualitySprite(hitQuality);

        // Decide the fixed screen position based on the noteWorldPosition.x
        Vector2 fixedScreenPosition = noteWorldPosition.x <= 0 ? leftScreenPosition : rightScreenPosition;
    
        // Set the image's local position to the fixed position
        imageInstance.rectTransform.anchoredPosition = fixedScreenPosition;

        // Start the animation coroutine
        animateCoroutine=StartCoroutine(AnimateImage(imageInstance.rectTransform, imageInstance));
    }
    
    public void DestroyHitQualityImage()
    {
        if (animateCoroutine != null)
        {
            // Stop the coroutine
            StopCoroutine(animateCoroutine);
            animateCoroutine = null;

            // Additional action after stopping the coroutine
            if (imageInstance != null)
            {
                Destroy(imageInstance.gameObject);
            }
        }
        
    }

    private IEnumerator AnimateImage(RectTransform rectTransform, Image imageInstance)
    {
        // Fade in and move up
        float duration = 0.5f; // Duration of the rise and fade in
        float pauseDuration = 0.5f; // Duration of the pause at peak
        Vector2 startPos = rectTransform.anchoredPosition+ new Vector2(0, -25);
        Vector2 endPos = startPos + new Vector2(0, 25); // End position after rise

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
}
