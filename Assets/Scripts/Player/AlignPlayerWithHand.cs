using Mediapipe.Unity;
using UnityEngine;
using System.Collections.Generic;

public class AlignPlayerWithHand : MonoBehaviour
{

    [SerializeField] private MultiHandLandmarkListAnnotation _handLandmarkListAnnotation;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _playerZValue = 100;
    
    // List containing the Hand Landmarks
    private HandLandmarkListAnnotation _handLandmarkList;

    private float _screenWidth = 1920;
    private float _screenHeight = 1020;
    
    // Boolean used to check if hands were already initialized before.
    private bool initialized = false;
    
    private float ratioIndex, ratioMiddle, ratioRing, ratioPinky;
    // Queue to hold the last ten ratio values for each finger
    private Queue<float> ratiosIndex = new Queue<float>();
    private Queue<float> ratiosMiddle = new Queue<float>();
    private Queue<float> ratiosRing = new Queue<float>();
    private Queue<float> ratiosPinky = new Queue<float>();

    // Threshold for determining if a fist is made
    [SerializeField] private float ratioThreshold = 1.1f;
    
    private bool isFistMade = false;
    
    void Update()
    {
        // Find hand position first before updating player position
        if (FindHands())
        {
            // Get player position in worlds space from hand position in screen space.
            var playerPosition = ModifyClippingPlane(GetPalmLocalPosition(),_playerZValue);
            playerPosition = OffsetHandLandmarks(playerPosition);
            playerPosition = Camera.main.ScreenToWorldPoint(playerPosition);

            _player.transform.position = playerPosition;
            
            CalculateDistanceRatios();
            
            isFistMade = CheckIfFist();
        }
    }

    private bool FindHands()
    {
        // No Hands found yet, can't set Hand Landmark List yet!
        if (_handLandmarkListAnnotation.count <= 0)
        {
            initialized = false;
            return false;
        }
        
        // Hands already found and initialized!
        if (initialized) return true;
        
        // Found hands! Initialize List and Screen Size
        _handLandmarkList = _handLandmarkListAnnotation[0];
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
    private PointAnnotation GetHandLandmark(int index)
    {
        return _handLandmarkList[index];
    }
    
    private Vector3 GetWristLocalPosition()
    {
        // Wrist Landmark has Index 0 in the list. 
        return GetHandLandmark(0).transform.localPosition;
    }

    private Vector3 GetMiddleFingerMcpLocalPosition()
    {
        return GetHandLandmark(9).transform.localPosition;
    }

    // Returns the local position of the center point of the palm. Approximated by the mid point between the wrist point
    // and middle finger MCP point.
    private Vector3 GetPalmLocalPosition()
    {
        return (GetWristLocalPosition() + GetMiddleFingerMcpLocalPosition()) / 2;
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
    
    // Method to calculate the angle between three points
    // Method to get the distance from a hand landmark to the wrist
    private float GetDistanceToWrist(Vector3 landmarkPosition, Vector3 wristPosition)
    {
        return Vector3.Distance(landmarkPosition, wristPosition);
    }
    
    
    // Method to return the angles of the index finger joints, including MCP
   public void CalculateDistanceRatios()
    {
        // Get the wrist position only once
        Vector3 wristPosition = GetHandLandmark(0).transform.localPosition;

        // Calculate the distances from the base of the fingers to the wrist
        float distanceToIndexBase = GetDistanceToWrist(GetHandLandmark(5).transform.localPosition, wristPosition);
        float distanceToMiddleBase = GetDistanceToWrist(GetHandLandmark(9).transform.localPosition, wristPosition);
        float distanceToRingBase = GetDistanceToWrist(GetHandLandmark(13).transform.localPosition, wristPosition);
        float distanceToPinkyBase = GetDistanceToWrist(GetHandLandmark(17).transform.localPosition, wristPosition);

        // Calculate the distances from the tips of the fingers to the wrist
        float distanceToIndexTip = GetDistanceToWrist(GetHandLandmark(8).transform.localPosition, wristPosition);
        float distanceToMiddleTip = GetDistanceToWrist(GetHandLandmark(12).transform.localPosition, wristPosition);
        float distanceToRingTip = GetDistanceToWrist(GetHandLandmark(16).transform.localPosition, wristPosition);
        float distanceToPinkyTip = GetDistanceToWrist(GetHandLandmark(20).transform.localPosition, wristPosition);

        // Calculate the ratios
        ratioIndex = distanceToIndexBase / distanceToIndexTip;
        ratioMiddle = distanceToMiddleBase / distanceToMiddleTip;
        ratioRing = distanceToRingBase / distanceToRingTip;
        ratioPinky = distanceToPinkyBase / distanceToPinkyTip;

        // Print the ratios to the console
        Debug.Log($"Ratio Index Base/Tip: {ratioIndex}");
        Debug.Log($"Ratio Middle Base/Tip: {ratioMiddle}");
        Debug.Log($"Ratio Ring Base/Tip: {ratioRing}");
        Debug.Log($"Ratio Pinky Base/Tip: {ratioPinky}");
    }

   // Method to calculate the average of a queue of floats
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
   public bool CheckIfFist()
   {
       // Update ratios by calling CalculateDistanceRatios (this method should update the class-level ratios)
       CalculateDistanceRatios();

       // Enqueue the latest ratios
       ratiosIndex.Enqueue(ratioIndex);
       ratiosMiddle.Enqueue(ratioMiddle);
       ratiosRing.Enqueue(ratioRing);
       ratiosPinky.Enqueue(ratioPinky);

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
       Debug.Log($"Average Ratio Index Base/Tip: {averageIndex}");
       Debug.Log($"Average Ratio Middle Base/Tip: {averageMiddle}");
       Debug.Log($"Average Ratio Ring Base/Tip: {averageRing}");
       Debug.Log($"Average Ratio Pinky Base/Tip: {averagePinky}");

       // Check if all the averages are below the threshold
       bool isFist = averageIndex > ratioThreshold && averageMiddle > ratioThreshold &&
                     averageRing > ratioThreshold && averagePinky > ratioThreshold;

       // Return true if fist is detected, false otherwise
       return isFist;
   }
   
    void OnGUI()
    {
        // Display the ratios on the GUI
        /*GUI.Label(new Rect(120, 10, 300, 20), $"Ratio Index Base/Tip: {ratioIndex}");
        GUI.Label(new Rect(120, 30, 300, 20), $"Ratio Middle Base/Tip: {ratioMiddle}");
        GUI.Label(new Rect(120, 50, 300, 20), $"Ratio Ring Base/Tip: {ratioRing}");
        GUI.Label(new Rect(120, 70, 300, 20), $"Ratio Pinky Base/Tip: {ratioPinky}");*/
        GUI.Label(new Rect(120, 10, 300, 20), $"Average Ratio Index Base/Tip: {CalculateAverage(ratiosIndex)}");
        GUI.Label(new Rect(120, 30, 300, 20), $"Average Ratio Middle Base/Tip: {CalculateAverage(ratiosMiddle)}");
        GUI.Label(new Rect(120, 50, 300, 20), $"Average Ratio Ring Base/Tip: {CalculateAverage(ratiosRing)}");
        GUI.Label(new Rect(120, 70, 300, 20), $"Average Ratio Pinky Base/Tip: {CalculateAverage(ratiosPinky)}");
        GUI.Label(new Rect(120, 90, 300, 20), $"Is Fist: {isFistMade}");
    }
}
