using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScroll : MonoBehaviour
{
    //private int timeinCredits = 0;

    public float speed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        Invoke(nameof(ReturnToMainMenu), 10f);
        //transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        //once time on screen = 60 seconds, go to main menu
        //SceneManager.LoadScene("Start Menu");
        
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }    
}
