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
