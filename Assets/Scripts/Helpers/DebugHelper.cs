using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class DebugHelper : Singleton<DebugHelper>
{

    [SerializeField] private TextMeshProUGUI debugText;
    
    // Getter for debugText object
    public TextMeshProUGUI DebugText => debugText;

    private void Start()
    {
        Assert.IsNotNull(debugText);
    }
}
