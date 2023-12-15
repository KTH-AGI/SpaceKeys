using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextLayers   pointTextLayers; 
    // UI text for displaying score
    [SerializeField]
    private TextLayers  maxComboTextLayers; 
    // Start is called before the first frame update
    void Start()
    {
        pointTextLayers.UpdateScoreIncrementally(PlayerPrefs.GetInt("Score"));
        maxComboTextLayers.UpdateScoreIncrementally(PlayerPrefs.GetInt("MaxCombo"));
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void LoadGameScene()
    {
        // Activate the CompleteScene
        SceneManager.LoadScene(1);
    }
    
    public void LoadHomeScene()
    {
        // Activate the CompleteScene
        SceneManager.LoadScene(0);
    }
}
