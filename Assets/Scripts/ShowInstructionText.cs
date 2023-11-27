using System.Collections;
using UnityEngine;

public class ShowInstructionText : MonoBehaviour
{
    [SerializeField] private float displayTime = 5f;
    [SerializeField] private GameObject instructionText;
    
    private void Start()
    {
        StartCoroutine(DisplayCanvas());
    }

    IEnumerator DisplayCanvas()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        
        instructionText.SetActive(true);
        yield return new WaitForSeconds(displayTime);

        
        // Fade out
        while (canvasGroup.alpha>0)
        {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        instructionText.SetActive(false);
    }
}
