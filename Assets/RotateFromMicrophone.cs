<<<<<<< HEAD
using UnityEngine;
using System.Collections;

public class RotateFromMicrophone : MonoBehaviour
{
    private Rigidbody rb;
    private bool isJumping = false;

    public AudioLoudnessDetection loudnessDetector;
    public PitchDetection pitchDetector;
    public float forwardSpeed = 5f; // Adjust the forward movement speed as needed
    public float lateralSpeed = 3f; // Adjust the lateral movement speed as needed
    public float jumpForce = 10f;

    public PlayerScore playerScore; // Reference to the PlayerScore script

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Ensure rotation is frozen
    }

    void Update()
    {
        if (Time.frameCount > 60)
        {
            int direction = loudnessDetector.GetDirection();
            int jumpAction = pitchDetector.Jump();

            // Move the player forward continuously
            MoveForward();

            // Move the player based on voice direction
            MovePlayer(direction);

            // Handle jump action
            HandleJump(jumpAction);
        }
    }

    void MoveForward()
    {
        // Move the player forward continuously
        rb.velocity = new Vector3(0, rb.velocity.y, forwardSpeed);
    }

    void MovePlayer(int direction)
    {
        // Move the player left or right based on voice direction
        if (direction == -1)
        {
            rb.velocity = new Vector3(-lateralSpeed, rb.velocity.y, forwardSpeed);
        }
        else if (direction == 1)
        {
            rb.velocity = new Vector3(lateralSpeed, rb.velocity.y, forwardSpeed);
        }
        else
        {
            // No lateral movement if direction is 0
            rb.velocity = new Vector3(0, rb.velocity.y, forwardSpeed);
        }
    }

    void HandleJump(int jumpAction)
    {
        if (jumpAction == 1 && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;

            // Reset the jump state after a delay
            StartCoroutine(ResetJumpStateAfterDelay(0.1f));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.CompareTag("Candle"))
    {
        Debug.Log("Collision with Candle!");
        playerScore.CollectCandle();
        Destroy(collision.gameObject);
    }
    }

    IEnumerator ResetJumpStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isJumping = false;
    }
}
=======
using System.Collections;
using UnityEngine;

public class RotateFromMicrophone : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 initialPosition;
    private bool isJumping = false;

    public AudioLoudnessDetection loudnessDetector;
    public PitchDetection pitchDetector;
    public float movementSpeed = 100f;
    public float jumpForce = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    void Update()
    {
        if (Time.frameCount > 60)
        {
            int direction = loudnessDetector.GetDirection();
            int jumpAction = pitchDetector.Jump();

            //Move the cube based on the direction
            if (direction == -1)
            {
                // Move left
                //transform.Translate(movementSpeed * Time.deltaTime * Vector3.left);
                rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            }
            else if (direction == 1)
            {
                // Move right
                //transform.Translate(movementSpeed * Time.deltaTime * Vector3.right);
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            }

            if (jumpAction == 1 && !isJumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = true;

                // Reset the jump state after a delay
                StartCoroutine(ResetJumpStateAfterDelay(0.1f));
            }
            else
            {
                rb.AddForce(Physics.gravity, ForceMode.Acceleration);

                if (!isJumping && transform.position.y > initialPosition.y)
                {
                    transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }

            IEnumerator ResetJumpStateAfterDelay(float delay)
            {
                yield return new WaitForSeconds(delay);
                isJumping = false;
            }
        }
    }
}
>>>>>>> e0ffee059ea6b266d29539e6e92285809074c5b5
