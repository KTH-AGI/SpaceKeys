using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    // Start is called before the first frame update    
    void Start()
    {

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
}
