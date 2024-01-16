using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followSpeed = 5f;
    public float zOffset = -100f;
    public float xOffset = -17.5f;
    private bool isGameOver = false;

    void Update()
    {
        // Check if the game is not over
        if (!isGameOver && player != null)
        {
            // Calculate the target position with an offset in the Z direction
            Vector3 targetPosition = new Vector3(player.position.x + xOffset, transform.position.y, player.position.z + zOffset);

            // Move the empty object (camera follow) towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        }
    }

    // Call this method when the game is over
    public void GameOver()
    {
        isGameOver = true;
    }
}
