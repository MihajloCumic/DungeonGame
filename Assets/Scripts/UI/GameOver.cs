using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance { get; private set; }
    [SerializeField] private GameObject won;
    [SerializeField] private GameObject lost;
    [SerializeField] private GameObject container;
    [SerializeField] private Button button;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        button.onClick.AddListener(RestartScene);
    }


    public void Won()
    {
        Time.timeScale = 0f;
        container.SetActive(true);
        won.SetActive(true);
    }
    public void Lost()
    {
        Time.timeScale = 0f;
        container.SetActive(true);
        lost.SetActive(true);
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
