using System.Linq;
using UnityEngine;

public class UpdateShieldColor : MonoBehaviour
{
 
    // Color and Blending variables
    private Material _shieldMaterial;
    private Color _noteColor;
    private Color _currentColor;
    private Gradient _blendGradient;
    private GradientColorKey[] _colorGradients; 
    private GradientAlphaKey[] _alphaGradients;
    
    // Variables used to determine how quick the blending of colors occur
    [SerializeField] private int blendFrequency = 120;
    private int _currentBlendStep;
    
    // Cached Property Index
    private static readonly int _EmissionColor = Shader.PropertyToID("_EmissionColor");
    private static readonly int _FresnelColor = Shader.PropertyToID("_FresnelColor");

    void Start()
    {
        _shieldMaterial = gameObject.GetComponent<Renderer>().material;
        SetUpGradientVariables();
    }

    // Set collision layer to only include "Notes"
    private void OnTriggerEnter(Collider other)
    {
        
        // Get emission color from note trigger
        Material noteMaterial;
        if (!other.gameObject.GetComponent<Renderer>())
        {
            return;
        }

        // Check if other object has an Emission color
        noteMaterial = other.gameObject.GetComponent<Renderer>().material;
        if (noteMaterial.HasProperty(_EmissionColor))
        {
            _noteColor = noteMaterial.GetColor(_EmissionColor);
            // Update last color for gradient
            UpdateGradient(_noteColor);
        }
    }

    void Update()
    {
        // Update fresnel color of shield
        _shieldMaterial.SetColor(_FresnelColor, GetNextColor());
    }


    // Gets next color according to the gradient and current step
    private Color GetNextColor()
    {
        var step = Mathf.Clamp((float) _currentBlendStep / blendFrequency, 0.0f, 1.0f);
        
        _currentBlendStep++;
        _currentColor = _blendGradient.Evaluate(step);
        return _currentColor;
    }

    
    // Updates last color of gradient
    private void UpdateGradient(Color endColor)
    {
        _colorGradients[0].color = _currentColor;
        _colorGradients[1].color = endColor;
        _currentBlendStep = 0;
        _blendGradient.SetKeys(_colorGradients, _alphaGradients);
    }

    
    // Setting Alpha Keys for the Gradient.
    private void SetUpGradientVariables()
    {
        // Setup current and next note color
        _currentColor = _shieldMaterial.GetColor(_FresnelColor); 
        _noteColor = _currentColor;
        
        // Initialize Alpha Gradients
        _alphaGradients = new GradientAlphaKey[2];
        // Setting alpha value at beginning and end to 1.
        _alphaGradients[0] = new GradientAlphaKey(1.0f, 0.0f);
        _alphaGradients[1] = new GradientAlphaKey(1.0f, 1.0f);

        // Initialize Color Gradients
        _colorGradients = new GradientColorKey[2];
        _colorGradients[0] = new GradientColorKey(_currentColor, 0.0f);
        _colorGradients[1] = new GradientColorKey(_currentColor, 1.0f);
        
        // Setup Gradient
        _blendGradient = new Gradient();
        _blendGradient.SetKeys(_colorGradients, _alphaGradients);
    }
    
}
