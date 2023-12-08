using System.Collections.Generic;
using Mediapipe.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    // Threshold for determining if a fist is made
    [SerializeField] private float ratioThreshold = 1.1f;

    private bool isLeftFistMade = false;
    private bool isRightFistMade = false;
    
    public delegate void FistDetectedHandler(bool isFistMade); 
    public event FistDetectedHandler OnFistDetected;

    void Update()
    {
        // Find hand position first before updating player position
        if (FindHands()) 
        {
            if (!GameManager.Instance.IsGamePaused)
            {
                AlignPlayer();
            }
            FistDetection();
        }

    }

    private void AlignPlayer()
    {
        // Get left player position in worlds space from hand position in screen space.
        var playerLeftPosition = ModifyClippingPlane(GetPalmLocalPosition(_handLandmarkListLeftHand), _playerZValue);
        playerLeftPosition = OffsetHandLandmarks(playerLeftPosition);
        playerLeftPosition = Camera.main.ScreenToWorldPoint(playerLeftPosition);

        _playerLeft.transform.position = playerLeftPosition;

        // Get right player position in worlds space from hand position in screen space.
        var playerRightPosition = ModifyClippingPlane(GetPalmLocalPosition(_handLandmarkListRightHand), _playerZValue);
        playerRightPosition = OffsetHandLandmarks(playerRightPosition);
        playerRightPosition = Camera.main.ScreenToWorldPoint(playerRightPosition);

        _playerRight.transform.position = playerRightPosition;
    }

    private void FistDetection()
    {
        // Calculate the distance ratios for the left and right hand using the landmark list
        CalculateDistanceRatios(_handLandmarkListLeftHand);
        CalculateDistanceRatios(_handLandmarkListRightHand);
        
        // Check if a fist is made with the left and right hand
        isLeftFistMade = CheckIfFist(_handLandmarkListLeftHand);
        isRightFistMade = CheckIfFist(_handLandmarkListRightHand);
        
        // If both hands have made fists, trigger the OnFistDetected event and pass the states of both fists
        if (isRightFistMade && isLeftFistMade)
        {
            OnFistDetected?.Invoke(true);
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
        for (int i = 0; i < 2; i++)
        {
            HandLandmarkListAnnotation hand = _handLandmarkListAnnotation[i];
            if(hand.GetHandedness()=="left")
            {
                _handLandmarkListLeftHand = hand;
            }
            else if(hand.GetHandedness()=="right")
            {
                _handLandmarkListRightHand = hand;
            }
        }

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
        return (GetWristLocalPosition(handLandmarkListAnnotation) +
                GetMiddleFingerMcpLocalPosition(handLandmarkListAnnotation)) / 2;
    }

    // Here specifically, the clipping plane determines how far the object should be rendered from the camera.
    private Vector3 ModifyClippingPlane(Vector3 v, float z)
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
    
    // Calculate the distance from a hand landmark to the wrist
    private float GetDistanceToWrist(Vector3 landmarkPosition, Vector3 wristPosition)
    {
        return Vector3.Distance(landmarkPosition, wristPosition);
    }


    // Calculate the ratio of the distance from the base of the fingers to the palm and the distance from the tips of the fingers to the palm
    public float[] CalculateDistanceRatios(HandLandmarkListAnnotation handLandmarkListAnnotation)
    {
        // Get the wrist position only once
        Vector3 wristPosition = GetHandLandmark(handLandmarkListAnnotation, 0).transform.localPosition;

        // Calculate the distances from the base of the fingers to the wrist
        float distanceToIndexBase =
            GetDistanceToWrist(GetHandLandmark(handLandmarkListAnnotation, 5).transform.localPosition, wristPosition);
        float distanceToMiddleBase =
            GetDistanceToWrist(GetHandLandmark(handLandmarkListAnnotation, 9).transform.localPosition, wristPosition);
        float distanceToRingBase =
            GetDistanceToWrist(GetHandLandmark(handLandmarkListAnnotation, 13).transform.localPosition, wristPosition);
        float distanceToPinkyBase =
            GetDistanceToWrist(GetHandLandmark(handLandmarkListAnnotation, 17).transform.localPosition, wristPosition);

        // Calculate the distances from the tips of the fingers to the wrist
        float distanceToIndexTip =
            GetDistanceToWrist(GetHandLandmark(handLandmarkListAnnotation, 8).transform.localPosition, wristPosition);
        float distanceToMiddleTip =
            GetDistanceToWrist(GetHandLandmark(handLandmarkListAnnotation, 12).transform.localPosition, wristPosition);
        float distanceToRingTip =
            GetDistanceToWrist(GetHandLandmark(handLandmarkListAnnotation, 16).transform.localPosition, wristPosition);
        float distanceToPinkyTip =
            GetDistanceToWrist(GetHandLandmark(handLandmarkListAnnotation, 20).transform.localPosition, wristPosition);

        // Calculate the ratios
        float ratioIndex = distanceToIndexBase / distanceToIndexTip;
        float ratioMiddle = distanceToMiddleBase / distanceToMiddleTip;
        float ratioRing = distanceToRingBase / distanceToRingTip;
        float ratioPinky = distanceToPinkyBase / distanceToPinkyTip;

        float[] ratios = new float[] { ratioIndex, ratioMiddle, ratioRing, ratioPinky };

        return ratios;
    }

    // Calculate the average of a queue of floats
    private float CalculateAverage(Queue<float> queue)
    {
        float sum = 0f;
        foreach (var value in queue)
        {
            sum += value;
        }

        return sum / queue.Count;
    }

    // Method to update ratio queues and calculate the average
    public bool CheckIfFist(HandLandmarkListAnnotation handLandmarkListAnnotation)
    {
        float[] ratios = CalculateDistanceRatios(handLandmarkListAnnotation);

        // Queue to hold the last ten ratio values for each finger
        Queue<float> ratiosIndex = new Queue<float>();
        Queue<float> ratiosMiddle = new Queue<float>();
        Queue<float> ratiosRing = new Queue<float>();
        Queue<float> ratiosPinky = new Queue<float>();

        // Enqueue the latest ratios
        ratiosIndex.Enqueue(ratios[0]);
        ratiosMiddle.Enqueue(ratios[1]);
        ratiosRing.Enqueue(ratios[2]);
        ratiosPinky.Enqueue(ratios[3]);

        // Ensure we only keep the last ten ratios for each finger
        if (ratiosIndex.Count > 10) ratiosIndex.Dequeue();
        if (ratiosMiddle.Count > 10) ratiosMiddle.Dequeue();
        if (ratiosRing.Count > 10) ratiosRing.Dequeue();
        if (ratiosPinky.Count > 10) ratiosPinky.Dequeue();

        // Calculate the averages
        float averageIndex = CalculateAverage(ratiosIndex);
        float averageMiddle = CalculateAverage(ratiosMiddle);
        float averageRing = CalculateAverage(ratiosRing);
        float averagePinky = CalculateAverage(ratiosPinky);

        // Print the averages to the console and GUI
        /*Debug.Log($"Average Ratio Index Base/Tip: {averageIndex}");
        Debug.Log($"Average Ratio Middle Base/Tip: {averageMiddle}");
        Debug.Log($"Average Ratio Ring Base/Tip: {averageRing}");
        Debug.Log($"Average Ratio Pinky Base/Tip: {averagePinky}");*/

        // Check if all the averages are below the threshold
        bool isFist = averageIndex > ratioThreshold && averageMiddle > ratioThreshold &&
                      averageRing > ratioThreshold && averagePinky > ratioThreshold;

        // Return true if fist is detected, false otherwise
        return isFist;
    }

    void OnGUI()
    {
        GUIStyle myLabelStyle = new GUIStyle(GUI.skin.label);
        myLabelStyle.fontSize = 30; 
        // Display the ratios on the GUI
        /*GUI.Label(new Rect(120, 10, 300, 20), $"Ratio Index Base/Tip: {ratioIndex}");
        GUI.Label(new Rect(120, 30, 300, 20), $"Ratio Middle Base/Tip: {ratioMiddle}");
        GUI.Label(new Rect(120, 50, 300, 20), $"Ratio Ring Base/Tip: {ratioRing}");
        GUI.Label(new Rect(120, 70, 300, 20), $"Ratio Pinky Base/Tip: {ratioPinky}");*/
        GUI.Label(new Rect(20, 10, 500, 50), $"Left Hand Is Fist: {isLeftFistMade}", myLabelStyle);
        GUI.Label(new Rect(20, 60, 500, 50), $"Right Hand Is Fist: {isRightFistMade}", myLabelStyle);
    }
}