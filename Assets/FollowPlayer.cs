using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followSpeed = 5f; // Adjust the follow speed as needed
    public float zOffset = 5f; // Adjust the desired distance in the Z direction

    void Update()
    {
        // Calculate the target position with an offset in the Z direction
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, player.position.z + zOffset);

        // Move the empty object (camera follow) towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
    }
}
