using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter Detected!");
    }
}
