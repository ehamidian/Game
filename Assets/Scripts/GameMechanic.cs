using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GameMechanic : MonoBehaviour
{
    private Rigidbody rb;
    //private int lastDirection = 2;

    public PlayerScore playerScore;
    public FollowPlayer followPlayer;

    //public float lateralSpeed = 100f;
    public float forwardSpeed = 20f;
    public float scaleFactor = 3.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Ensure rotation is frozen
        rb.useGravity = true;
    }

    void Update()
    {
        if (!playerScore.IsGameOver())
        {
            int loudness = AudioLoudnessDetection.Instance.audioLoudness;

            MovePlayer(loudness);
        }
    }

    // Move the player based on voice direction
    void MovePlayer(int loudness)
    {
        float targetLateralVelocity = 0f;
        float lateralSpeed = 100f;

        //if (loudness == 1)
        //{
        //    SoundEFManager.instance.PlaySoundEffect("move");
        //    Debug.Log("Moving left");
        //    rb.velocity = new Vector3(-lateralSpeed, rb.velocity.y, forwardSpeed);
        //}
        //else if (loudness == 2)
        //{
        //    SoundEFManager.instance.PlaySoundEffect("move");
        //    Debug.Log("Moving right");
        //    rb.velocity = new Vector3(lateralSpeed, rb.velocity.y, forwardSpeed);
        //}
        //else
        //{
        //    // No lateral movement if direction is 0
        //    rb.velocity = new Vector3(0, rb.velocity.y, forwardSpeed);
        //}

        switch (loudness)
        {
            case 1:
                targetLateralVelocity = -lateralSpeed;
                break;
            case 2:
                targetLateralVelocity = -lateralSpeed * 2;
                break;
            case 3:
                targetLateralVelocity = -lateralSpeed * 3;
                break;
            case 4:
                targetLateralVelocity = lateralSpeed;
                break;
            case 5:
                targetLateralVelocity = lateralSpeed * 2;
                break;
            case 6:
                targetLateralVelocity = lateralSpeed * 3;
                break;
            default:
                targetLateralVelocity = 0;
                break;
        }
        
        if(loudness != 0)
        {
            SoundEFManager.instance.PlaySoundEffect("move");
        }

        // Use Mathf.Sign to ensure the correct sign for the lateral velocity
        targetLateralVelocity *= Mathf.Sign(transform.localScale.x);

        // Gradually interpolate between current and target velocities
        Vector3 targetVelocity = new Vector3(targetLateralVelocity, 0, forwardSpeed);
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime * scaleFactor);
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

    void OnCollisionEnter(Collision collision)
    {
        if (playerScore == null)
        {
            Debug.LogError("playerScore is null!");
            return;
        }

        if (collision.gameObject.CompareTag("Mountain"))
        {
            SoundEFManager.instance.PlaySoundEffect("mountain");
        }

        if (collision.gameObject.CompareTag("Candle"))
        {
            //Debug.Log("Collision with Candle!");
            playerScore.CollectCandle();
            SoundEFManager.instance.PlaySoundEffect("flame");

            if (playerScore.ValidateFlameCollision())
            {
                forwardSpeed += 5f;
            }

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            //Debug.Log("Collision with Obstacle!");
            playerScore.CollideWithBox();
            SoundEFManager.instance.PlaySoundEffect("hit");
            Destroy(collision.gameObject);

            // Check for game over after colliding with the box
            if (playerScore.ValidateBoxCollision())
            {
                SoundEFManager.instance.PlaySoundEffect("lose");
                GameOver();
            }
        }
    }
}
