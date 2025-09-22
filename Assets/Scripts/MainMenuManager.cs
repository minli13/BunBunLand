using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject startPanel;

    public void Awake()
    {
        // Ensure controls panel is hidden at start
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
            startPanel.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1"); // load gameplay scene
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game!"); // works in editor
    }

    // Show controls
    public void ShowControls()
    {
        // Show controls panel here
        startPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void ShowStartMenu()
    {
        // Show start menu here
        controlsPanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
