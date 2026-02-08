using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform camTarget; // the object the VCAM follows
    [SerializeField] private float panSpeed = 10f; // units per second
    [SerializeField] private Vector2 panRangeX = new Vector2(-500f, 500f); // min/max X
    [SerializeField] private Vector2 panRangeZ = new Vector2(-500f, 500f); // min/max Z
    [SerializeField] private float edgeThreshold = 0.05f; // how far from screen edge to start moving

    private void Update()
    {
        if (camTarget == null) return;

        Vector3 move = Vector3.zero;

        // Screen normalized [0..1]
        float mouseX = Input.mousePosition.x / Screen.width;
        float mouseY = Input.mousePosition.y / Screen.height;

        // Left/right movement
        if (mouseX < edgeThreshold)
            move.x = -1f;
        else if (mouseX > 1f - edgeThreshold)
            move.x = 1f;

        // Forward/back movement (Z)
        if (mouseY < edgeThreshold)
            move.z = -1f;
        else if (mouseY > 1f - edgeThreshold)
            move.z = 1f;

        // Apply movement
        camTarget.position += move * panSpeed * Time.deltaTime;

        // Clamp to bounds
        camTarget.position = new Vector3(
            Mathf.Clamp(camTarget.position.x, panRangeX.x, panRangeX.y),
            camTarget.position.y,
            Mathf.Clamp(camTarget.position.z, panRangeZ.x, panRangeZ.y)
        );
    }
}
