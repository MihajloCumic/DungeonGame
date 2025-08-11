using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject container;
    private bool paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                container.SetActive(false);
                Time.timeScale = 1f;
                paused = false;
                return;
            }
            container.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
    }
}
