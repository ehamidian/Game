using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public TextMeshProUGUI collectedCandlesText;
    private int score = 0;

    // Call this method when a candle is collected
    public void CollectCandle()
    {
        score++;
        UpdateScoreText();
    }

    // Update the UI text to display the current score
    private void UpdateScoreText()
    {
        if (collectedCandlesText != null)
        {
            collectedCandlesText.text = "Collected Candles: " + score;
        }
    }
}
