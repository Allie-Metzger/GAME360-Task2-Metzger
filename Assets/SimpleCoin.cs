using UnityEngine;

/// <summary>
/// Very simple coin script that teaches singleton usage
/// Students click on coins to collect them
/// </summary>
public class SimpleCoin : MonoBehaviour
{
    [Header("Coin Settings")]
    public int pointsWorth = 10;
    
    void OnMouseDown()
    {
        // This is the KEY LEARNING: How to use the singleton!
        CollectCoin();
    }
    
    void CollectCoin()
    {
        Debug.Log("Coin clicked!");
        
        // MAIN LESSON: Check if GameManager exists first
        if (SimpleGameManager.Instance != null)
        {
            // Use the singleton to add score
            SimpleGameManager.Instance.AddScore(pointsWorth);
            Debug.Log($"Added {pointsWorth} points using GameManager singleton!");
            
            // Destroy this coin
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("No GameManager found! Make sure it exists in the scene.");
        }
    }
    
    void Start()
    {
        // Simple animation - coin spins
        InvokeRepeating("SpinCoin", 0f, 0.02f);
        
        Debug.Log("Coin ready! Click me to collect.");
    }
    
    void SpinCoin()
    {
        transform.Rotate(0, 2, 0);
    }
}