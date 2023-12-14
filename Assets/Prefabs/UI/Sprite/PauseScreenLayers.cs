using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreenLayers : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Image pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
        Color color=pauseButton.color;
        color.a=0.2f;
        pauseButton.color=color;
    }

    // Method to resume the game
    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
        Color color=pauseButton.color;
        color.a=1f;
        pauseButton.color=color;
    }
}
