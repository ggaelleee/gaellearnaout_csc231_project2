using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winMessage;

    private void Start()
    {
        titlePanel.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (titlePanel.activeSelf || winMessage.activeInHierarchy)
            return;

        if (Keyboard.current?.escapeKey.wasPressedThisFrame == true)
        {
            bool pause = !pausePanel.activeSelf;
            pausePanel.SetActive(pause);
            Time.timeScale = pause ? 0f : 1f;
        }
    }

    public void PlayGame()
    {
        titlePanel.SetActive(false);
        Resume();
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}