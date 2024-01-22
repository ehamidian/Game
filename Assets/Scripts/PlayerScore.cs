using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public TextMeshProUGUI collectedCandlesText;
    public TextMeshProUGUI collectedBoxesText; // Display the number of boxes collected
    public TextMeshProUGUI gameOverText; // Reference to the game over text UI element
    public TextMeshProUGUI startMessageText; // Reference to the start message text UI element
    public TextMeshProUGUI winMessageText; // Reference to the win message text UI element
    [SerializeField] HealthBar healthBar;
    [SerializeField] BulletBar bulletBar;

    private int _score = 0;
    private int _boxCollisionCount = 0; // Counter for box collisions
    private int maxBoxCollisions = 5; // Maximum allowed box collisions before game over
    private int maxFlamesCollision = 5; // Maximum number of candles to collect
    private bool isGameOver = false;

    void Start()
    {
        // Set the initial state of UI elements
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        if (startMessageText != null)
        {
            // Show the start message for 3 seconds and then hide it
            startMessageText.gameObject.SetActive(true);
            Invoke("HideStartMessage", 3f); // Hide the start message after 3 seconds
        }

        if (winMessageText != null)
        {
            winMessageText.gameObject.SetActive(false);
        }

        if(healthBar == null || bulletBar == null)
        {
            Debug.LogError("HealthBar or BulletBar is null");
        }

        UpdateScoreText(); // Update the UI text to display the initial score
        healthBar.SetMaxHealth(100);
        bulletBar.SetMaxBullet(0);
    }

    void HideStartMessage()
    {
        if (startMessageText != null)
        {
            startMessageText.gameObject.SetActive(false);
        }
    }

    // Call this method when a candle is collected
    public void CollectCandle()
    {
        _score++;
        bulletBar.SetBullet(10);
        UpdateScoreText();

        if (_score == maxFlamesCollision)
        {
            //ShowWinMessage();
            healthBar.SetHealth(10);
        }

        if (_score == 1)
        {
            // Activate the game over text after the first candle is collected
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(false);
            }
        }
    }

    // Call this method when the player collides with a box
    public void CollideWithBox()
    {
        if (!isGameOver)
        {
            //UpdateBoxCollisionText();
            
            _boxCollisionCount++;
            float health = (float)maxBoxCollisions / (float)_boxCollisionCount;
            healthBar.SetHealth(health, true);

            if (ValidateBoxCollision())
            {
                ShowGameOver();
            }
        }
    }

    // Update the UI text to display the current score
    private void UpdateScoreText()
    {
        if (collectedCandlesText != null)
        {
            collectedCandlesText.text = "Candles: " + _score;
        }
    }

    // Update the UI text to display the number of boxes collected
    //private void UpdateBoxCollisionText()
    //{
    //    if (collectedBoxesText != null)
    //    {
    //        collectedBoxesText.text = "Obstacles: " + boxCollisionCount;
    //    }
    //}

    // Show game over text and stop the player
    public void ShowGameOver()
    {
        isGameOver = true;

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        // Stop the player or perform other game over actions
    }

    // Show win message
    //private void ShowWinMessage()
    //{
    //    if (winMessageText != null)
    //    {
    //        SoundEFManager.instance.PlaySoundEffect("win");
    //        winMessageText.gameObject.SetActive(true);
    //    }

    //    // You can perform additional actions when the player wins
    //}

    // Check if the game is over
    public bool IsGameOver()
    {
        return isGameOver;
    }

    // Validate if the player collided with the maximum allowed boxes
    public bool ValidateBoxCollision()
    {
        return _boxCollisionCount == maxBoxCollisions;
    }

    // Validate if the player collided with the maximum allowed flames
    public bool ValidateFlameCollision()
    {
        return _score % maxFlamesCollision == 0;
    }
}
