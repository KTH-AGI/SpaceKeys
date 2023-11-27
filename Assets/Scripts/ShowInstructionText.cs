using System.Collections;
using UnityEngine;

public class ShowInstructionText : MonoBehaviour
{
    [SerializeField] private float displayTime = 5f;
    [SerializeField] private GameObject InstructionText;
    
    private void Start()
    {
        // InstructionText.SetActive(false);
        StartCoroutine(DisplayCanvas());
    }

    IEnumerator DisplayCanvas()
    {
        InstructionText.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        
        InstructionText.SetActive(false);
    }
}
