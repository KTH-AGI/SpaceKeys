using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f; // 可以根据需要调整速度

    private Rigidbody rb;
    private Vector3 deviceAcceleration;
    private Vector3 filteredAcceleration;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true; // 开启陀螺仪
    }

    void Update()
    {
        deviceAcceleration = Input.acceleration;
        filteredAcceleration = FilterAcceleration(deviceAcceleration);

        MovePlayer(filteredAcceleration);
        DisplayAcceleration();
    }

    // 去除重力影响的加速度
    Vector3 FilterAcceleration(Vector3 acceleration)
    {
        // 这里的算法可以根据需要进行调整，以更准确地去除重力影响
        Vector3 gravity = new Vector3(0, 0, -1) * acceleration.z;
        return acceleration - gravity;
    }

    // 根据加速度移动玩家
    void MovePlayer(Vector3 acceleration)
    {
        Vector3 movement = new Vector3(acceleration.x, acceleration.y, 0);
        rb.velocity = movement * speed;
    }

    // 在控制台和GUI上显示加速度
    void DisplayAcceleration()
    {
        Debug.Log("Acceleration X: " + filteredAcceleration.x + " Y: " + filteredAcceleration.y);
    }

    void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 50;
        GUI.Label(new Rect(10, 10, 400, 60), "X: " + filteredAcceleration.x + " Y: " + filteredAcceleration.y, guiStyle);
    }
}