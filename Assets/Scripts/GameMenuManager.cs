using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // for TextMeshPro
using UnityEngine.UI; // for UI elements

public class GameMenuManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject endMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject controlsPanel;
    public GameObject player;

    public bool gameIsStarted = false;
    public Button pauseBtn;

    public TMP_Text startLevelText;
    public TMP_Text levelCompleteText;

    void Start()
    {
        Debug.Log("Start menu showing.");
        Time.timeScale = 0f; // Pause game at start
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<PlayerController>().canMove = false;

        startMenu.SetActive(true);
        endMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        controlsPanel.SetActive(false);


        // Disable pause button at start
        pauseBtn.interactable = false;
        pauseBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);

        // Get current scene index
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        string levelName = "LEVEL " + currentIndex;

        // Set text
        startLevelText.text = levelName;
    }

    public void StartGame()
    {
        Time.timeScale = 1f; // Unpause
        startMenu.SetActive(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<PlayerController>().canMove = true;
        gameIsStarted = true;

        // Enable pause button when game starts
        pauseBtn.interactable = true;
        pauseBtn.GetComponent<Image>().color = Color.white;
    }

    // Pause game
    public void PauseGame()
    {
        if (!gameIsStarted) return; // Do nothing if game hasn't started
        Time.timeScale = 0f; // Pause
        pauseMenu.SetActive(true);
        player.GetComponent<PlayerController>().canMove = false;
        player.GetComponent<SpriteRenderer>().enabled = false;

        // Disable pause button when game is paused
        pauseBtn.interactable = false;
        pauseBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    // Resume game
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Unpause
        pauseMenu.SetActive(false);
        player.GetComponent<PlayerController>().canMove = true;
        player.GetComponent<SpriteRenderer>().enabled = true;

        // Enable pause button when game resumes
        pauseBtn.interactable = true;
        pauseBtn.GetComponent<Image>().color = Color.white;
    }

    // Show controls
    public void ShowControls()
    {
        if (!gameIsStarted) return; // Do nothing if game hasn't started
        // Show controls panel here
        pauseMenu.SetActive(false);
        controlsPanel.SetActive(true);

    }

    public void HideControls()
    {
        // Hide controls panel here
        controlsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ShowEndMenu()
    {
        Time.timeScale = 0f; // Pause when game ends
        endMenu.SetActive(true);
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<PlayerController>().canMove = false;

        // Get current scene index
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        string levelName = "LEVEL " + currentIndex;

        // Set text
        levelCompleteText.text = levelName + " COMPLETE!";
        Debug.Log("End menu showing.");
    }

    public void ShowGameOverMenu()
    {
        Time.timeScale = 0f; // Pause when game ends
        gameOverMenu.SetActive(true);
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<PlayerController>().canMove = false;
        Debug.Log("Game Over menu showing.");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void NextLevel()
    {
        Time.timeScale = 1f;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("No more levels! Returning to Main Menu...");
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

