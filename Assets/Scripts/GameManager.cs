using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject gameOverPanel;    // Drag your panel here
    public Button restartButton;        // Drag your restart button here

    [Header("Player Reference")]
    public GameObject player;           // Drag your player GameObject here

    void Start()
    {
        // Hide the game over panel initially
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Setup restart button click
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        // If player reference is not set, try to find it automatically
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                Debug.Log("Player found automatically");
        }
    }

    void Update()
    {
        // Check if player is destroyed or not active
        if (player == null || !player.activeInHierarchy)
        {
            GameOver();
        }
    }

    // Call this function when player loses
    public void GameOver()
    {
        // Show the restart options
        if (gameOverPanel != null && !gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over Panel activated");
        }

        // Pause the game
        Time.timeScale = 1f;
    }

    // Restart the current scene
    public void RestartGame()
    {
        Debug.Log("Restarting game...");

        // Resume time if it was paused
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Optional: You can also call this from other scripts when player dies
    public void PlayerDied()
    {
        GameOver();
    }
}