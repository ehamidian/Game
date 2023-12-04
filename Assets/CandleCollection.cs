using UnityEngine;
public class CandleCollection : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle candle collection logic here
            // For example, increase score and destroy the candle object
            Destroy(gameObject);
        }
    }
}