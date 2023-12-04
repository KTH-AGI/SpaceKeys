using Mediapipe.Unity;
using UnityEngine;

public class AlignPlayerWithHand : MonoBehaviour
{

    [SerializeField] private MultiHandLandmarkListAnnotation _handLandmarkListAnnotation;
    [SerializeField] private GameObject _playerLeft;
    [SerializeField] private GameObject _playerRight;
    [SerializeField] private float _playerZValue = 100;


    
    // Lists containing the Hand Landmarks
    private HandLandmarkListAnnotation _handLandmarkListLeftHand;
    private HandLandmarkListAnnotation _handLandmarkListRightHand;

    private float _screenWidth = 1920;
    private float _screenHeight = 1020;
    
    // Boolean used to check if hands were already initialized before.
    private bool initialized = false;
    
    void Update()
    {
        // Find hand position first before updating player position
        if (FindHands())
        {
            // Get left player position in worlds space from hand position in screen space.
            var playerLeftPosition = ModifyClippingPlane(GetPalmLocalPosition(_handLandmarkListLeftHand),_playerZValue);
            playerLeftPosition = OffsetHandLandmarks(playerLeftPosition);
            playerLeftPosition = Camera.main.ScreenToWorldPoint(playerLeftPosition);

            _playerLeft.transform.position = playerLeftPosition;

            // Get right player position in worlds space from hand position in screen space.
            var playerRightPosition = ModifyClippingPlane(GetPalmLocalPosition(_handLandmarkListRightHand), _playerZValue);
            playerRightPosition = OffsetHandLandmarks(playerRightPosition);
            playerRightPosition = Camera.main.ScreenToWorldPoint(playerRightPosition);

            _playerRight.transform.position = playerRightPosition;
        }
    }

    private bool FindHands()
    {
        // No Hands found yet, can't set Hand Landmark List yet!
        if (_handLandmarkListAnnotation.count <= 1)
        {
            initialized = false;
            return false;
        }
        
        // Hands already found and initialized!
        if (initialized) return true;
        
        // Found hands! Initialize Lists and Screen Size
        _handLandmarkListLeftHand = _handLandmarkListAnnotation[0];
        _handLandmarkListRightHand = _handLandmarkListAnnotation[1];
        InitializeScreenDimensions();
        initialized = true;
        return true;
    }

    private void InitializeScreenDimensions()
    {
        _screenWidth = _handLandmarkListAnnotation.GetScreenRect().width;
        _screenHeight = _handLandmarkListAnnotation.GetScreenRect().height;
    }
    
    // Indices of each hand landmark defined at https://developers.google.com/mediapipe/solutions/vision/hand_landmarker
    private PointAnnotation GetHandLandmark(HandLandmarkListAnnotation handLandmarkListAnnotation, int index)
    {
        return handLandmarkListAnnotation[index];
    }
    
    private Vector3 GetWristLocalPosition(HandLandmarkListAnnotation handLandmarkListAnnotation)
    {
        // Wrist Landmark has Index 0 in the list. 
        return GetHandLandmark(handLandmarkListAnnotation, 0).transform.localPosition;
    }

    private Vector3 GetMiddleFingerMcpLocalPosition(HandLandmarkListAnnotation handLandmarkListAnnotation)
    {
        return GetHandLandmark(handLandmarkListAnnotation, 9).transform.localPosition;
    }

    // Returns the local position of the center point of the palm. Approximated by the mid point between the wrist point
    // and middle finger MCP point.
    private Vector3 GetPalmLocalPosition(HandLandmarkListAnnotation handLandmarkListAnnotation)
    {
        return (GetWristLocalPosition(handLandmarkListAnnotation) + GetMiddleFingerMcpLocalPosition(handLandmarkListAnnotation)) / 2;
    }

    // Here specifically, the clipping plane determines how far the object should be rendered from the camera.
    private Vector3 ModifyClippingPlane(Vector3 v ,float z)
    {
        v.z = z;
        return v;
    }

    // Assumes the current Origin(0,0) is in the middle of the Screen and returns a Vector3 position
    // where the new Origin is at the bottom right.
    private Vector3 OffsetHandLandmarks(Vector3 v)
    {
        return new Vector3(v.x + _screenWidth / 2, v.y + _screenHeight / 2, v.z);
    }
    
    
}
