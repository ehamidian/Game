using UnityEngine;
using UnityEngine.Windows.Speech;

public class GameMechanic : MonoBehaviour
{
    private Rigidbody rb;
    private bool isJumping = false;
    private KeywordRecognizer keywordRecognizer;
    private string selectedMicrophone1;
    private string selectedMicrophone2;
    private int lastDirection = 2;
    private string[] keywords = new string[] { "up" };

    protected string word = "";

    public AudioLoudnessDetection loudnessDetector;
    public MicrophoneSettings microphoneSettings;
    public PlayerScore playerScore; // Reference to the PlayerScore script
    public FollowPlayer followPlayer; // Reference to the FollowPlayer script

    public float jumpForce = 10f;
    public float forwardSpeed = 5f;
    public float lateralSpeed = 10f;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Ensure rotation is frozen
        rb.useGravity = true;

        if (microphoneSettings == null || loudnessDetector == null) Debug.LogError("Null object!");

        selectedMicrophone1 = microphoneSettings.Microphones[0];
        selectedMicrophone2 = microphoneSettings.Microphones[2];

        InitialKeyRecognition();
    }

    void Update()
    {
        if (!playerScore.IsGameOver())
        {
            if (!isJumping)
            {
                // Get the direction from the AudioLoudnessDetection script
                int direction = loudnessDetector.GetDirection(selectedMicrophone1, selectedMicrophone2,
                microphoneSettings.audioSourceLeft.clip, microphoneSettings.audioSourceRight.clip);

                // Move the player based on voice direction
                MovePlayer(direction);

                // Perform actions based on direction
                if (direction != lastDirection)
                {
                    // Direction has changed, perform actions
                    lastDirection = direction;

                    // Perform actions based on direction
                    if (direction == -1)
                    {
                        // Move left
                        Debug.Log("Move to the left");
                    }
                    else if (direction == 1)
                    {
                        // Move right
                        Debug.Log("Move to the right");
                    }
                    else
                    {
                        // No movement
                        Debug.Log("Stable");
                    }
                }
            }

            if (isGrounded)
            {
                // Player is jumping, perform jump action
                Jump();
            }

        }
    }

    void MovePlayer(int direction)
    {
        // Move the player based on voice direction
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

    // Speech recognition using keywords
    void InitialKeyRecognition()
    {
        ConfidenceLevel confidence = ConfidenceLevel.Medium;

        if (keywords == null || keywords.Length == 0)
        {
            Debug.LogError("No keys in the array keywords.");
            return;
        }
        else
        {
            Debug.Log($"The key is: {keywords[0]}");
        }

        keywordRecognizer = new KeywordRecognizer(keywords, confidence);

        if (keywordRecognizer == null)
        {
            Debug.LogError("KeywordRecognizer is null.");
            return;
        }

        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        word = speech.text;
        Debug.Log(word);

        if (word == "up")
        {
            isJumping = true;
        }
    }

    void Jump()
    {
        if (rb != null && isJumping)
        {
            // rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, forwardSpeed);
            isJumping = false; // Reset the jump flag after performing the jump
        }
    }

    void GameOver()
    {
        if (followPlayer == null)
        {
            Debug.LogError("followPlayer is null!");
            return;
        }

        playerScore.ShowGameOver();
        followPlayer.GameOver();  // Call the GameOver method on the FollowPlayer script
        StopPlayerAndCamera();
    }

    void StopPlayerAndCamera()
    {
        // Stop the player
        rb.velocity = Vector3.zero;

        // Stop the camera follow
        followPlayer.enabled = false;
    }

    void OnDestroy()
    {
        // Stop the KeywordRecognizer when the script is destroyed or when it's no longer needed.
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.OnPhraseRecognized -= RecognizedSpeech;
            keywordRecognizer.Stop();
        }
        else
        {
            return;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (playerScore == null)
        {
            Debug.LogError("playerScore is null!");
            return;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Candle"))
        {
            Debug.Log("Collision with Candle!");
            playerScore.CollectCandle();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            Debug.Log("Collision with Obstacle!");
            playerScore.CollideWithBox();
            Destroy(collision.gameObject);

            // Check for game over after colliding with the box
            if (playerScore.GetBoxCollisionCount() >= 3)
            {
                GameOver();
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
