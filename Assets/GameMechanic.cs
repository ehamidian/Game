using System.Collections;
using UnityEngine;

public class GameMechanic : MonoBehaviour
{
    private Rigidbody rb;
    private bool isJumping = false;

    private AudioLoudnessDetection loudnessDetector;
    //public PitchDetection pitchDetector;
    private SpeechDetection speechDetector;
    public float movementSpeed = 100f;
    public float jumpForce = 5f;
    public float forwardSpeed = 5f;
    public float lateralSpeed = 3f;
    //public PlayerScore playerScore; // Reference to the PlayerScore script

    void Start()
    {
        loudnessDetector = GetComponent<AudioLoudnessDetection>();
        speechDetector = GetComponent<SpeechDetection>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Time.frameCount > 60)
        {
            int direction = loudnessDetector.GetDirection();
            //int jumpAction = pitchDetector.Jump();

            // Move the player based on voice direction
            MovePlayer(direction);

            // Handle jump action
            //speechDetector.Jump();


            //HandleJump(jumpAction);
        }
    }

    void MovePlayer(int direction)
    {
        // Move the player left or right based on voice direction
        if (direction == -1)
        {
            rb.velocity = new Vector3(-lateralSpeed, rb.velocity.y);
        }
        else if (direction == 1)
        {
            rb.velocity = new Vector3(lateralSpeed, rb.velocity.y);
        }
        else
        {
            // No lateral movement if direction is 0
            rb.velocity = new Vector3(0, rb.velocity.y);
        }
    }

    void HandleJump(int jumpAction)
    {
        if (jumpAction == 1 && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;

            //Reset the jump state after a delay
            StartCoroutine(ResetJumpStateAfterDelay(0.1f));
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Candle"))
    //    {
    //        Debug.Log("Collision with Candle!");
    //        playerScore.CollectCandle();
    //        Destroy(collision.gameObject);
    //    }
    //}

    IEnumerator ResetJumpStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isJumping = false;
    }
}
