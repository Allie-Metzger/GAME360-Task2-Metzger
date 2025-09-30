using UnityEngine;

/// <summary>
/// Simple button that students can click to understand singleton pattern
/// This shows different ways to interact with the GameManager singleton
/// </summary>
public class SimpleButton : MonoBehaviour
{
    [Header("What this button does")]
    public int scoreToAdd = 20;
    public Color buttonColor = Color.green;
    
    void Start()
    {
        // Change button color so it's easy to see
        GetComponent<Renderer>().material.color = buttonColor;
        
        Debug.Log("Button ready! Click me to add " + scoreToAdd + " points!");
    }
    
    void OnMouseDown()
    {
        Debug.Log("Button clicked!");
        
        // LEARNING POINT: Another way to use the singleton
        GivePoints();
    }
    
    void GivePoints()
    {
        // Check if GameManager exists (always do this!)
        if (SimpleGameManager.Instance == null)
        {
            Debug.LogError("Cannot give points - GameManager not found!");
            return;
        }
        
        // Use the singleton to add score
        SimpleGameManager.Instance.AddScore(scoreToAdd);
        Debug.Log($"Button gave {scoreToAdd} points to player!");
        
        // Make button flash when clicked
        StartCoroutine(FlashButton());
    }
    
    System.Collections.IEnumerator FlashButton()
    {
        // Simple visual feedback
        Color originalColor = buttonColor;
        GetComponent<Renderer>().material.color = Color.white;
        
        yield return new WaitForSeconds(0.1f);
        
        GetComponent<Renderer>().material.color = originalColor;
    }
}