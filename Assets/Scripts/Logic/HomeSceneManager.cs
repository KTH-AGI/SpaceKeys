using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    private AsyncOperation sceneAsyncOperation;
    // Start is called before the first frame update
    void Start()
    {
        // Load the HWDScene asynchronously
        sceneAsyncOperation = SceneManager.LoadSceneAsync("HWDScene");
        sceneAsyncOperation.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayButtonClicked()
    {
        // Activate the HWDScene when it is ready
        if (sceneAsyncOperation != null)
        {
            sceneAsyncOperation.allowSceneActivation = true;
        }
    }
}
