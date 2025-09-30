using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Super simple game manager for beginners learning singleton pattern
/// Students click buttons to see how singleton works
/// </summary>
public class SimpleGameManager : MonoBehaviour
{
    // This is the singleton pattern - only one GameManager exists
    public static SimpleGameManager Instance;
    
    [Header("Game Data")]
    public int score = 0;
    public int clicks = 0;
    
    [Header("UI Elements - Drag from scene")]
    public Text scoreText;
    public Text clickText;
    public Button clickButton;
    public Button resetButton;
    
    void Awake()
    {
        // Singleton pattern: only allow one GameManager to exist
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("✓ GameManager created!");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Extra GameManager destroyed - only one allowed!");
        }
    }
    
    void Start()
    {
        // Setup button clicks
        if (clickButton != null)
            clickButton.onClick.AddListener(OnButtonClicked);
        
        if (resetButton != null)
            resetButton.onClick.AddListener(OnResetClicked);
        
        UpdateUI();
        Debug.Log("Simple Clicker Game ready!");
    }
    
    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
        Debug.Log($"Score added: {points}. Total: {score}");
    }
    
    public void AddClick()
    {
        clicks++;
        UpdateUI();
        Debug.Log($"Click counted! Total clicks: {clicks}");
    }
    
    public void ResetGame()
    {
        score = 0;
        clicks = 0;
        UpdateUI();
        Debug.Log("Game reset!");
    }
    
    void OnButtonClicked()
    {
        // Add both score and click count
        AddScore(10);
        AddClick();
    }
    
    void OnResetClicked()
    {
        ResetGame();
    }
    
    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        
        if (clickText != null)
            clickText.text = "Clicks: " + clicks;
    }
}