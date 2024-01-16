using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public TextMeshProUGUI collectedCandlesText;
    public TextMeshProUGUI collectedBoxesText; // Display the number of boxes collected
    public TextMeshProUGUI gameOverText; // Reference to the game over text UI element
    public TextMeshProUGUI startMessageText; // Reference to the start message text UI element
    public TextMeshProUGUI winMessageText; // Reference to the win message text UI element
    private int score = 0;
    private int boxCollisionCount = 0; // Counter for box collisions
    public int maxBoxCollisions = 3; // Maximum allowed box collisions before game over
    private bool isGameOver = false;
    [SerializeField] Healthbar healthbar;
    private int maxHealth = 100;

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

        UpdateScoreText(); // Update the UI text to display the initial score
        healthbar.SetMaxHealth(maxHealth);
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
        score++;
        healthbar.SetHealth(10, "score");
        UpdateScoreText();

        if (score == 5)
        {
            ShowWinMessage();
        }

        if (score == 1)
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
            boxCollisionCount++;
            float health = (float)maxBoxCollisions / (float)boxCollisionCount;
            healthbar.SetHealth(health);
            //UpdateBoxCollisionText();

            if (boxCollisionCount >= maxBoxCollisions)
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
            collectedCandlesText.text = "Candles: " + score;
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
    private void ShowWinMessage()
    {
        if (winMessageText != null)
        {
            SoundEFManager.instance.PlaySoundEffect("win");
            winMessageText.gameObject.SetActive(true);
        }

        // You can perform additional actions when the player wins
    }

    // Check if the game is over
    public bool IsGameOver()
    {
        return isGameOver;
    }

    // Get the box collision count
    public int GetBoxCollisionCount()
    {
        return boxCollisionCount;
    }
}
