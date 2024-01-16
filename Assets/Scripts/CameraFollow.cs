using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = new(0, 2, -5); // Adjust the offset as needed

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Set the camera's position to follow the player with the specified offset
            transform.position = playerTransform.position + offset;
        }
    }
}
